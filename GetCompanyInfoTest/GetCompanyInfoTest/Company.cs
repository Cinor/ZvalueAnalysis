using System;
using System.Collections.Generic;
using System.Text;

namespace GetCompanyInfoTest
{
    class Company
    {
        /// <summary>
        /// 股票代號
        /// </summary>
        public string Ticker { get; set; }

        /// <summary>
        /// 公司名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 季別(Ex:2017Q2)
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 營運資金
        /// </summary>
        public double WorkingCapital { get; set; }

        /// <summary>
        /// 保留盈餘
        /// </summary>
        public double RetainedEarning { get; set; }

        /// <summary>
        /// 營業利益
        /// </summary>
        public double EBIT { get; set; }

        /// <summary>
        /// 總資產
        /// </summary>
        public double TotalAsset { get; set; }

        /// <summary>
        /// 總負債
        /// </summary>
        public double TotalLiability { get; set; }

        /// <summary>
        /// 營業收入
        /// </summary>
        public double GrossSales { get; set; }

        /// <summary>
        /// 總市值
        /// </summary>
        public double MarketValue { get; set; }

        /// <summary>
        /// 股本
        /// </summary>
        public int CompanyStock { get; set; }

        /// <summary>
        /// 股價
        /// </summary>
        public double StockPrice { get; set; }

        /// <summary>
        /// z value
        /// </summary>
        public double ZValue { get; set; }

        /// <summary>
        /// 股東權益
        /// </summary>
        public double Equity { get; set; }
    }
}
