using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CompanyFinancialAnalysis.Models
{
    class MyWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest WR = base.GetWebRequest(uri);
            WR.Timeout = 5 * 1000;
            return WR;
        }
    }
}
