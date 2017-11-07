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
        public async Task<ActionResult> Index()
        {
            var result= await GetHis();
            var last = result.Last();

            return View();
        }

        public async Task<IList<Candle>> GetHis()
        {

            IList<Candle> result = await Yahoo.GetHistoricalAsync("6166.TW", new DateTime(2017, 11, 6),
                                        new DateTime(2017, 11, 6), Period.Daily);
            return result;
        }

        [HttpPost]
        public ActionResult Index(string stockId)
        {
            order.ZvalueAnalysis(stockId);

            return View();
        }

        public ActionResult Analysis(string group)
        {
            List<CompanyFinancial> rst =  order.GetCompanyFinancialList(group);

            return View(rst);
        }


    }
}