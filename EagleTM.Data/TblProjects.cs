using System;
using System.Collections.Generic;

namespace EagleTM.Data
{
    public partial class TblProjects
    {
        public TblProjects()
        {
            TblSpec = new HashSet<TblSpec>();
            TblTempTimecards = new HashSet<TblTempTimecards>();
        }

        public int ProjectAutoId { get; set; }
        public int ProjectId { get; set; }
        public int CompanyId { get; set; }
        public string ProjectAlternate { get; set; }
        public int? PsalesId { get; set; }
        public int? PmanagerId { get; set; }
        public string Pstatus { get; set; }
        public int? Ppriority { get; set; }
        public string Pdescription { get; set; }
        public DateTime? Pfirst { get; set; }
        public DateTime? Pnext { get; set; }
        public DateTime? Plast { get; set; }
        public bool Pactive { get; set; }
        public DateTime? Pdelivery { get; set; }
        public int? Preference { get; set; }
        public decimal PsaleCurrRate { get; set; }
        public string PsaleCurr { get; set; }
        public decimal? PercentComplete { get; set; }
        public DateTime? PercentCompleteDate { get; set; }
        public string ProjectCustom1 { get; set; }
        public string ProjectCustom2 { get; set; }
        public decimal? ProjectCustom3 { get; set; }
        public decimal? ProjectCustom4 { get; set; }
        public DateTime? ProjectCustom5 { get; set; }
        public DateTime? ProjectCustom6 { get; set; }
        public bool ProjectCustom7 { get; set; }
        public bool ProjectCustom8 { get; set; }

        public virtual TblEmployee Pmanager { get; set; }
        public virtual TblEmployee Psales { get; set; }
        public virtual ICollection<TblSpec> TblSpec { get; set; }
        public virtual ICollection<TblTempTimecards> TblTempTimecards { get; set; }
    }
}
