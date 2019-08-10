using System;
using System.Collections.Generic;

namespace EagleTM.Data
{
    public partial class TblEmployee
    {
        public TblEmployee()
        {
            TblProjectsPmanager = new HashSet<TblProjects>();
            TblProjectsPsales = new HashSet<TblProjects>();
            TblSpec = new HashSet<TblSpec>();
            TblTimecards = new HashSet<TblTimecards>();
        }

        public int EmployeeId { get; set; }
        public string EmpNumber { get; set; }
        public string EmpFirstName { get; set; }
        public string EmpLastName { get; set; }
        public string EmpDept { get; set; }
        public bool? EmpActive { get; set; }
        public string EmpEmail { get; set; }
        public decimal EmpBaseHourlyCost { get; set; }
        public string UserId { get; set; }
        public byte[] Password { get; set; }
        public bool SystemUser { get; set; }
        public int? EmpSubDept { get; set; }
        public int? EmpTypeId { get; set; }
        public int? PayGroupId { get; set; }
        public Guid? LoginIdentifier { get; set; }
        public string UserName { get; set; }
        public bool? RequirePasswordChange { get; set; }
        public string EmployeeCustom1 { get; set; }
        public string EmployeeCustom2 { get; set; }
        public decimal? EmployeeCustom3 { get; set; }
        public decimal? EmployeeCustom4 { get; set; }
        public DateTime? EmployeeCustom5 { get; set; }
        public DateTime? EmployeeCustom6 { get; set; }
        public bool EmployeeCustom7 { get; set; }
        public bool EmployeeCustom8 { get; set; }
        public string QbaccessToken { get; set; }
        public string QbaccessSecretCode { get; set; }
        public string QbaccessCompanyId { get; set; }

        public virtual ICollection<TblProjects> TblProjectsPmanager { get; set; }
        public virtual ICollection<TblProjects> TblProjectsPsales { get; set; }
        public virtual ICollection<TblSpec> TblSpec { get; set; }
        public virtual ICollection<TblTimecards> TblTimecards { get; set; }
    }
}
