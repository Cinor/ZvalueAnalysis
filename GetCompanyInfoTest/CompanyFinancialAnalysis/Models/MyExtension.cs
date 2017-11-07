using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyFinancialAnalysis.Models
{
    public static class MyExtensions
    {
        /// <summary>
        /// "20170101" to datetime
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ToDatetime(this String str)
        {
            return new DateTime(Convert.ToInt32(str.Substring(0,4)), Convert.ToInt32(str.Substring(4,2)), Convert.ToInt32(str.Substring(6)));
        }
    }
}
