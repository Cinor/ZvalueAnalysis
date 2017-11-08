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
        /// 
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


        public void Get()
        {
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
        }


    }
}