using System;
using System.Collections.Generic;

namespace EagleTM.Data
{
    public partial class TlkpHourTypes
    {
        public TlkpHourTypes()
        {
            TblTempTimecards = new HashSet<TblTempTimecards>();
            TblTimecards = new HashSet<TblTimecards>();
        }

        public int HourType { get; set; }
        public string HourDescription { get; set; }
        public string HourDepartment { get; set; }
        public string HourClass { get; set; }
        public decimal? HourCost { get; set; }
        public bool? HourActive { get; set; }
        public bool? Exported { get; set; }
        public string EarningNumber { get; set; }
        public string Glaccount { get; set; }
        public int? Glid { get; set; }

        public virtual ICollection<TblTempTimecards> TblTempTimecards { get; set; }
        public virtual ICollection<TblTimecards> TblTimecards { get; set; }
    }
}
