﻿using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YahooFinanceApi;
using CompanyFinancialAnalysis.Models;

namespace CompanyFinancialAnalysis.Models
{
    public class Program
    {
        /// <summary>
        /// 取得ifrs前的財務報表
        /// </summary>
        /// <param name="stockId">股票代號(Ex:6616)</param>
        /// <param name="date">年季別(Ex:2017Q4)</param>
        private static Company GetCompanyFinanceStatBeforeIfrs(string stockId, string date)
        {
            try
            {
                Company company = new Company();

                //資產負債表
                string apiBSUrlIfrs = "http://mops.twse.com.tw/mops/web/ajax_t05st29?TYPEK=all&step=1&firstin=1&off=1&queryName=co_id&isnew=false";

                //損益表
                string apiISUrlIfrs = "http://mops.twse.com.tw/mops/web/ajax_t05st30_c?TYPEK=all&step=1&firstin=1&off=1&queryName=co_id&t05st29_c_ifrs=N&t05st31_c_ifrs=N&isnew=false";

                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc;
                //HtmlNodeCollection nodes;

                apiBSUrlIfrs += "&co_id=" + stockId + "&year=" + (Convert.ToInt32(date.Substring(0, 4)) - 1911).ToString();

                apiISUrlIfrs += "&co_id=" + stockId + "&year=" + (Convert.ToInt32(date.Substring(0, 4)) - 1911).ToString();

                //資產負債表
                doc = web.Load(apiBSUrlIfrs);

                //項目名稱
                var nodeHead = doc.DocumentNode.SelectNodes("//tr[@class!=\"reportCont\"]/td[1]");
                //取得第四季資料
                string dataPath = "//tr[@class!=\"reportCont\"]/td[5]";
                //季別資料
                var nodeData = doc.DocumentNode.SelectNodes(dataPath);

                Dictionary<string, string> BSData = new Dictionary<string, string>();

                var numbersAndWords = nodeHead.Zip(nodeData, (w, n) => new { Word = w, Number = n });
                foreach (var nw in numbersAndWords)
                {
                    BSData.Add(nw.Word.InnerText, nw.Number.InnerText);

                    //Console.WriteLine(nw.Word.InnerText + ":" + nw.Number.InnerText);
                }

                //損益表
                doc = web.Load(apiISUrlIfrs);

                //項目名稱
                nodeHead = doc.DocumentNode.SelectNodes("//tr[@class!=\"reportCont\"]/td[1]");
                //path設定為日期所指定的季別
                dataPath = "//tr[@class!=\"reportCont\"]/td[5]";
                //季別資料
                nodeData = doc.DocumentNode.SelectNodes(dataPath);


                Dictionary<string, string> ISData = new Dictionary<string, string>();

                numbersAndWords = nodeHead.Zip(nodeData, (w, n) => new { Word = w, Number = n });
                foreach (var nw in numbersAndWords)
                {
                    ISData.Add(nw.Word.InnerText, nw.Number.InnerText);
                    //Console.WriteLine(nw.Word.InnerText + ":" + nw.Number.InnerText);
                }

                //var aaa = Convert.ToInt32(BSData["流動資產"]);

                company.Ticker = stockId;

                company.Name = GetStockName(stockId);

                company.Date = date;

                company.WorkingCapital = StringToInt(BSData["流動資產"]) - StringToInt(BSData["流動負債"]);

                company.RetainedEarning = StringToInt(BSData["保留盈餘"]);

                company.EBIT = StringToInt(ISData["營業收入"]) - StringToInt(ISData["營業成本"]) - StringToInt(ISData["營業費用"]);

                //有些是 資產總額 or 資產總計
                //company.TotalAsset = StringToInt(BSData["資產總計"]);
                if (BSData.ContainsKey("資產總額"))
                {
                    company.TotalAsset = StringToInt(BSData["資產總額"]);
                }
                else if (BSData.ContainsKey("資產總計"))
                {
                    company.TotalAsset = StringToInt(BSData["資產總計"]);
                }
                else
                {
                    company.TotalAsset = StringToInt("0");
                }

                //有些是 負債總額 or 負債總計
                //company.TotalLiability = StringToInt(BSData["負債總計"]);
                if (BSData.ContainsKey("負債總額"))
                {
                    company.TotalLiability = StringToInt(BSData["負債總額"]);
                }
                else if (BSData.ContainsKey("負債總計"))
                {
                    company.TotalLiability = StringToInt(BSData["負債總計"]);
                }
                else
                {
                    company.TotalLiability = StringToInt("0");
                }

                //股東權益
                if (BSData.ContainsKey("權益總計"))
                {
                    company.Equity = StringToInt(BSData["權益總計"]);
                }
                else if (BSData.ContainsKey("股東權益總計"))
                {
                    company.Equity = StringToInt(BSData["股東權益總計"]);
                }
                else
                {
                    company.Equity = StringToInt("0");
                }

                company.GrossSales = StringToInt(ISData["營業收入"]);

                company.StockPrice = Convert.ToDouble(GetStockPriceFromYah(stockId, date));

                company.MarketValue = StringToInt(BSData["股本"]) / 10 * company.StockPrice;

                company.CompanyStock = StringToInt(BSData["股本"]);

                company.ZValue = GetZValue(company.WorkingCapital, company.RetainedEarning, company.EBIT, company.MarketValue, company.GrossSales, company.TotalAsset, company.TotalLiability);

                return company;
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal static WebRequest CreateWebRequest()
        {
            WebRequest myWebRequest = WebRequest.Create("http://mops.twse.com.tw/server-java/t164sb01?step=1&CO_ID=2349&SYEAR=2016&SSEASON=4&REPORT_ID=C");
            //string[] strByPass = new string[] { "192.1.1.1", "192.2.2.2" };
            myWebRequest.Timeout = 10000;
            //myWebRequest.Credentials = new NetworkCredential("Name", "PWD", "Domain Name");
            //myWebRequest.Proxy = new WebProxy(new Uri("http://proxy.hinet.net:8080"));
            //myWebRequest.Proxy.Credentials = new NetworkCredential("Name", "PWD", "Domain Name");
            return myWebRequest;
        }

        /// <summary>
        /// 季的股價資訊 從證交所網站
        /// </summary>
        /// <param name="stockId">股票代號</param>
        /// <param name="date">年季別yyyyQx(x:1~4)</param>
        /// <returns></returns>
        internal static string GetStockPrice(string stockId, string date)
        {
            try
            {
                using (MyWebClient wc = new MyWebClient())
                {
                    //不要在短時間請求網頁
                    Thread.Sleep(1);

                    string url = "http://www.twse.com.tw/exchangeReport/STOCK_DAY?response=.csv";

                    url += "&date=" + date.Substring(0, 4) + ConvertSeasonToDate(date.Substring(4));

                    url += "&stockNo=" + stockId.ToString();

                    wc.Encoding = Encoding.UTF8;

                    var jsonStr = wc.DownloadString(url);

                    if (jsonStr == "")
                    {
                        //沒有連到網頁
                        return "0";
                    }

                    JObject stockDataO = JObject.Parse(jsonStr);

                    //沒有撈到資料(網站更新時會沒有資料)
                    if (stockDataO["stat"].ToString() != "OK")
                    {
                        return "0";
                    }

                    return stockDataO["data"].Last()[6].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return "0";

                throw;
            }
        }

        internal static string GetStockPriceFromYah(string stockId, string date)
        {
            try
            {
                var ticker = stockId + ".TW";

                var start_date = new DateTime(Convert.ToInt32(date.Substring(0, 4)), Convert.ToInt32(ConvertSeasonToDate(date.Substring(4)).Substring(0, 2)), 1);

                var str_date = (date.Substring(0, 4) + ConvertSeasonToDate(date.Substring(4)).Substring(0, 2) + 1.ToString()).ToDatetime();

                var end_date = new DateTime(Convert.ToInt32(date.Substring(0, 4)), Convert.ToInt32(ConvertSeasonToDate(date.Substring(4)).Substring(0, 2)), Convert.ToInt32(ConvertSeasonToDate(date.Substring(4)).Substring(2)));

                var ed_date = (date.Substring(0, 4) + ConvertSeasonToDate(date.Substring(4))).ToDatetime();

                var results = Task.Run(() => Yahoo.GetHistoricalAsync(ticker, start_date, end_date, Period.Daily));

                var last = results.Result.Last();

                if (last != null)
                {
                    return last.Close.ToString();
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //沒有使用
        public async Task<string> GetStockPriceFromYahAsync(string stockId, string date)
        {
            try
            {
                var ticker = stockId + ".TW";

                var start_date = new DateTime(Convert.ToInt32(date.Substring(0, 4)), Convert.ToInt32(ConvertSeasonToDate(date.Substring(4)).Substring(0, 2)), 1);

                var str_date = (date.Substring(0, 4) + ConvertSeasonToDate(date.Substring(4)).Substring(0, 2) + 1.ToString()).ToDatetime();

                var end_date = new DateTime(Convert.ToInt32(date.Substring(0, 4)), Convert.ToInt32(ConvertSeasonToDate(date.Substring(4)).Substring(0, 2)), Convert.ToInt32(ConvertSeasonToDate(date.Substring(4)).Substring(2)));

                var ed_date = (date.Substring(0, 4) + ConvertSeasonToDate(date.Substring(4))).ToDatetime();

                var results = await Yahoo.GetHistoricalAsync(ticker, start_date, end_date, Period.Daily);

                var last = results.Last();

                if (last != null)
                {
                    return last.Close.ToString();
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 取公司財報資料
        /// </summary>
        /// <param name="stockId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        internal static Company GetCompanyFinanceStat(string stockId, string date)

        {
            try
            {
                Company company = new Company();

                //修改
                //資產負債表
                string apiBSUrlIfrs;

                //損益表
                string apiISUrlIfrs;

                string bsHeadPath;

                string bsDataPath;

                string isHeadPath;

                string isDataPath;

                if (Convert.ToInt32(date.Substring(0,4)) > 2012)
                {
                    //資產負債表
                    apiBSUrlIfrs = "http://mops.twse.com.tw/mops/web/ajax_t163sb16?TYPEK=all&step=1&firstin=1&off=1&queryName=co_id&t05st29_c_ifrs=N&t05st30_c_ifrs=N&isnew=false";

                    //損益表
                    apiISUrlIfrs = "http://mops.twse.com.tw/mops/web/ajax_t163sb15?TYPEK=all&step=1&firstin=1&off=1&queryName=co_id&t05st29_c_ifrs=N&t05st30_c_ifrs=N&isnew=false";

                    apiBSUrlIfrs += "&co_id=" + stockId + "&year=" + (Convert.ToInt32(date.Substring(0, 4)) - 1911).ToString();

                    apiISUrlIfrs += "&co_id=" + stockId + "&year=" + (Convert.ToInt32(date.Substring(0, 4)) - 1911).ToString();

                    bsHeadPath = "//tr/th[@class!=\"tblHead\"]";

                    bsDataPath = "//td[@class!=\"reportCont\"][" + date.Substring(date.Length - 1) + "]";

                    isHeadPath = "//tr/th[@class!=\"tblHead\"]";

                    isDataPath = "//td[@class!=\"reportCont\"][" + date.Substring(date.Length - 1) + "]";

                }
                else
                {
                    //資產負債表
                    apiBSUrlIfrs = "http://mops.twse.com.tw/mops/web/ajax_t05st29?TYPEK=all&step=1&firstin=1&off=1&queryName=co_id&isnew=false";

                    //損益表
                    apiISUrlIfrs = "http://mops.twse.com.tw/mops/web/ajax_t05st30_c?TYPEK=all&step=1&firstin=1&off=1&queryName=co_id&t05st29_c_ifrs=N&t05st31_c_ifrs=N&isnew=false";

                    apiBSUrlIfrs += "&co_id=" + stockId + "&year=" + (Convert.ToInt32(date.Substring(0, 4)) - 1911).ToString();

                    apiISUrlIfrs += "&co_id=" + stockId + "&year=" + (Convert.ToInt32(date.Substring(0, 4)) - 1911).ToString();

                    bsHeadPath = "//tr[@class!=\"reportCont\"]/td[1]";

                    bsDataPath = "//tr[@class!=\"reportCont\"]/td[5]";

                    isHeadPath = "//tr[@class!=\"reportCont\"]/td[1]";

                    isDataPath = "//tr[@class!=\"reportCont\"]/td[5]";
                }
                
                ////資產負債表
                //string apiBSUrlIfrs = "http://mops.twse.com.tw/mops/web/ajax_t163sb16?TYPEK=all&step=1&firstin=1&off=1&queryName=co_id&t05st29_c_ifrs=N&t05st30_c_ifrs=N&isnew=false";

                ////損益表
                //string apiISUrlIfrs = "http://mops.twse.com.tw/mops/web/ajax_t163sb15?TYPEK=all&step=1&firstin=1&off=1&queryName=co_id&t05st29_c_ifrs=N&t05st30_c_ifrs=N&isnew=false";

                HtmlWeb web = new HtmlWeb();
                WebClient webC = new WebClient();
                //webC.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.202 Safari/535.1";
                //webC.Headers["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                HtmlDocument doc = new HtmlDocument();
                //HtmlNodeCollection nodes;

                //apiBSUrlIfrs += "&co_id=" + stockId + "&year=" + (Convert.ToInt32(date.Substring(0, 4)) - 1911).ToString();

                //apiISUrlIfrs += "&co_id=" + stockId + "&year=" + (Convert.ToInt32(date.Substring(0, 4)) - 1911).ToString();

                //資產負債表
                doc = web.Load(apiBSUrlIfrs);

                //doc.Load(webC.OpenRead(apiBSUrlIfrs));


                //項目名稱
                //string headPath = "//tr/th[@class!=\"tblHead\"]";

                //var nodeHead = doc.DocumentNode.SelectNodes("//tr/th[@class!=\"tblHead\"]");
                var nodeHead = doc.DocumentNode.SelectNodes(bsHeadPath);

                //path設定為日期所指定的季別
                //string dataPath = "//td[@class!=\"reportCont\"][" + date.Substring(date.Length - 1) + "]";
                //季別資料
                var nodeData = doc.DocumentNode.SelectNodes(bsDataPath);

                Dictionary<string, int> BSData = new Dictionary<string, int>();

                var numbersAndWords = nodeHead.Zip(nodeData, (w, n) => new { Word = w, Number = n });
                foreach (var nw in numbersAndWords)
                {
                    BSData.Add(nw.Word.InnerText, StringToInt(nw.Number.InnerText));

                    //Console.WriteLine(nw.Word.InnerText + ":" + nw.Number.InnerText);
                }

                //損益表
                doc = web.Load(apiISUrlIfrs);

                //項目名稱
                //nodeHead = doc.DocumentNode.SelectNodes("//tr/th[@class!=\"tblHead\"]");
                nodeHead = doc.DocumentNode.SelectNodes(isHeadPath);

                //path設定為日期所指定的季別
                //dataPath = "//td[@class!=\"reportCont\"][" + date.Substring(date.Length - 1) + "]";


                //季別資料
                //nodeData = doc.DocumentNode.SelectNodes(dataPath);
                nodeData = doc.DocumentNode.SelectNodes(isDataPath);


                Dictionary<string, int> ISData = new Dictionary<string, int>();

                numbersAndWords = nodeHead.Zip(nodeData, (w, n) => new { Word = w, Number = n });
                foreach (var nw in numbersAndWords)
                {
                    ISData.Add(nw.Word.InnerText, StringToInt(nw.Number.InnerText));
                    //Console.WriteLine(nw.Word.InnerText + ":" + nw.Number.InnerText);
                }

                //var aaa = Convert.ToInt32(BSData["流動資產"]);

                company.Ticker = stockId;

                company.Name = GetStockName(stockId);

                company.Date = date;

                company.compBD = TransBSDicToClass(BSData);

                company.compCD = TransISDicToClass(ISData);

                //company.WorkingCapital = BSData["流動資產"] - BSData["流動負債"];

                company.WorkingCapital = company.compBD.CurrentAssets - company.compBD.Currentliabilities;

                //company.RetainedEarning = BSData["保留盈餘"];

                company.RetainedEarning = company.compBD.RetainedEarning;

                //company.EBIT = (ISData["營業收入"]) - (ISData["營業成本"]) - (ISData["營業費用"]);

                company.EBIT = company.compCD.OperatingRevenue - company.compCD.CostOfGoodsSold - company.compCD.OperatingExpenses;

                //有些是 資產總額 or 資產總計
                //company.TotalAsset = StringToInt(BSData["資產總計"]);
                //if (BSData.ContainsKey("資產總額"))
                //{
                //    company.TotalAsset = (BSData["資產總額"]);
                //}
                //else if (BSData.ContainsKey("資產總計"))
                //{
                //    company.TotalAsset = (BSData["資產總計"]);
                //}
                //else
                //{
                //    company.TotalAsset = StringToInt("0");
                //}

                company.TotalAsset = company.compBD.TotalAsset;

                //有些是 負債總額 or 負債總計
                //company.TotalLiability = StringToInt(BSData["負債總計"]);
                //if (BSData.ContainsKey("負債總額"))
                //{
                //    company.TotalLiability = (BSData["負債總額"]);
                //}
                //else if (BSData.ContainsKey("負債總計"))
                //{
                //    company.TotalLiability = (BSData["負債總計"]);
                //}
                //else
                //{
                //    company.TotalLiability = StringToInt("0");
                //}

                company.TotalLiability = company.compBD.TotalLiability;

                //股東權益
                //if (BSData.ContainsKey("權益總計"))
                //{
                //    company.Equity = (BSData["權益總計"]);
                //}
                //else if (BSData.ContainsKey("股東權益總計"))
                //{
                //    company.Equity = (BSData["股東權益總計"]);
                //}
                //else
                //{
                //    company.Equity = StringToInt("0");
                //}
                company.Equity = company.compBD.Equity;

                company.GrossSales = company.compCD.OperatingRevenue;

                company.StockPrice = Convert.ToDouble(GetStockPriceFromYah(stockId, date));

                //company.MarketValue = Math.Round(((BSData["股本"]) / 10 * company.StockPrice),5);

                company.MarketValue = Math.Round((company.compBD.CompanyStock / 10 * company.StockPrice), 5);

                //company.CompanyStock = (BSData["股本"]);

                company.CompanyStock = company.compBD.CompanyStock;

                company.ZValue = GetZValue(company.WorkingCapital, company.RetainedEarning, company.EBIT, company.MarketValue, company.GrossSales, company.TotalAsset, company.TotalLiability);

                //Console.WriteLine(company);
                return company;
                //Console.WriteLine(string.Format("stock id:{0} season:{1} 股價:", stockId, date) + GetStockPrice(stockId, date));
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 將資產負債表Dictionary轉成Class
        /// </summary>
        /// <param name="bsDic"></param>
        /// <returns></returns>
        public static CompanyBalanceData TransBSDicToClass(Dictionary<string,int> bsDic)
        {
            return new CompanyBalanceData(bsDic);
        }

        public static CompanyCashData TransISDicToClass(Dictionary<string,int> isDic)
        {
            return new CompanyCashData(isDic);
        }

        /// <summary>
        /// 計算 z value
        /// </summary>
        /// <param name="wc">營運資金</param>
        /// <param name="re">保留盈餘</param>
        /// <param name="ebit">營業利益</param>
        /// <param name="mv">市值</param>
        /// <param name="gs">營業收入</param>
        /// <param name="ta">總資產</param>
        /// <param name="tl">總負債</param>
        /// <returns></returns>
        internal static double GetZValue(double wc, double re, double ebit, double mv, double gs, double ta, double tl)
        {
            try
            {
                return 1.2 * wc / ta + 1.4 * re / ta + 3.3 * ebit / ta + 0.6 * mv / tl + gs / ta;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 將季別轉換成月份
        /// </summary>
        /// <param name="season"></param>
        /// <returns></returns>
        internal static string ConvertSeasonToDate(string season)
        {
            try
            {
                switch (season)
                {
                    case "Q1":
                        return "0331";
                    case "Q2":
                        return "0630";
                    case "Q3":
                        return "0930";
                    case "Q4":
                        return "1231";
                    default:
                        return "not valid season";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// #,###,### to #######
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        internal static int StringToInt(string num)
        {
            try
            {
                if (num == "" || num == "-")
                {
                    return 0;
                }

                string strNum = "";
                foreach (var n in num.Split(','))
                {
                    strNum += n;
                }

                int rnum;

                return Int32.TryParse(strNum, out rnum) ? rnum : Convert.ToInt32(Double.Parse(strNum));

                //return Convert.ToInt32(strNum);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 回傳十年間的公司財報資料
        /// </summary>
        /// <param name="stockId">股市代號(Ex:6166)</param>
        /// <returns>回傳公司十年的Z值</returns>
        public List<Company> GetCompanyTenStatDataLst(string stockId)
        {
            try
            {

                var season = LastStatDate();

                Company temp = new Company();

                List<Company> compTenYrDataLst = new List<Company>();

                temp = GetCompanyFinanceStat(stockId, season);

                compTenYrDataLst.Add(temp);

                for (int i = 1; i < 10; i++)
                {
                    if ((Convert.ToInt32(season.Substring(0, 4)) - i) < 2013)
                    {
                        //temp = GetCompanyFinanceStatBeforeIfrs(stockId, (Convert.ToInt32(season.Substring(0, 4)) - i).ToString() + "Q4");
                        temp = GetCompanyFinanceStat(stockId, (Convert.ToInt32(season.Substring(0, 4)) - i).ToString() + "Q4");
                    }
                    else
                    {
                        temp = GetCompanyFinanceStat(stockId, (Convert.ToInt32(season.Substring(0, 4)) - i).ToString() + "Q4");
                        //temp = GetCompanyFinanceStatBeforeIfrs(stockId, (Convert.ToInt32(season.Substring(0, 4)) - i).ToString() + "Q4");
                    }

                    compTenYrDataLst.Add(temp);
                }

                return compTenYrDataLst;
            }
            catch (Exception)
            {
                Console.WriteLine("GetCompanyTenStatDataList Error");

                throw;
            }

        }

        /// <summary>
        /// 回傳最新的財報資料年季別
        /// </summary>
        /// <returns>Ex:2017Q4</returns>
        internal static string LastStatDate()
        {
            try
            {
                //第一季財報出來月份
                DateTime fstSeasonStatDt = Convert.ToDateTime(DateTime.Now.Year.ToString() + "/5/15");

                DateTime secSeasonStatDt = Convert.ToDateTime(DateTime.Now.Year.ToString() + "/8/14");

                DateTime thdSeasonStatDt = Convert.ToDateTime(DateTime.Now.Year.ToString() + "/11/14");

                DateTime lstSeasonStatDt = Convert.ToDateTime(DateTime.Now.Year.ToString() + "/3/31");

                if (DateTime.Compare(DateTime.Now, lstSeasonStatDt) <= 0)
                {
                    return DateTime.Now.AddYears(-1).Year.ToString() + "Q3";
                }
                else if (DateTime.Compare(DateTime.Now, lstSeasonStatDt) > 0 && DateTime.Compare(DateTime.Now, fstSeasonStatDt) <= 0)
                {
                    return DateTime.Now.AddYears(-1).Year.ToString() + "Q4";
                }
                else if (DateTime.Compare(DateTime.Now, fstSeasonStatDt) > 0 && DateTime.Compare(DateTime.Now, secSeasonStatDt) <= 0)
                {
                    return DateTime.Now.Year.ToString() + "Q1";
                }
                else if (DateTime.Compare(DateTime.Now, secSeasonStatDt) > 0 && DateTime.Compare(DateTime.Now, thdSeasonStatDt) <= 0)
                {
                    return DateTime.Now.Year.ToString() + "Q2";
                }
                else
                {
                    return DateTime.Now.Year.ToString() + "Q3";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 查看是否有此檔股票代號
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        public bool CheckValidStockId(string stockId)
        {
            try
            {
                if (stockId == "")
                {
                    return false;
                }

                string url = "http://isin.twse.com.tw/isin/C_public.jsp?strMode=2";

                WebClient webC = new WebClient();

                HtmlWeb web = new HtmlWeb();

                HtmlDocument doc = new HtmlDocument();

                //doc = web.Load(url);

                doc.Load(webC.OpenRead(url), Encoding.GetEncoding("big5"));
                //doc.Load(web.OpenRead(url));

                //WebBrowser htmlbrowser = new WebBrowser();

                //var node = doc.DocumentNode.SelectNodes("//tbody/tr[position()>2]/td[1]");
                //doc.ParsedText = EncodToBig5(doc.ParsedText);

                var nodes = doc.DocumentNode.SelectNodes("//tr[position()>2]/td[1]");

                if (nodes == null)
                {
                    return false;
                }

                var stockCnt = nodes.ToList().Where(s => s.InnerText.Split('　').First().Length == 4)
                                            .Where(s => s.InnerText.Split('　').First() == stockId)
                                            .Count();

                //var ticker = nodes.ToList().Where(s => s.InnerText.Split('　').First().Length == 4)
                //                                    .Where(s => s.InnerText.Split('　').First() == stockId)
                //                                    .First();

                var stockZhName = nodes.ToList().Where(s => s.InnerText.Split('　').First().Length == 4)
                                            .Where(s => s.InnerText.Split('　').First() == stockId)
                                            .Select(s => s.InnerText.Split('　').Last());

                if (stockCnt != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 回傳股票的公司名稱
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        internal static string GetStockName(string stockId)
        {
            try
            {
                if (stockId == "")
                {
                    return "請輸入正確股票代號";
                }

                string url = "http://isin.twse.com.tw/isin/C_public.jsp?strMode=2";

                WebClient webC = new WebClient();

                HtmlWeb web = new HtmlWeb();

                HtmlDocument doc = new HtmlDocument();

                //doc = web.Load(url);

                Thread.Sleep(1);

                doc.Load(webC.OpenRead(url), Encoding.GetEncoding("big5"));
                //doc.Load(web.OpenRead(url));

                //WebBrowser htmlbrowser = new WebBrowser();

                //var node = doc.DocumentNode.SelectNodes("//tbody/tr[position()>2]/td[1]");
                //doc.ParsedText = EncodToBig5(doc.ParsedText);

                var nodes = doc.DocumentNode.SelectNodes("//tr[position()>2]/td[1]");

                if (nodes == null)
                {
                    return "沒有資料";
                }

                var stockCnt = nodes.ToList().Where(s => s.InnerText.Split('　').First().Length == 4)
                                            .Where(s => s.InnerText.Split('　').First() == stockId)
                                            .Count();

                //var ticker = nodes.ToList().Where(s => s.InnerText.Split('　').First().Length == 4)
                //                                    .Where(s => s.InnerText.Split('　').First() == stockId)
                //                                    .First();

                var stockZhName = nodes.ToList().Where(s => s.InnerText.Split('　').First().Length == 4)
                                            .Where(s => s.InnerText.Split('　').First() == stockId)
                                            .Select(s => s.InnerText.Split('　').Last());

                if (stockCnt != 0)
                {
                    return stockZhName.First();
                }
                else
                {
                    return "沒有資料";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 顯示zvalue
        /// </summary>
        /// <param name="cmpLst"></param>
        internal static void ShowCompLstZValue(List<Company> cmpLst)
        {
            try
            {

                Console.WriteLine(string.Format("公司名稱:{0} 股票代號:{1}", cmpLst.First().Name, cmpLst.First().Ticker));

                foreach (var c in cmpLst)
                {
                    Console.Write(string.Format("{0} ", c.Date));
                }
                Console.WriteLine();

                foreach (var c in cmpLst)
                {
                    Console.Write(string.Format("{0} ", Math.Round(c.ZValue, 4)));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
