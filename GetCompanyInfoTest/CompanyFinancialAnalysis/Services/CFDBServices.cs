using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CompanyFinancialAnalysis.Models;

namespace CompanyFinancialAnalysis.Services
{

    public class CFDBServices
    {
        CorporateFinanceDBEntities CFDB = new CorporateFinanceDBEntities();

        /// <summary>
        /// 抓取資產負債表
        /// </summary>
        /// <param name="stockId">(EX: 6166 )</param
        /// <param name="data">(EX: 2017Q4 )</param>
        /// <returns> IQueryable<BalanceSheet> item </returns>
        public IQueryable<BalanceSheet> GetBSTableBystockId(string stockId, string data)
        {
            var result = (from DB in CFDB.BalanceSheet
                          where DB.Ticker == stockId & DB.Date == data
                          select DB);

            return result;
        }

        /// <summary>
        /// 抓取損益表
        /// </summary>
        /// <param name="stockId">(EX: 6166 )</param
        /// <param name="data">(EX: 2017Q4 )</param>
        /// <returns> IQueryable<IncomeStatement> item </returns>
        public IQueryable<IncomeStatement> GetISTableBystockId(string stockId, string data)
        {
            var result = (from DB in CFDB.IncomeStatement
                          where DB.Ticker == stockId & DB.Date == data
                          select DB);

            return result;
        }

        /// <summary>
        /// 抓取CompanyTable
        /// </summary>
        /// <param name="stockId"></param>
        /// <param name="data"></param>
        /// <returns> IQueryable<CompanyDataTable> item </returns>
        public IQueryable<CompanyDataTable> GetCompanyTableBystockId(string stockId, string data)
        {
            var result = (from DB in CFDB.CompanyDataTable
                          where DB.Ticker == stockId & DB.Date == data
                          select DB);

            return result;
        }


        /// <summary>
        /// 測試從CompanyDataTable取值
        /// </summary>
        /// <param name="stockId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public Company GetCompanyTable(string stockId, string date)
        {
            Company company = new Company();

            CFDBServices CFDB = new CFDBServices();

            var CTitem = CFDB.GetCompanyTableBystockId(stockId, date);

            foreach (var item in CTitem)
            {
                company.Ticker = item.Ticker;

                company.Name = item.Name;

                company.Date = item.Date;

                company.WorkingCapital = item.WorkingCapital.GetValueOrDefault();

                company.RetainedEarning = item.RetainedEarning.GetValueOrDefault();

                company.EBIT = item.EBIT.GetValueOrDefault();

                company.TotalAsset = item.TotalAsset.GetValueOrDefault();

                company.TotalLiability = item.TotalLiability.GetValueOrDefault();

                company.Equity = item.Equity.GetValueOrDefault();

                company.GrossSales = item.GrossSales.GetValueOrDefault();

                company.StockPrice = item.StockPrice.GetValueOrDefault();

                company.MarketValue = item.MarketValue.GetValueOrDefault();

                company.CompanyStock = Convert.ToInt32(item.CompanyStock);

                company.ZValue = Convert.ToDouble(item.ZValue);

            }

            return company;

        }

        //public Company GetCompanyByTwoTable(string stockId, string data)
        //{
        //    CFDBServices CFDB = new CFDBServices();

        //    var BSData = CFDB.GetBSTableBystockId(stockId, data).ToList();

        //    var ISData = CFDB.GetISTableBystockId(stockId, data).ToList();

        //    var Comp = CFDB.GetCompanyTableBystockId(stockId, data).ToList();

        //    var result = new Company
        //    {
        //        Ticker = BSData.FirstOrDefault().Ticker,

        //        Name = Comp.FirstOrDefault().Name,

        //        Date = BSData.FirstOrDefault(),

        //        WorkingCapital = StringToInt(BSData["流動資產"]) - StringToInt(BSData["流動負債"]),

        //        RetainedEarning = StringToInt(BSData["保留盈餘"]),

        //        EBIT = StringToInt(ISData["營業收入"]) - StringToInt(ISData["營業成本"]) - StringToInt(ISData["營業費用"]),

        //        TotalAsset = StringToInt(BSData["資產總額"]),

        //        TotalLiability = StringToInt("0"),

        //        Equity = StringToInt("0"),

        //        GrossSales = StringToInt(ISData["營業收入"]),

        //        StockPrice = Convert.ToDouble(GetStockPriceFromYah(stockId, date)),

        //        MarketValue = StringToInt(BSData["股本"]) / 10 * company.StockPrice,

        //        CompanyStock = StringToInt(BSData["股本"]),

        //        ZValue = GetZValue(company.WorkingCapital, company.RetainedEarning, company.EBIT, company.MarketValue, company.GrossSales, company.TotalAsset, company.TotalLiability)

        //    };

        //    return result;
        //}


    }
}