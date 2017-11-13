using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyFinancialAnalysis.Models
{
    /// <summary>
    /// 現金流量表
    /// </summary>
    public class CompanyCashData:Company
    {
        public CompanyCashData() { }

        public CompanyCashData(Dictionary<string,int> isDic)
        {
            OperatingRevenue = isDic.ContainsKey("營業收入") ? isDic["營業收入"] : 0;
            CostOfGoodsSold = isDic.ContainsKey("營業成本") ? isDic["營業成本"] : 0;
            Grossprofit = isDic.ContainsKey("營業毛利（毛損）") ? isDic["營業毛利（毛損）"] :
                          isDic.ContainsKey("營業毛利(毛損)") ? isDic["營業毛利(毛損)"] : 0;
            RealizedGainFromInterAffiliateAccounts = isDic.ContainsKey("聯屬公司間未實現利益") ? isDic["聯屬公司間未實現利益"] : 0;
            UnrealizedGainFromInterAffiliateAccounts = isDic.ContainsKey("聯署公司間已實現利益") ? isDic["聯署公司間已實現利益"] : 0;
            OperatingExpenses = isDic.ContainsKey("營業費用") ? isDic["營業費用"] : 0;
            OperatingIncome= isDic.ContainsKey("營業淨利(淨損)") ? isDic["營業淨利(淨損)"] :
                             isDic.ContainsKey("營業利益（損失）") ? isDic["營業利益（損失）"] : 0;
            NonOperatingIncome= isDic.ContainsKey("營業外收入及利益") ? isDic["營業外收入及利益"] : 0;
            NonOperatingExpenses= isDic.ContainsKey("營業外費用及損失") ? isDic["營業外費用及損失"] : 0;
            IncomeBeforeTaxFromContinuingOperations= isDic.ContainsKey("繼續營業單位稅前淨利(淨損)") ? isDic["繼續營業單位稅前淨利(淨損)"] :
                                                     isDic.ContainsKey("繼續營業單位本期淨利（淨損）") ? isDic["繼續營業單位本期淨利（淨損）"] : 0;
            IncomeTaxExpense = isDic.ContainsKey("所得稅費用（利益）") ? isDic["所得稅費用（利益）"] : 0;
            IncomeFromContinuingOperations = isDic.ContainsKey("繼續營業單位淨利(淨損)") ? isDic["繼續營業單位淨利(淨損)"] :
                                             isDic.ContainsKey("繼續營業單位本期淨利（淨損）") ? isDic["繼續營業單位本期淨利（淨損）"] : 0;
            IncomeOnDiscontinuedOperations = isDic.ContainsKey("停業單位損益") ? isDic["停業單位損益"] : 0;
            ExtraordinaryItems= isDic.ContainsKey("非常損益") ? isDic["非常損益"] : 0;
            MulativeEffectOfChangeInAccountingPrinciple= isDic.ContainsKey("會計原則變動累積影響數") ? isDic["會計原則變動累積影響數"] : 0;
            NetIncome= isDic.ContainsKey("本期淨利（淨損）") ? isDic["本期淨利（淨損）"] : 0;
            EarningPerShare= isDic.ContainsKey("基本每股盈餘") ? isDic["基本每股盈餘"] :
                             isDic.ContainsKey("基本每股盈餘（元）") ? isDic["基本每股盈餘（元）"] : 0;
        }

        /// <summary>
        /// 營業收入
        /// </summary>
        public double OperatingRevenue { get; set; }
        /// <summary>
        /// 營業成本
        /// </summary>
        public double CostOfGoodsSold { get; set; }
        /// <summary>
        /// 營業毛利
        /// </summary>
        public double Grossprofit { get; set; }
        /// <summary>
        /// 聯屬公司間未實現利益
        /// </summary>
        public double RealizedGainFromInterAffiliateAccounts { get; set; }
        /// <summary>
        /// 聯屬公司間已實現利益
        /// </summary>
        public double UnrealizedGainFromInterAffiliateAccounts { get; set; }
        /// <summary>
        /// 營業費用
        /// </summary>
        public double OperatingExpenses { get; set; }
        /// <summary>
        /// 營業淨利
        /// </summary>
        public double OperatingIncome { get; set; }
        /// <summary>
        /// 營業外收入及利益
        /// </summary>
        public double NonOperatingIncome { get; set; }
        /// <summary>
        /// 營業外費用及損失
        /// </summary>
        public double NonOperatingExpenses { get; set; }
        /// <summary>
        /// 繼續營業單位稅前淨利
        /// </summary>
        public double IncomeBeforeTaxFromContinuingOperations { get; set; }
        /// <summary>
        /// 所得稅費用
        /// </summary>
        public double IncomeTaxExpense { get; set; }
        /// <summary>
        /// 繼續營業單位淨利
        /// </summary>
        public double IncomeFromContinuingOperations { get; set; }
        /// <summary>
        /// 停業單位損益
        /// </summary>
        public double IncomeOnDiscontinuedOperations { get; set; }
        /// <summary>
        /// 非常損益
        /// </summary>
        public double ExtraordinaryItems { get; set; }
        /// <summary>
        /// 會計原則變動累計影響數
        /// </summary>
        public double MulativeEffectOfChangeInAccountingPrinciple { get; set; }
        /// <summary>
        /// 本期淨利
        /// </summary>
        public double NetIncome { get; set; }
        /// <summary>
        /// 基本每股盈餘
        /// </summary>
        public double EarningPerShare { get; set; }
    }
}