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
        /// 依照stockId從CompanyDataTable取10筆近年資料
        /// </summary>
        /// <param name="stockId"></param>
        /// <param name="date"></param>
        /// <returns> List<Company> TenData </returns>
        public List<Company> GetCompanyTableTenData(string stockId)
        {

            //List<CompanyDataTable> listitem = new List<CompanyDataTable>();
            //listitem.Add(new CompanyDataTable
            //{
            //    Ticker = "6166",
            //    Name = "小倒暗",
            //    Date = "2017Q4",
            //    WorkingCapital = 12213132,
            //    RetainedEarning = 13123123,
            //    EBIT = 123123123,
            //    TotalAsset = 123123123,
            //    TotalLiability = 123123123,
            //    Equity = 123123123,
            //    GrossSales = 123123123,
            //    StockPrice = 123123123,
            //    MarketValue = 123123123123,
            //    CompanyStock = 5000,
            //    ZValue = "123123123123"
            //});

            //listitem.Add(new CompanyDataTable
            //{
            //    Ticker = "6166",
            //    Name = "小倒暗",
            //    Date = "2016Q4",
            //    WorkingCapital = 12213132,
            //    RetainedEarning = 13123123,
            //    EBIT = 123123123,
            //    TotalAsset = 123123123,
            //    TotalLiability = 123123123,
            //    Equity = 123123123,
            //    GrossSales = 123123123,
            //    StockPrice = 123123123,
            //    MarketValue = 123123123123,
            //    CompanyStock = 5000,
            //    ZValue = "123123123123"
            //});
            //listitem.Add(new CompanyDataTable
            //{
            //    Ticker = "6166",
            //    Name = "小倒暗",
            //    Date = "2015Q4",
            //    WorkingCapital = 12213132,
            //    RetainedEarning = 13123123,
            //    EBIT = 123123123,
            //    TotalAsset = 123123123,
            //    TotalLiability = 123123123,
            //    Equity = 123123123,
            //    GrossSales = 123123123,
            //    StockPrice = 123123123,
            //    MarketValue = 123123123123,
            //    CompanyStock = 5000,
            //    ZValue = "123123123123"
            //});
            //listitem.Add(new CompanyDataTable
            //{
            //    Ticker = "6166",
            //    Name = "小倒暗",
            //    Date = "2014Q4",
            //    WorkingCapital = 12213132,
            //    RetainedEarning = 13123123,
            //    EBIT = 123123123,
            //    TotalAsset = 123123123,
            //    TotalLiability = 123123123,
            //    Equity = 123123123,
            //    GrossSales = 123123123,
            //    StockPrice = 123123123,
            //    MarketValue = 123123123123,
            //    CompanyStock = 5000,
            //    ZValue = "123123123123"
            //});
            //listitem.Add(new CompanyDataTable
            //{
            //    Ticker = "6166",
            //    Name = "小倒暗",
            //    Date = "2013Q4",
            //    WorkingCapital = 12213132,
            //    RetainedEarning = 13123123,
            //    EBIT = 123123123,
            //    TotalAsset = 123123123,
            //    TotalLiability = 123123123,
            //    Equity = 123123123,
            //    GrossSales = 123123123,
            //    StockPrice = 123123123,
            //    MarketValue = 123123123123,
            //    CompanyStock = 5000,
            //    ZValue = "123123123123"
            //});
            //listitem.Add(new CompanyDataTable
            //{
            //    Ticker = "6166",
            //    Name = "小倒暗",
            //    Date = "2012Q4",
            //    WorkingCapital = 12213132,
            //    RetainedEarning = 13123123,
            //    EBIT = 123123123,
            //    TotalAsset = 123123123,
            //    TotalLiability = 123123123,
            //    Equity = 123123123,
            //    GrossSales = 123123123,
            //    StockPrice = 123123123,
            //    MarketValue = 123123123123,
            //    CompanyStock = 5000,
            //    ZValue = "123123123123"
            //});
            //listitem.Add(new CompanyDataTable
            //{
            //    Ticker = "6166",
            //    Name = "小倒暗",
            //    Date = "2011Q4",
            //    WorkingCapital = 12213132,
            //    RetainedEarning = 13123123,
            //    EBIT = 123123123,
            //    TotalAsset = 123123123,
            //    TotalLiability = 123123123,
            //    Equity = 123123123,
            //    GrossSales = 123123123,
            //    StockPrice = 123123123,
            //    MarketValue = 123123123123,
            //    CompanyStock = 5000,
            //    ZValue = "123123123123"

            //});
            //listitem.Add(new CompanyDataTable
            //{
            //    Ticker = "6166",
            //    Name = "小倒暗",
            //    Date = "2010Q4",
            //    WorkingCapital = 12213132,
            //    RetainedEarning = 13123123,
            //    EBIT = 123123123,
            //    TotalAsset = 123123123,
            //    TotalLiability = 123123123,
            //    Equity = 123123123,
            //    GrossSales = 123123123,
            //    StockPrice = 123123123,
            //    MarketValue = 123123123123,
            //    CompanyStock = 5000,
            //    ZValue = "123123123123"
            //});
            //listitem.Add(new CompanyDataTable
            //{
            //    Ticker = "6166",
            //    Name = "小倒暗",
            //    Date = "2009Q4",
            //    WorkingCapital = 12213132,
            //    RetainedEarning = 13123123,
            //    EBIT = 123123123,
            //    TotalAsset = 123123123,
            //    TotalLiability = 123123123,
            //    Equity = 123123123,
            //    GrossSales = 123123123,
            //    StockPrice = 123123123,
            //    MarketValue = 123123123123,
            //    CompanyStock = 5000,
            //    ZValue = "123123123123"
            //});
            //listitem.Add(new CompanyDataTable
            //{
            //    Ticker = "6166",
            //    Name = "小倒暗",
            //    Date = "2008Q4",
            //    WorkingCapital = 12213132,
            //    RetainedEarning = 13123123,
            //    EBIT = 123123123,
            //    TotalAsset = 123123123,
            //    TotalLiability = 123123123,
            //    Equity = 123123123,
            //    GrossSales = 123123123,
            //    StockPrice = 123123123,
            //    MarketValue = 123123123123,
            //    CompanyStock = 5000,
            //    ZValue = "123123123123"
            //});
            //listitem.Add(new CompanyDataTable
            //{
            //    Ticker = "6166",
            //    Name = "小倒暗",
            //    Date = "2007Q4",
            //    WorkingCapital = 12213132,
            //    RetainedEarning = 13123123,
            //    EBIT = 123123123,
            //    TotalAsset = 123123123,
            //    TotalLiability = 123123123,
            //    Equity = 123123123,
            //    GrossSales = 123123123,
            //    StockPrice = 123123123,
            //    MarketValue = 123123123123,
            //    CompanyStock = 5000,
            //    ZValue = "123123123123"
            //});
            //listitem.Add(new CompanyDataTable
            //{
            //    Ticker = "6166",
            //    Name = "小倒暗",
            //    Date = "2006Q4",
            //    WorkingCapital = 12213132,
            //    RetainedEarning = 13123123,
            //    EBIT = 123123123,
            //    TotalAsset = 123123123,
            //    TotalLiability = 123123123,
            //    Equity = 123123123,
            //    GrossSales = 123123123,
            //    StockPrice = 123123123,
            //    MarketValue = 123123123123,
            //    CompanyStock = 5000,
            //    ZValue = "123123123123"
            //});
            //listitem.Add(new CompanyDataTable
            //{
            //    Ticker = "6166",
            //    Name = "小倒暗",
            //    Date = "2005Q4",
            //    WorkingCapital = 12213132,
            //    RetainedEarning = 13123123,
            //    EBIT = 123123123,
            //    TotalAsset = 123123123,
            //    TotalLiability = 123123123,
            //    Equity = 123123123,
            //    GrossSales = 123123123,
            //    StockPrice = 123123123,
            //    MarketValue = 123123123123,
            //    CompanyStock = 5000,
            //    ZValue = "123123123123"
            //});
            //listitem.Add(new CompanyDataTable
            //{
            //    Ticker = "6166",
            //    Name = "小倒暗",
            //    Date = "2004Q4",
            //    WorkingCapital = 12213132,
            //    RetainedEarning = 13123123,
            //    EBIT = 123123123,
            //    TotalAsset = 123123123,
            //    TotalLiability = 123123123,
            //    Equity = 123123123,
            //    GrossSales = 123123123,
            //    StockPrice = 123123123,
            //    MarketValue = 123123123123,
            //    CompanyStock = 5000,
            //    ZValue = "123123123123"
            //});
            //listitem.Add(new CompanyDataTable
            //{
            //    Ticker = "6166",
            //    Name = "小倒暗",
            //    Date = "2003Q4",
            //    WorkingCapital = 12213132,
            //    RetainedEarning = 13123123,
            //    EBIT = 123123123,
            //    TotalAsset = 123123123,
            //    TotalLiability = 123123123,
            //    Equity = 123123123,
            //    GrossSales = 123123123,
            //    StockPrice = 123123123,
            //    MarketValue = 123123123123,
            //    CompanyStock = 5000,
            //    ZValue = "123123123123"
            //});
            //listitem.Add(new CompanyDataTable
            //{
            //    Ticker = "6166",
            //    Name = "小倒暗",
            //    Date = "2002Q4",
            //    WorkingCapital = 12213132,
            //    RetainedEarning = 13123123,
            //    EBIT = 123123123,
            //    TotalAsset = 123123123,
            //    TotalLiability = 123123123,
            //    Equity = 123123123,
            //    GrossSales = 123123123,
            //    StockPrice = 123123123,
            //    MarketValue = 123123123123,
            //    CompanyStock = 5000,
            //    ZValue = "123123123123"
            //});

            var resultList = (from DB in CFDB.CompanyDataTable
                                  //from DB in listitem
                              where DB.Ticker == stockId && Convert.ToInt32(DB.Date.Substring(0, 4)) <= Convert.ToInt32(DateTime.Today.Year)
                              orderby Convert.ToInt32(DB.Date.Substring(0, 4)) descending
                              select DB).Take(10).OrderBy(x=>Convert.ToInt32(x.Date.Substring(0,4)));

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