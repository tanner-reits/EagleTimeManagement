using System;
using System.Collections.Generic;

namespace EagleTM.Data
{
    public partial class TblSpec
    {
        public TblSpec()
        {
            InverseParentSpecAuto = new HashSet<TblSpec>();
            TblTimecards = new HashSet<TblTimecards>();
            TblTempTimecards = new HashSet<TblTempTimecards>();
        }

        public int SpecAutoId { get; set; }
        public int ProjectId { get; set; }
        public double SpecId { get; set; }
        public string Sinternal { get; set; }
        public int Squantity { get; set; }
        public DateTime Sdate { get; set; }
        public string Srevision { get; set; }
        public DateTime? SrevDate { get; set; }
        public string Sdescription { get; set; }
        public int? SrefProjectId { get; set; }
        public double? SrefSpecId { get; set; }
        public string Scomments { get; set; }
        public bool Sactive { get; set; }
        public int MachineType { get; set; }
        public int? Engineer { get; set; }
        public int? BuildLocation { get; set; }
        public DateTime? BudgetEngRelease { get; set; }
        public DateTime? RevisedEngRelease { get; set; }
        public DateTime? SfinalBillingComplete { get; set; }
        public bool? Archived { get; set; }
        public DateTime? BudgetEngPreRelease { get; set; }
        public DateTime? BudgetMfgRelease { get; set; }
        public DateTime? RevisedMfgRelease { get; set; }
        public DateTime? BudgetAccRelease { get; set; }
        public DateTime? BudgetClosingRelease { get; set; }
        public decimal? SamountInvoicedtoDate { get; set; }
        public DateTime? MfgBegin { get; set; }
        public decimal SalesPrice { get; set; }
        public bool SalesPriceFixed { get; set; }
        public int? ParentSpecAutoId { get; set; }
        public string Hierarchy { get; set; }
        public int? ItemId { get; set; }
        public decimal? PercentComplete { get; set; }
        public DateTime? PercentCompleteDate { get; set; }
        public string MachineCustom1 { get; set; }
        public string MachineCustom2 { get; set; }
        public decimal? MachineCustom3 { get; set; }
        public decimal? MachineCustom4 { get; set; }
        public DateTime? MachineCustom5 { get; set; }
        public DateTime? MachineCustom6 { get; set; }
        public bool MachineCustom7 { get; set; }
        public bool MachineCustom8 { get; set; }
        public DateTime? BudgetShipRelease { get; set; }

        public virtual TblEmployee EngineerNavigation { get; set; }
        public virtual TblSpec ParentSpecAuto { get; set; }
        public virtual TblProjects Project { get; set; }
        public virtual ICollection<TblSpec> InverseParentSpecAuto { get; set; }
        public virtual ICollection<TblTimecards> TblTimecards { get; set; }
        public virtual ICollection<TblTempTimecards> TblTempTimecards { get; set; }
    }
}
