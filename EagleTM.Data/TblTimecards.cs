using System;
using System.Collections.Generic;

namespace EagleTM.Data
{
    public partial class TblTimecards
    {
        public TblTimecards()
        {
            TblTempTimecards = new HashSet<TblTempTimecards>();
        }

        public int TimeId { get; set; }
        public DateTime TimeDate { get; set; }
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public double SpecId { get; set; }
        public int HourType { get; set; }
        public decimal HourTime { get; set; }
        public string HourDrawing { get; set; }
        public decimal HourRate { get; set; }
        public string HourClass { get; set; }
        public decimal HourFactor { get; set; }
        public int? TimePeriodId { get; set; }
        public bool Validated { get; set; }
        public bool Batched { get; set; }
        public string EmpNumber { get; set; }
        public int? NonConformanceId { get; set; }
        public string TimecardCustom1 { get; set; }
        public string TimecardCustom2 { get; set; }
        public decimal? TimecardCustom3 { get; set; }
        public decimal? TimecardCustom4 { get; set; }
        public DateTime? TimecardCustom5 { get; set; }
        public DateTime? TimecardCustom6 { get; set; }
        public bool TimecardCustom7 { get; set; }
        public bool TimecardCustom8 { get; set; }
        public int? ProcessScheduleDetailId { get; set; }
        public string Comments { get; set; }
        public bool Exported { get; set; }
        public bool DoNotExport { get; set; }
        public string ExportErrorMsg { get; set; }

        public virtual TblEmployee Employee { get; set; }
        public virtual TlkpHourTypes HourTypeNavigation { get; set; }
        public virtual TblSpec TblSpec { get; set; }
        public virtual TblTimecardHeader TimePeriod { get; set; }
        public virtual ICollection<TblTempTimecards> TblTempTimecards { get; set; }
    }
}
