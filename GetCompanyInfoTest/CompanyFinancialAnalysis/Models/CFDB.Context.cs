﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CorporateFinanceDBEntities : DbContext
    {
        public CorporateFinanceDBEntities()
            : base("name=CorporateFinanceDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BalanceSheet> BalanceSheet { get; set; }
        public virtual DbSet<CompanyDataTable> CompanyDataTable { get; set; }
        public virtual DbSet<IncomeStatement> IncomeStatement { get; set; }
    }
}
