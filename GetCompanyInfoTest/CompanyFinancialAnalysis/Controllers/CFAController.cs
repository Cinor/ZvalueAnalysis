using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompanyFinancialAnalysis.Models;
using CompanyFinancialAnalysis.ViewModel;
using YahooFinanceApi;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyFinancialAnalysis.Controllers
{
    public class CFAController : Controller
    {
        OrderLibrary order = new OrderLibrary();

        // GET: Companyfinancial
        //public async Task<ActionResult> Index()
        public ActionResult Index()
        {
            //var result= await GetHisAsync();
            //var last = result.Last();

            return View();
        }

        public async Task<string> GetHisAsync(string id)
        {
            Program p = new Program();
            string result = await p.GetStockPriceFromYahAsync(id, "2016Q4");
            return result;
        }

        [HttpPost]
        public ActionResult Index(string stockId)
        {
            order.ZvalueAnalysis(stockId);

            //var r = await GetHisAsync(stockId);

            return View();
        }

        public ActionResult Analysis(string group)
        {
            List<CompanyFinancial> rst =  order.GetCompanyFinancialList(group);

            return View(rst);
        }


    }
}