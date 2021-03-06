﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyFinancialAnalysis.Models;

namespace CompanyFinancialAnalysis.Models
{
    public class Company
    {
        public string Ticker { get; set; }

        /// <summary>
        /// 公司名稱
        /// </summary>
        public string Name { get; set; }

        //季別
        public string Date { get; set; }

        //營運資金
        public double WorkingCapital { get; set; }

        /// <summary>
        /// 保留盈餘
        /// </summary>
        public double RetainedEarning { get; set; }

        //營業利益
        public double EBIT { get; set; }

        //總資產
        public double TotalAsset { get; set; }

        //總負債
        public double TotalLiability { get; set; }

        //營業收入
        public double GrossSales { get; set; }

        //總市值
        public double MarketValue { get; set; }

        //股本
        public int CompanyStock { get; set; }

        //股價
        public double StockPrice { get; set; }

        //z value
        public double ZValue { get; set; }

        /// <summary>
        /// 股東權益
        /// </summary>
        public double Equity { get; set; }

        public CompanyBalanceData compBD { get; set; }

        public CompanyCashData compCD { get; set; }
    }
}
