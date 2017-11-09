using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CompanyFinancialAnalysis.Models;
using System.Data.Entity;

namespace CompanyFinancialAnalysis.Service
{
    public class DBService
    {
        /// <summary>
        /// 更新資料庫內容，沒有的加進去，有的改成加入的內容。比較Ticker&Date
        /// </summary>
        /// <param name="companyLst"></param>
        public void SaveCompanyList(List<Company> companyLst)
        {
            using (CorporateFinanceDBEntities _cfDB = new CorporateFinanceDBEntities())
            {
                foreach (var comp in companyLst)
                {

                    if (CheckCompInDb(comp))
                    {
                        CompanyDataTable cmpResult = _cfDB.CompanyDataTable.Single(c => (c.Ticker == comp.Ticker && c.Date == comp.Date));

                        CompanyDataTable newCmp = new CompanyDataTable
                        {
                            Id = cmpResult.Id,
                            Ticker = comp.Ticker,
                            Name = comp.Name,
                            Date = comp.Date,
                            CompanyStock = comp.CompanyStock,
                            EBIT = comp.EBIT,
                            Equity = comp.Equity,
                            GrossSales = comp.GrossSales,
                            MarketValue = comp.MarketValue,
                            RetainedEarning = comp.RetainedEarning,
                            StockPrice = comp.StockPrice,
                            TotalAsset = comp.TotalAsset,
                            TotalLiability = comp.TotalLiability,
                            WorkingCapital = comp.WorkingCapital,
                            ZValue = comp.ZValue.ToString()
                        };

                        if (cmpResult.Equals(newCmp))
                        {
                            _cfDB.Entry(cmpResult).State = EntityState.Unchanged;
                        }
                        else
                        {
                            _cfDB.Entry(cmpResult).CurrentValues.SetValues(newCmp);

                            _cfDB.Entry(cmpResult).State = EntityState.Modified;
                        }


                        //_cfDB.Entry(cmpResult).CurrentValues.SetValues(newCmp);

                        //_cfDB.Entry(cmpResult).State = EntityState.Modified;

                        BalanceSheet bsResult = _cfDB.BalanceSheet.Single(b => (b.Ticker == comp.Ticker && b.Date == comp.Date));

                        BalanceSheet newCmpB = new BalanceSheet
                        {
                            Id = bsResult.Id,
                            Ticker = comp.Ticker,
                            Date = comp.Date,
                            AdditionalPaidInCapital = comp.compBD.AdditionalPaidInCapital,
                            BeforeTheMmergerCommonNonControllingInterestsAndlosses = comp.compBD.BeforeTheMmergerCommonNonControllingInterestsAndlosses,
                            BookValuePerShare = comp.compBD.BookValuePerShare,
                            CompanyStock = comp.compBD.CompanyStock,
                            CurrentAssets = comp.compBD.CurrentAssets,
                            Currentliabilities = comp.compBD.Currentliabilities,
                            EquivalentIssueSharesOfAdvanceReceiptsForOrdinaryShare = comp.compBD.EquivalentIssueSharesOfAdvanceReceiptsForOrdinaryShare,
                            FixedAssets = comp.compBD.FixedAssets,
                            IntangibleAssets = comp.compBD.IntangibleAssets,
                            LongTermliabilities = comp.compBD.LongTermliabilities,
                            NoncontrollingInterests = comp.compBD.NoncontrollingInterests,
                            NonCurrentAsset = comp.compBD.NonCurrentAsset,
                            NumberOfShareCapitalAwaitingRetirement = comp.compBD.NumberOfShareCapitalAwaitingRetirement,
                            NumberOfSharesInEntityHeldByEntityAndByItsSubsidiaries = comp.compBD.NumberOfSharesInEntityHeldByEntityAndByItsSubsidiaries,
                            OtherAssets = comp.compBD.OtherAssets,
                            UnderTheCommonControlOfTheFormerHandRights = comp.compBD.UnderTheCommonControlOfTheFormerHandRights,
                            Otherliabilities = comp.compBD.Otherliabilities,
                            RetainedEarnings_ = comp.compBD.RetainedEarning,
                            Stockholders_EquitiesAdjustments = comp.compBD.Equity,
                            TotalAssets = comp.compBD.TotalAsset,
                            TotalEquity = comp.compBD.Equity,
                            Totalliabilities = comp.compBD.TotalLiability,
                            VariousPreparations = comp.compBD.VariousPreparations
                        };

                        if (bsResult.Equals(newCmpB))
                        {
                            _cfDB.Entry(bsResult).State = EntityState.Unchanged;
                        }
                        else
                        {
                            _cfDB.Entry(bsResult).CurrentValues.SetValues(newCmpB);

                            _cfDB.Entry(bsResult).State = EntityState.Modified;
                        }

                        //_cfDB.Entry(bsResult).CurrentValues.SetValues(newCmpB);

                        //_cfDB.Entry(bsResult).State = EntityState.Modified;

                        IncomeStatement isResult = _cfDB.IncomeStatement.Single(b => (b.Ticker == comp.Ticker && b.Date == comp.Date));

                        IncomeStatement newCmpI = new IncomeStatement
                        {
                            Id = isResult.Id,
                            Ticker = comp.Ticker,
                            Date = comp.Date,
                            CostOfGoodsSold = comp.compCD.CostOfGoodsSold,
                            EarningPerShare = comp.compCD.EarningPerShare,
                            ExtraordinaryItems = comp.compCD.ExtraordinaryItems,
                            Grossprofit = comp.compCD.Grossprofit,
                            IncomeBeforeTaxFromContinuingOperations = comp.compCD.IncomeBeforeTaxFromContinuingOperations,
                            IncomeFromContinuingOperations = comp.compCD.IncomeFromContinuingOperations,
                            IncomeTaxExpense = comp.compCD.IncomeTaxExpense,
                            MulativeEffectOfChangeInAccountingPrinciple = comp.compCD.MulativeEffectOfChangeInAccountingPrinciple,
                            NetIncome = comp.compCD.NetIncome,
                            NonOperatingExpenses = comp.compCD.NonOperatingExpenses,
                            NonOperatingIncome = comp.compCD.NonOperatingIncome,
                            OperatingExpenses = comp.compCD.OperatingExpenses,
                            OperatingIncome = comp.compCD.OperatingIncome,
                            OperatingRevenue = comp.compCD.OperatingRevenue,
                            RealizedGainFromInterAffiliateAccounts = comp.compCD.RealizedGainFromInterAffiliateAccounts,
                            UnrealizedGainFromInterAffiliateAccounts = comp.compCD.UnrealizedGainFromInterAffiliateAccounts,
                            IncomeOnDiscontinuedOperations = comp.compCD.IncomeOnDiscontinuedOperations
                        };

                        if (isResult.Equals(newCmpI))
                        {
                            _cfDB.Entry(bsResult).State = EntityState.Unchanged;
                        }
                        else
                        {
                            _cfDB.Entry(isResult).CurrentValues.SetValues(newCmpI);

                            _cfDB.Entry(isResult).State = EntityState.Modified;
                        }

                        //_cfDB.Entry(isResult).CurrentValues.SetValues(newCmpI);

                        //_cfDB.Entry(isResult).State = EntityState.Modified;

                        //var result = _cfDB.SaveChanges();
                    }
                    else
                    {
                        CompanyDataTable addCmp = new CompanyDataTable
                        {
                            Ticker = comp.Ticker,
                            Name = comp.Name,
                            Date = comp.Date,
                            CompanyStock = comp.CompanyStock,
                            EBIT = comp.EBIT,
                            Equity = comp.Equity,
                            GrossSales = comp.GrossSales,
                            MarketValue = comp.MarketValue,
                            RetainedEarning = comp.RetainedEarning,
                            StockPrice = comp.StockPrice,
                            TotalAsset = comp.TotalAsset,
                            TotalLiability = comp.TotalLiability,
                            WorkingCapital = comp.WorkingCapital,
                            ZValue = comp.ZValue.ToString()
                        };

                        _cfDB.CompanyDataTable.Add(addCmp);

                        BalanceSheet addCmpB = new BalanceSheet
                        {
                            Ticker = comp.Ticker,
                            Date = comp.Date,
                            AdditionalPaidInCapital = comp.compBD.AdditionalPaidInCapital,
                            BeforeTheMmergerCommonNonControllingInterestsAndlosses = comp.compBD.BeforeTheMmergerCommonNonControllingInterestsAndlosses,
                            BookValuePerShare = comp.compBD.BookValuePerShare,
                            CompanyStock = comp.compBD.CompanyStock,
                            CurrentAssets = comp.compBD.CurrentAssets,
                            Currentliabilities = comp.compBD.Currentliabilities,
                            EquivalentIssueSharesOfAdvanceReceiptsForOrdinaryShare = comp.compBD.EquivalentIssueSharesOfAdvanceReceiptsForOrdinaryShare,
                            FixedAssets = comp.compBD.FixedAssets,
                            IntangibleAssets = comp.compBD.IntangibleAssets,
                            LongTermliabilities = comp.compBD.LongTermliabilities,
                            NoncontrollingInterests = comp.compBD.NoncontrollingInterests,
                            NonCurrentAsset = comp.compBD.NonCurrentAsset,
                            NumberOfShareCapitalAwaitingRetirement = comp.compBD.NumberOfShareCapitalAwaitingRetirement,
                            NumberOfSharesInEntityHeldByEntityAndByItsSubsidiaries = comp.compBD.NumberOfSharesInEntityHeldByEntityAndByItsSubsidiaries,
                            OtherAssets = comp.compBD.OtherAssets,
                            UnderTheCommonControlOfTheFormerHandRights = comp.compBD.UnderTheCommonControlOfTheFormerHandRights,
                            Otherliabilities = comp.compBD.Otherliabilities,
                            RetainedEarnings_ = comp.compBD.RetainedEarning,
                            Stockholders_EquitiesAdjustments = comp.compBD.Equity,
                            TotalAssets = comp.compBD.TotalAsset,
                            TotalEquity = comp.compBD.Equity,
                            Totalliabilities = comp.compBD.TotalLiability,
                            VariousPreparations = comp.compBD.VariousPreparations
                        };

                        _cfDB.BalanceSheet.Add(addCmpB);

                        IncomeStatement addCmpI = new IncomeStatement
                        {
                            Ticker = comp.Ticker,
                            Date = comp.Date,
                            CostOfGoodsSold = comp.compCD.CostOfGoodsSold,
                            EarningPerShare = comp.compCD.EarningPerShare,
                            ExtraordinaryItems = comp.compCD.ExtraordinaryItems,
                            Grossprofit = comp.compCD.Grossprofit,
                            IncomeBeforeTaxFromContinuingOperations = comp.compCD.IncomeBeforeTaxFromContinuingOperations,
                            IncomeFromContinuingOperations = comp.compCD.IncomeFromContinuingOperations,
                            IncomeTaxExpense = comp.compCD.IncomeTaxExpense,
                            MulativeEffectOfChangeInAccountingPrinciple = comp.compCD.MulativeEffectOfChangeInAccountingPrinciple,
                            NetIncome = comp.compCD.NetIncome,
                            NonOperatingExpenses = comp.compCD.NonOperatingExpenses,
                            NonOperatingIncome = comp.compCD.NonOperatingIncome,
                            OperatingExpenses = comp.compCD.OperatingExpenses,
                            OperatingIncome = comp.compCD.OperatingIncome,
                            OperatingRevenue = comp.compCD.OperatingRevenue,
                            RealizedGainFromInterAffiliateAccounts = comp.compCD.RealizedGainFromInterAffiliateAccounts,
                            UnrealizedGainFromInterAffiliateAccounts = comp.compCD.UnrealizedGainFromInterAffiliateAccounts,
                            IncomeOnDiscontinuedOperations = comp.compCD.IncomeOnDiscontinuedOperations
                        };

                        _cfDB.IncomeStatement.Add(addCmpI);

                        //var result = _cfDB.SaveChanges();
                    }
                }

                var result = _cfDB.SaveChanges();
            }
        }

        /// <summary>
        /// 檢查是否存在資料庫中
        /// </summary>
        /// <param name="cmp">公司資料</param>
        /// <returns>布林值</returns>
        private bool CheckCompInDb(Company cmp)
        {
            using (CorporateFinanceDBEntities _cfDb = new CorporateFinanceDBEntities())
            {
                var findResult = _cfDb.CompanyDataTable.Where(c => (c.Ticker == cmp.Ticker && c.Date == cmp.Date)).ToList();

                if (findResult.Count != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void UpdateCompInDB(Company comp)
        {
            using (CorporateFinanceDBEntities _cfDb = new CorporateFinanceDBEntities())
            {
                CompanyDataTable cmpResult = _cfDb.CompanyDataTable.Single(c => (c.Ticker == comp.Ticker && c.Date == comp.Date));

                CompanyDataTable newCmp = new CompanyDataTable
                {
                    Ticker = comp.Ticker,
                    Name = comp.Name,
                    Date = comp.Date,
                    CompanyStock = comp.CompanyStock,
                    EBIT = comp.EBIT,
                    Equity = comp.Equity,
                    GrossSales = comp.GrossSales,
                    MarketValue = comp.MarketValue,
                    RetainedEarning = comp.RetainedEarning,
                    StockPrice = comp.StockPrice,
                    TotalAsset = comp.TotalAsset,
                    TotalLiability = comp.TotalLiability,
                    WorkingCapital = comp.WorkingCapital,
                    ZValue = comp.ZValue.ToString()
                };

                _cfDb.Entry(cmpResult).CurrentValues.SetValues(newCmp);

                _cfDb.Entry(cmpResult).State = EntityState.Modified;

                BalanceSheet bsResult = _cfDb.BalanceSheet.Single(b => (b.Ticker == comp.Ticker && b.Date == comp.Date));

                BalanceSheet newCmpB = new BalanceSheet
                {
                    Ticker = comp.Ticker,
                    Date = comp.Date,
                    AdditionalPaidInCapital = comp.compBD.AdditionalPaidInCapital,
                    BeforeTheMmergerCommonNonControllingInterestsAndlosses = comp.compBD.BeforeTheMmergerCommonNonControllingInterestsAndlosses,
                    BookValuePerShare = comp.compBD.BookValuePerShare,
                    CompanyStock = comp.compBD.CompanyStock,
                    CurrentAssets = comp.compBD.CurrentAssets,
                    Currentliabilities = comp.compBD.Currentliabilities,
                    EquivalentIssueSharesOfAdvanceReceiptsForOrdinaryShare = comp.compBD.EquivalentIssueSharesOfAdvanceReceiptsForOrdinaryShare,
                    FixedAssets = comp.compBD.FixedAssets,
                    IntangibleAssets = comp.compBD.IntangibleAssets,
                    LongTermliabilities = comp.compBD.LongTermliabilities,
                    NoncontrollingInterests = comp.compBD.NoncontrollingInterests,
                    NonCurrentAsset = comp.compBD.NonCurrentAsset,
                    NumberOfShareCapitalAwaitingRetirement = comp.compBD.NumberOfShareCapitalAwaitingRetirement,
                    NumberOfSharesInEntityHeldByEntityAndByItsSubsidiaries = comp.compBD.NumberOfSharesInEntityHeldByEntityAndByItsSubsidiaries,
                    OtherAssets = comp.compBD.OtherAssets,
                    UnderTheCommonControlOfTheFormerHandRights = comp.compBD.UnderTheCommonControlOfTheFormerHandRights,
                    Otherliabilities = comp.compBD.Otherliabilities,
                    RetainedEarnings_ = comp.compBD.RetainedEarning,
                    Stockholders_EquitiesAdjustments = comp.compBD.Equity,
                    TotalAssets = comp.compBD.TotalAsset,
                    TotalEquity = comp.compBD.Equity,
                    Totalliabilities = comp.compBD.TotalLiability,
                    VariousPreparations = comp.compBD.VariousPreparations
                };

                _cfDb.Entry(bsResult).CurrentValues.SetValues(newCmpB);

                _cfDb.Entry(bsResult).State = EntityState.Modified;

                IncomeStatement isResult = _cfDb.IncomeStatement.Single(b => (b.Ticker == comp.Ticker && b.Date == comp.Date));

                IncomeStatement newCmpI = new IncomeStatement
                {
                    Ticker = comp.Ticker,
                    Date = comp.Date,
                    CostOfGoodsSold = comp.compCD.CostOfGoodsSold,
                    EarningPerShare = comp.compCD.EarningPerShare,
                    ExtraordinaryItems = comp.compCD.ExtraordinaryItems,
                    Grossprofit = comp.compCD.Grossprofit,
                    IncomeBeforeTaxFromContinuingOperations = comp.compCD.IncomeBeforeTaxFromContinuingOperations,
                    IncomeFromContinuingOperations = comp.compCD.IncomeFromContinuingOperations,
                    IncomeTaxExpense = comp.compCD.IncomeTaxExpense,
                    MulativeEffectOfChangeInAccountingPrinciple = comp.compCD.MulativeEffectOfChangeInAccountingPrinciple,
                    NetIncome = comp.compCD.NetIncome,
                    NonOperatingExpenses = comp.compCD.NonOperatingExpenses,
                    NonOperatingIncome = comp.compCD.NonOperatingIncome,
                    OperatingExpenses = comp.compCD.OperatingExpenses,
                    OperatingIncome = comp.compCD.OperatingIncome,
                    OperatingRevenue = comp.compCD.OperatingRevenue,
                    RealizedGainFromInterAffiliateAccounts = comp.compCD.RealizedGainFromInterAffiliateAccounts,
                    UnrealizedGainFromInterAffiliateAccounts = comp.compCD.UnrealizedGainFromInterAffiliateAccounts,
                    IncomeOnDiscontinuedOperations = comp.compCD.IncomeOnDiscontinuedOperations
                };

                _cfDb.Entry(isResult).CurrentValues.SetValues(newCmpI);

                _cfDb.Entry(isResult).State = EntityState.Modified;

                var result = _cfDb.SaveChanges();
            }
        }
    }
}