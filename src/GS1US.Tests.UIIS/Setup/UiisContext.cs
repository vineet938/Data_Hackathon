using GS1US.Tests.UIIS.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.UIIS.Setup
{
    class UiisContext
    {
        public Name TestAccount;
        public int SelectedCapacity;
        public string PrefixToVendOrHold;
        public LabelerCode LabelerCode;
        public string Range;   // the default range from which prefixes are auto-vended
        public string Range2;
    }
}
