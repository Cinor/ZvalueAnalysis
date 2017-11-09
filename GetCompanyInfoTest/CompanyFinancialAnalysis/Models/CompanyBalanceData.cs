using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyFinancialAnalysis.Models
{
    /// <summary>
    /// 資產負債表
    /// </summary>
    public class CompanyBalanceData:Company
    {
        public CompanyBalanceData() { }

        public CompanyBalanceData(Dictionary<string,int> bsDic)
        {
            CurrentAssets = bsDic.ContainsKey("流動資產")?bsDic["流動資產"]:0;
            NonCurrentAsset = bsDic.ContainsKey("非流動資產") ? bsDic["非流動資產"] : 0;
            FixedAssets = bsDic.ContainsKey("固定資產") ? bsDic["固定資產"] : 0;
            IntangibleAssets = bsDic.ContainsKey("無形資產") ? bsDic["無形資產"] : 0;
            OtherAssets = bsDic.ContainsKey("其他資產")?bsDic["其他資產"]:0;
            TotalAsset = bsDic.ContainsKey("資產總計") ? bsDic["資產總計"] :
                         bsDic.ContainsKey("資產總額") ? bsDic["資產總額"] :
                         0;
            Currentliabilities = bsDic.ContainsKey("流動負債") ? bsDic["流動負債"]:0;
            LongTermliabilities = bsDic.ContainsKey("長期負債") ? bsDic["長期負債"] : 0;
            VariousPreparations = bsDic.ContainsKey("各項準備") ? bsDic["各項準備"] : 0;
            Otherliabilities = bsDic.ContainsKey("其他負債") ? bsDic["其他負債"] : 0;
            TotalLiability = bsDic.ContainsKey("負債總額") ? bsDic["負債總額"] :
                             bsDic.ContainsKey("負債總計") ? bsDic["負債總計"] :
                             0;
            AdditionalPaidInCapital = bsDic.ContainsKey("基本公債") ? bsDic["基本公債"] : 0;
            RetainedEarning = bsDic["保留盈餘"];
            Equity = bsDic.ContainsKey("權益總計") ? bsDic["權益總計"] :
                     bsDic.ContainsKey("股東權益總計") ? bsDic["股東權益總計"] :
                     bsDic.ContainsKey("權益總額")? bsDic["權益總額"]:
                     0;
            UnderTheCommonControlOfTheFormerHandRights = bsDic.ContainsKey("共同控制下前手權益") ? bsDic["共同控制下前手權益"]:0;
            NoncontrollingInterests = bsDic.ContainsKey("非控制權益") ? bsDic["非控制權益"] : 0;
            NumberOfShareCapitalAwaitingRetirement = bsDic.ContainsKey("待註銷股本股數（單位：股）") ? bsDic["待註銷股本股數（單位：股）"] : 0;
            EquivalentIssueSharesOfAdvanceReceiptsForOrdinaryShare = bsDic.ContainsKey("預收股款（股東權益項下）之約當發行股數（單位：股）") ? bsDic["預收股款（股東權益項下）之約當發行股數（單位：股）"] :
                                                                     bsDic.ContainsKey("預收股款（權益項下）之約當發行股數（單位：股）") ? bsDic["預收股款（權益項下）之約當發行股數（單位：股）"] :
                                                                     0;
            BeforeTheMmergerCommonNonControllingInterestsAndlosses = bsDic.ContainsKey("合併前非屬共同控制股權") ? bsDic["合併前非屬共同控制股權"] : 0;
            NumberOfSharesInEntityHeldByEntityAndByItsSubsidiaries = bsDic.ContainsKey("母公司暨子公司所持有之母公司庫藏股股數(單位:股)") ? bsDic["母公司暨子公司所持有之母公司庫藏股股數(單位:股)"] :
                                                                     bsDic.ContainsKey("母公司暨子公司所持有之母公司庫藏股股數（單位：股）") ? bsDic["母公司暨子公司所持有之母公司庫藏股股數（單位：股）"] :
                                                                     0;
            BookValuePerShare = bsDic.ContainsKey("每股淨值(元)") ? bsDic["每股淨值(元)"]:0;
            CompanyStock = bsDic.ContainsKey("股本")?bsDic["股本"]:0;
        }

        /// <summary>
        /// 流動資產
        /// </summary>
        public double CurrentAssets { get; set; }
        /// <summary>
        /// 非流動資產
        /// </summary>
        public double NonCurrentAsset { get; set; }
        /// <summary>
        /// 固定資產
        /// </summary>
        public double? FixedAssets { get; set; }
        /// <summary>
        /// 無形資產
        /// </summary>
        public double IntangibleAssets { get; set; }
        /// <summary>
        /// 其他資產
        /// </summary>
        public double OtherAssets { get; set; }
        /// <summary>
        /// 資產總計 繼承 Company
        /// </summary>
        //public double TotalAsset { get; set; }
        /// <summary>
        /// 流動負債
        /// </summary>
        public double Currentliabilities { get; set; }
        /// <summary>
        /// 長期負債
        /// </summary>
        public double LongTermliabilities { get; set; }
        /// <summary>
        /// 各項準備
        /// </summary>
        public double VariousPreparations { get; set; }
        /// <summary>
        /// 其他負債
        /// </summary>
        public double Otherliabilities { get; set; }
        ///// <summary>
        ///// 負債總計
        ///// </summary>
        //public double Totalliabilities { get; set; }
        ///// <summary>
        ///// 股本 Company裡面有
        ///// </summary>
        //public double CompanyStock { get; set; }
        /// <summary>
        /// 資本公債
        /// </summary>
        public double AdditionalPaidInCapital { get; set; }
        ///// <summary>
        ///// 保留盈餘 繼承Company
        ///// </summary>
        //public double RetainedEarnings_ { get; set; }
        ///// <summary>
        ///// 股東權益 繼承Company
        ///// </summary>
        //public double Stockholders_EquitiesAdjustments { get; set; }
        /// <summary>
        /// 共同控制下前手權益
        /// </summary>
        public double UnderTheCommonControlOfTheFormerHandRights { get; set; }
        /// <summary>
        /// 非控制權益
        /// </summary>
        public double NoncontrollingInterests { get; set; }
        ///// <summary>
        ///// 權益總計
        ///// </summary>
        //public double TotalEquity { get; set; }
        /// <summary>
        /// 貸註銷股本
        /// </summary>
        public double NumberOfShareCapitalAwaitingRetirement { get; set; }
        /// <summary>
        /// 預收股款之約當發行股數
        /// </summary>
        public double EquivalentIssueSharesOfAdvanceReceiptsForOrdinaryShare { get; set; }
        /// <summary>
        /// 合併前非屬共同控制股權損益
        /// </summary>
        public double BeforeTheMmergerCommonNonControllingInterestsAndlosses { get; set; }
        /// <summary>
        /// 母公司暨子公司所持有之母公司庫藏股
        /// </summary>
        public double NumberOfSharesInEntityHeldByEntityAndByItsSubsidiaries { get; set; }
        /// <summary>
        /// 每股淨值
        /// </summary>
        public double BookValuePerShare { get; set; }
    }
}