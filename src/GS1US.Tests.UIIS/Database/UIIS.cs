using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using static LanguageExt.Prelude;

namespace GS1US.Tests.UIIS.Database
{
    class UIIS
    {
        private readonly SqlConnection conn;

        public UIIS(SqlConnection connection)
        {
            conn = connection;
        }

        public void Close()
        {
            conn.Close();
        }

        public Option<string> GetLastPrefixVended(string prefixType, int capacity)
        {
            // get all open ranges and number of vended prefixes for them
            var s2 = @"
            select * from
                (select pr.Value, sum(iif(p.Value is null, 0, 1)) n
                from dbo.CompanyPrefixRange pr
                left join dbo.CompanyPrefix p on CHARINDEX(pr.Value, p.Value)=1
                where pr.PrefixWidth=@PrefixWidth and pr.Type=@PrefixType
                group by pr.Value
                ) t
            where n=0 or n < POWER(10, @PrefixWidth - 3)
            ";

            var s = @"
            select top 1 Value from dbo.CompanyPrefix where CHARINDEX(@Range, Value)=1
            order by Value desc
            ";

            var width = 12 - (int)Math.Ceiling(Math.Log10(capacity));
            return conn.Query<Range>(s2, new { PrefixWidth = width, PrefixType = prefixType })
                .HeadOrNone()
                .Bind(range =>
                {
                    if (range.N == 0)
                    {
                        return Some(range.Value.PadRight(width, '0'));
                    }
                    else
                    {
                        return Some(conn.QuerySingle<string>(s, new { Range = range.Value }));
                    }
                });
        }

        public string GetLastPrefixVended2(string prefixType, int capacityToAvoid)
        {
            var s = @"
            select top 1 Value from dbo.CompanyPrefix
            where left(Value, 3) in (
                select top 1 Value from dbo.CompanyPrefixRange
                where PrefixWidth!=@PrefixWidth and Type=@PrefixType
            )
            order by Value desc
            ";

            var width = 12 - (int)Math.Ceiling(Math.Log10(capacityToAvoid));
            return conn.QuerySingle<string>(s, new { PrefixWidth = width, PrefixType = prefixType });
        }

        public IEnumerable<LabelerCode> GetFreeLabelerCodes(int count)
        {
            var s = @"
            select top (@N) *
            from dbo.LabelerCode lc
            left join dbo.CompanyPrefix p on '03' + lc.Code = Value
            where p.Id is null
            ";

            return conn.Query<LabelerCode>(s, new { N = count });
        }

        public string GetPrefixRangeForAutoVendToOpen(string prefixType, int capacity)
        {
            var s = @"
            select top 1 Value from
            (select ar.Value, max(len(p.Value)) a, min(len(p.Value)) b, count(p.Value) as c
            from dbo.CompanyPrefixAvailableRange ar
            left join dbo.CompanyPrefix p on charindex(ar.Value, p.Value) = 1
            where ar.Type=@PrefixType
            group by ar.Value) as t
            where (c = 0 or a = @Width)
            ";
            var width = 12 - (int)Math.Ceiling(Math.Log10(capacity));
            return conn.QuerySingle<string>(s, new { Width = width, PrefixType = prefixType });
        }

        public string GetSmallestOpenRangeForAutoVend(string prefixType, int capacity)
        {
            var s = @"
            select top 1 Value
            from dbo.CompanyPrefixRange pr
            where pr.Type=@PrefixType
            and pr.PrefixWidth=@Width
            and AutoAssign=1
            ";
            var width = 12 - (int)Math.Ceiling(Math.Log10(capacity));
            return conn.QuerySingle<string>(s, new { PrefixType = prefixType, Width = width });
        }

        public string GetPrefixRangeForManualVendToOpen(string prefixType, int capacity, bool randomize=true)
        {
            var s = @"
            select rng from
                (select left(Value,3) rng, max(len(Value)) w, count(*) n
                from dbo.CompanyPrefix
                where len(Value) >= 7
                group by left(Value,3)) t
            where n >= power(10, w-3) or w != @Width

            union

            select Value from dbo.CompanyPrefixRange

            union

            select Value from dbo.CompanyPrefixAvailableRange
            ";

            IEnumerable<int> makeList()
            {
                switch (prefixType)
                {
                    case "UPC":
                        return Enumerable.Range(0, 100).Where(n => n < 20 || n > 59);
                    case "EAN":
                        return Enumerable.Range(100, 40);
                    default:
                        throw new ArgumentException($"invalid prefix type: {prefixType}");
                }
            }

            var width = 12 - (int)Math.Ceiling(Math.Log10(capacity));
            var r1 = conn.Query<string>(s, new { Width = width });
            var list = makeList().Select(o => $"{o}".PadLeft(3, '0')).Except(r1);
            if (randomize)
            {
                var x = new Random().Next(list.Count());
                return list.ElementAt(x);
            }
            else
            {
                return list.OrderBy(identity).First();
            }
        }

        private string GetOpenRangeToClose(int autoAssign)
        {
            // NDC ranges (03) can't be closed
            var s = @"
            select top 1 Value from dbo.CompanyPrefixRange
            where Value not like '03%' and AutoAssign = @Auto
            ";
            return conn.QuerySingle<string>(s, new { Auto = autoAssign });
        }

        public string GetOpenRangeToCloseForAutoVend() => GetOpenRangeToClose(1);

        public string GetOpenRangeToCloseForManualVend() => GetOpenRangeToClose(0);

        public string GetOpenRange(string prefixType)
        {
            // exclude NDC ranges (03)
            var s = @"
            select top 1 Value from dbo.CompanyPrefixRange
            where Value not like '03%' and Type=@PrefixType
            ";
            return conn.QuerySingle<string>(s, new { PrefixType = prefixType });
        }

        public IEnumerable<Prefix> GetHeldPrefixByValue(string prefix)
        {
            var s = @"select * from dbo.CompanyPrefixHold where Value=@Prefix";
            return conn.Query<Prefix>(s, new { Prefix = prefix });
        }

        public IEnumerable<DetailedPrefix> GetHeldPrefixByType(string type, int n)
        {
            var s = @"
            select top (@N) h.Id, h.Value, hl.Account, hl.Type
            from dbo.CompanyPrefixHold h
            left join (
			    select Value, max(Id) id from dbo.CompanyPrefixHoldLog
			    group by Value
			    ) as t on h.Value = t.Value
			left join dbo.CompanyPrefixHoldLog hl on t.id=hl.Id
            left join dbo.CompanyPrefix p on h.Value=p.Value
			where hl.StatusId=2 and hl.Type=@PrefixType and p.Value is null
            ";
            return conn.Query<DetailedPrefix>(s, new { N = n, PrefixType = type });
        }

        public IEnumerable<Prefix> GetPrefixByValue(string prefix)
        {
            var s = @"select * from dbo.CompanyPrefix where Value=@Prefix";
            return conn.Query<Prefix>(s, new { Prefix = prefix });
        }

        public IEnumerable<OpenRange> OpenRangeByPredicate(string range)
        {
            var s = @"
            select * from dbo.CompanyPrefixRange
            where Value=@Range
            ";
            return conn.Query<OpenRange>(s, new { Range = range });
        }

        public IEnumerable<AvailableRange> AvailableRangeByPredicate(string range)
        {
            var s = @"
            select * from dbo.CompanyPrefixAvailableRange
            where Value=@Range
            ";
            return conn.Query<AvailableRange>(s, new { Range = range });
        }
    }

    class Range
    {
        public string Value;
        public int N;  // number of vended prefixes under the range
    }

    class LabelerCode
    {
        public string Code;
        public string FirmName;
    }

    class OpenRange
    {
        public int Id;
        public String Value;
        public String Type;
        public int PrefixWidth;
        public bool AutoAssign;
    }

    class AvailableRange
    {
        public int Id;
        public String Value;
        public String Type;
    }

    class Prefix
    {
        public int Id;
        public string Value;
    }

    class DetailedPrefix
    {
        public int Id;
        public string Value;
        public string Account;
        public string Type;
    }
}
