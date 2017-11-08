//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CompanyFinancialAnalysis.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class IncomeStatement
    {
        public int Id { get; set; }
        public string Ticker { get; set; }
        public string Date { get; set; }
        public Nullable<double> OperatingRevenue { get; set; }
        public Nullable<double> CostOfGoodsSold { get; set; }
        public Nullable<double> Grossprofit { get; set; }
        public Nullable<double> RealizedGainFromInterAffiliateAccounts { get; set; }
        public Nullable<double> UnrealizedGainFromInterAffiliateAccounts { get; set; }
        public Nullable<double> OperatingExpenses { get; set; }
        public Nullable<double> OperatingIncome { get; set; }
        public Nullable<double> NonOperatingIncome { get; set; }
        public Nullable<double> NonOperatingExpenses { get; set; }
        public Nullable<double> IncomeBeforeTaxFromContinuingOperations { get; set; }
        public Nullable<double> IncomeTaxExpense { get; set; }
        public Nullable<double> IncomeFromContinuingOperations { get; set; }
        public Nullable<double> IncomeOnDiscontinuedOperations { get; set; }
        public Nullable<double> ExtraordinaryItems { get; set; }
        public Nullable<double> MulativeEffectOfChangeInAccountingPrinciple { get; set; }
        public Nullable<double> NetIncome { get; set; }
        public Nullable<double> EarningPerShare { get; set; }
    }
}