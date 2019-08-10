using System;
using System.Collections.Generic;

namespace EagleTM.Data
{
    public partial class TblTempTimecards
    {
        public int TempCardId { get; set; }
        public int EmployeeId { get; set; }
        public int TimeId { get; set; }
        public int TimePeriodId { get; set; }
        public DateTime TimeDate { get; set; }
        public int ProjectId { get; set; }
        public double SpecId { get; set; }
        public int HourType { get; set; }
        public decimal HourTime { get; set; }
        public bool? OnRoad { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? EditedDate { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }

        public virtual TlkpHourTypes HourTypeNavigation { get; set; }
        public virtual TblProjects Project { get; set; }
        public virtual TblTimecards Time { get; set; }
        
        public virtual TblSpec Spec { get; set; }
    }
}
