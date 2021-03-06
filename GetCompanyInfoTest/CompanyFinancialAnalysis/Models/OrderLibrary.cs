﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CompanyFinancialAnalysis.ViewModel;
using CompanyFinancialAnalysis.Service;
using CompanyFinancialAnalysis.Services;

namespace CompanyFinancialAnalysis.Models
{

    public class OrderLibrary
    {
        Program GetProgram = new Program();

        /// <summary>
        /// 將前十年公司財務包進 CompanyFinancial 
        /// </summary>
        /// <param name="stockId">股票編號(EX:3008)</param>
        public void ZvalueAnalysis(string stockId)
        {
            CompanyFinancial companyfinancial = new CompanyFinancial();

            if (GetProgram.CheckValidStockId(stockId))
            {
                companyfinancial.stockId = stockId;
            }

            companyfinancial.CFA_Information = GetProgram.GetCompanyTenStatDataLst(stockId);

            DBService db = new DBService();

            db.SaveCompanyList(companyfinancial.CFA_Information);

        }



        /// <summary>
        /// 回傳ViewModelList CompanyFinancial
        /// </summary>
        /// <param name="co_id_group">公司名稱List</param>
        /// <returns></returns>
        public List<CompanyFinancial> GetCompanyFinancialList(string co_group)
        {
            var co_id_group = new List<string>();
            var co_name_group = new List<string>();
            CFDBServices Test = new CFDBServices();
            if (String.IsNullOrEmpty(co_group)&&co_group!="1"&&co_group!="2")
            {
                return new List<CompanyFinancial>();
            }
            else if (co_group=="1")
            {
                co_id_group.Add("3008");
                co_name_group.Add("大立光");
                co_id_group.Add("3406");
                co_name_group.Add("玉晶光");
                co_id_group.Add("6209");
                co_name_group.Add("今國光");
            }
            else
            {
                co_id_group.Add("2308");
                co_name_group.Add("台達電");
                co_id_group.Add("2395");
                co_name_group.Add("研華");
                co_id_group.Add("6166");
                co_name_group.Add("凌華");
            }
            
            var rst = new List<CompanyFinancial>();



            int i = 0;
            foreach (var item in co_id_group)
            {
                var addf = new CompanyFinancial();
                addf.stockId = item;
                addf.stockName = co_name_group[i];
                addf.CFA_Information = Test.GetCompanyTableTenData(item);
                rst.Add(addf);
                i+=1;
            }
            

            return rst;
            
        }


    }
}