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
    
    public partial class BalanceSheet
    {
        public int Id { get; set; }
        public string Ticker { get; set; }
        public string Date { get; set; }
        public Nullable<double> CurrentAssets { get; set; }
        public Nullable<double> NonCurrentAsset { get; set; }
        public Nullable<double> FixedAssets { get; set; }
        public Nullable<double> IntangibleAssets { get; set; }
        public Nullable<double> OtherAssets { get; set; }
        public Nullable<double> TotalAssets { get; set; }
        public Nullable<double> Currentliabilities { get; set; }
        public Nullable<double> LongTermliabilities { get; set; }
        public Nullable<double> VariousPreparations { get; set; }
        public Nullable<double> Otherliabilities { get; set; }
        public Nullable<double> Totalliabilities { get; set; }
        public Nullable<double> CompanyStock { get; set; }
        public Nullable<double> AdditionalPaidInCapital { get; set; }
        public Nullable<double> RetainedEarnings_ { get; set; }
        public Nullable<double> Stockholders_EquitiesAdjustments { get; set; }
        public Nullable<double> UnderTheCommonControlOfTheFormerHandRights { get; set; }
        public Nullable<double> NoncontrollingInterests { get; set; }
        public Nullable<double> TotalEquity { get; set; }
        public Nullable<double> NumberOfShareCapitalAwaitingRetirement { get; set; }
        public Nullable<double> EquivalentIssueSharesOfAdvanceReceiptsForOrdinaryShare { get; set; }
        public Nullable<double> BeforeTheMmergerCommonNonControllingInterestsAndlosses { get; set; }
        public Nullable<double> NumberOfSharesInEntityHeldByEntityAndByItsSubsidiaries { get; set; }
        public Nullable<double> BookValuePerShare { get; set; }
    }
}
