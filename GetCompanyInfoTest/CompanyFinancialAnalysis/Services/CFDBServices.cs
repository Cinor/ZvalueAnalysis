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
        public List<Company> GetCompanyTableTenData(string stockId)
        {

            var resultList = (from DB in CFDB.CompanyDataTable
                              where DB.Ticker == stockId && Convert.ToInt32(DB.Date.Substring(0, 4)) <= Convert.ToInt32(DateTime.Today.Year) 
                              orderby Convert.ToInt32(DB.Date.Substring(0, 4))
                              select DB).ToList();

            var compTenYrDataLst = (from item in resultList
                                    select new Company()
                                    {
                                        Ticker = item.Ticker,

                                        Name = item.Name,

                                        Date = item.Date,

                                        WorkingCapital = item.WorkingCapital.GetValueOrDefault(),

                                        RetainedEarning = item.RetainedEarning.GetValueOrDefault(),

                                        EBIT = item.EBIT.GetValueOrDefault(),

                                        TotalAsset = item.TotalAsset.GetValueOrDefault(),

                                        TotalLiability = item.TotalLiability.GetValueOrDefault(),

                                        Equity = item.Equity.GetValueOrDefault(),

                                        GrossSales = item.GrossSales.GetValueOrDefault(),

                                        StockPrice = item.StockPrice.GetValueOrDefault(),

                                        MarketValue = item.MarketValue.GetValueOrDefault(),

                                        CompanyStock = Convert.ToInt32(item.CompanyStock),

                                        ZValue = Convert.ToDouble(item.ZValue)

                                    }).ToList();

            return compTenYrDataLst;

        }

    }
}