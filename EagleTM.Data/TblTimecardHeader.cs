using System;
using System.Collections.Generic;

namespace EagleTM.Data
{
    public partial class TblTimecardHeader
    {
        public TblTimecardHeader()
        {
            TblTimecards = new HashSet<TblTimecards>();
        }

        public int TimePeriodId { get; set; }
        public DateTime PayPeriodStartDate { get; set; }
        public DateTime PayPeriodEndDate { get; set; }
        public bool Batched { get; set; }
        public DateTime? PostedDate { get; set; }
        public bool Validated { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public string RefDescription { get; set; }
        public string Comments { get; set; }
        public int? AccountNum { get; set; }
        public decimal? Amount { get; set; }
        public bool Exported { get; set; }

        public virtual ICollection<TblTimecards> TblTimecards { get; set; }
    }
}
