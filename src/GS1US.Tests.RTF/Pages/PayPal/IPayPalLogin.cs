using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS1US.Tests.RTF.Pages.PayPal
{
    interface IPayPalLogin
    {
        IPayPalLogin FillCredentials(string email, string password);
        IPayPalLogin Login();
    }
}
