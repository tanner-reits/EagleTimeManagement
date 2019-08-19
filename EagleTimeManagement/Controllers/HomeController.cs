using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using EagleTimeManagement.Models;
using EagleTM.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EagleTimeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly QuesticaContext context;

        public HomeController(QuesticaContext context)
        {
            this.context = context;
        }
        
        public IActionResult Index(string actionDone)
        {
            // Redirect user to login screen if not currently logged in
            if (HttpContext.Session.GetInt32("EmpID") == null)
            {
                return Redirect("/Login");
            }

            ViewData["EmpName"] = HttpContext.Session.GetString("EmpName");


            //////////////////////////////////////////////////////////////////
            /// DELETE WHEN NOT TESTING!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            /// ///////////////////////////////////////////////////////////
            //int empId = 77;

            int empId = (int) HttpContext.Session.GetInt32("EmpID");
            
            // Get the active pay period 
            var activePayPeriod = context.TblTimecardHeader.Where(x => x.Validated == false).Select(x => x.TimePeriodId).FirstOrDefault();

            // Get a list of the time cards that are to be deleted (in the temp table and marked as delete)
            var deletedTimeIds = context.TblTempTimecards.Where(x => x.TimePeriodId == activePayPeriod && x.EmployeeId == empId && x.Delete).Select(x => x.TimeId).ToList();

            // Get all the time cards from the temp table that are for the current employee and current pay period and return as a tbltimecards model
            var testTemp = context.TblTempTimecards.Include(x => x.Spec).Include(x => x.Project).Include(x => x.HourTypeNavigation).Where(x => x.TimePeriodId == activePayPeriod
                                                    && x.EmployeeId == empId)
                                                    .Select(x => new TblTimecards
                                                    {
                                                        TimeId = (int)x.TimeId,
                                                        TimeDate = (DateTime)x.TimeDate,
                                                        EmployeeId = (int)x.EmployeeId,
                                                        ProjectId = (int)x.ProjectId,
                                                        SpecId = (double)x.SpecId,
                                                        HourType = (int)x.HourType,
                                                        HourTime = (decimal)x.HourTime,
                                                        TimePeriodId = x.TimePeriodId,
                                                        TblSpec = context.TblSpec.Include(y => y.Project).Where(y => y.SpecId == x.SpecId && y.ProjectId == x.ProjectId).FirstOrDefault(),
                                                        TblProjects = context.TblProjects.Where(y => y.ProjectId == x.ProjectId).FirstOrDefault(),
                                                        HourTypeNavigation = context.TlkpHourTypes.FirstOrDefault(y => y.HourType == x.HourType)
                                                    }).ToList();


            // Sort all the "temp" time cards by the timeid and only take the last entry for each timeid (this gives us the last edited version
            // of that timeid entry
            var temp = testTemp.GroupBy(x => x.TimeId).Select(y => y.LastOrDefault()).ToList();



            // Get all live time cards for the current pay period and employee that are not also in temp
            var timeCardEntries = context.TblTimecards.Include(tc => tc.TblSpec)
                                                    .Include(tc => tc.TblProjects)
                                                    .Include(tc => tc.HourTypeNavigation)
                                                    .Where(x => x.TimePeriodId == activePayPeriod && x.EmployeeId == empId
                                                    && !temp.Any(y => y.TimeId == x.TimeId)).ToList();

            // Add the temp and live entries together that are not in the delete list. These are now the current active time cards and in order
            // by date
            var allEntries = timeCardEntries.Concat(temp).OrderBy(x => x.TimeDate).Where(x => !deletedTimeIds.Contains(x.TimeId)).ToList();

            if (actionDone != null)
            {
                ViewData["actionDone"] = actionDone;
            }

            return View("Index", allEntries);
        }

        public IActionResult EditTimeCard(int id)
        {
            if (id == 0)
            {
                return View("Index");
            }
            // if the TimeId exists in the temp table
            bool inTempTable = context.TblTempTimecards.Any(x => x.TimeId == id);
            var timecard = new TblTimecards();

            if (inTempTable)
            {
                // if TimeId is in the table, grab the most recent one and assign that info to the timecard
                var tempcard = context.TblTempTimecards.Include(x => x.Spec).Include(x => x.Project).Include(x => x.HourTypeNavigation).Where(x => x.TimeId == id).GroupBy(x => x.TimeId).Select(x => x.LastOrDefault()).FirstOrDefault();

                timecard.TimeId = tempcard.TimeId;
                timecard.TimeDate = tempcard.TimeDate;
                timecard.EmployeeId = tempcard.EmployeeId;
                timecard.ProjectId = tempcard.ProjectId;
                timecard.SpecId = tempcard.SpecId;
                timecard.HourType = tempcard.HourType;
                timecard.HourTime = tempcard.HourTime;
                timecard.TimePeriodId = tempcard.TimePeriodId;
                timecard.TblSpec = tempcard.Spec;
                timecard.TblProjects = tempcard.Project;
                timecard.HourTypeNavigation = tempcard.HourTypeNavigation;
            }
            else
            {
                // if TimeId is not in the temp table, grab it from the live table and assign to timecard
                timecard = context.TblTimecards.Include(tc => tc.TblSpec)
                                                    .Include(tc => tc.TblProjects)
                                                    .Include(tc => tc.HourTypeNavigation)
                                                    .Where(x => x.TimeId == id).FirstOrDefault();
            }


            // Convert the timecard to an editVM that has all the necessary stuff to view the edit page
            EditViewModel editModel = new EditViewModel();
            editModel.TimeCard = timecard;
            // Get the start and end dates for the pay period to then populate all the dates for the pay period
            var payPeriodDates = context.TblTimecardHeader.Where(x => x.Validated == false).Select(x => new { x.PayPeriodStartDate, x.PayPeriodEndDate }).ToList();
            editModel.DateRange = editModel.PopulateDatesInPayPeriod(payPeriodDates[0].PayPeriodStartDate, payPeriodDates[0].PayPeriodEndDate);

            // Populate the select list items with the projects, stations, and labor codes from the database and assign to VM
            var projects = context.TblProjects.Select(x => new SelectListItem
            {
                Text = $@"{ x.ProjectId.ToString() } - { x.Pdescription.Substring(0, Math.Min(x.Pdescription.ToString().Length, 50)) }",
                Value = x.ProjectId.ToString()
            }).ToList();


            var stations = context.TblSpec.Where(x => x.ProjectId == editModel.TimeCard.ProjectId).Select(x => new SelectListItem
            {
                Text = $@"{ x.SpecId.ToString() } - { x.Sdescription }",
                Value = x.SpecId.ToString()
            }).ToList();

            var hours = context.TlkpHourTypes.Select(x => new SelectListItem
            {
                Text = $@"{ x.HourType.ToString() } - { x.HourDescription }",
                Value = x.HourType.ToString()
            }).ToList();

            editModel.Projects = projects;
            editModel.Stations = stations;
            editModel.LaborCodes = hours;

            return PartialView("EditTimeCard", editModel);
        }

        public IActionResult PopulateStations(int id)
        {
            // Returns Json of select list items for all the stations associated with the projectid (id)
            return Json(context.TblSpec.Where(x => x.ProjectId == id).Select(x => new SelectListItem
            {
                Text = $@"{ x.SpecId.ToString() } - { x.Sdescription }",
                Value = x.SpecId.ToString()
            }).ToList());
        }

        public IActionResult Save(EditViewModel editModel)
        {

            // Get the timecard from the edit VM 
            TblTimecards timecard = editModel.TimeCard;

            // Check if the original entry is in the temp table - if so, grab the last updated one
            var tempcard = context.TblTempTimecards.Where(x => x.TimeId == timecard.TimeId).GroupBy(x => x.TimeId).Select(x => x.LastOrDefault()).ToList();

            bool modified = false;

            // If tempcard is empty, then the original entry is not in the temp table but in the live table
            if (tempcard.Count == 0)
            {
                var tmcard = context.TblTimecards.Find(timecard.TimeId);
                modified = Modified(tmcard, timecard);
            }
            else
            {
                // Should probably do something here if the entry for the timecard is marked for delete
                if (tempcard[0].Delete)
                {
                    return View("Index");
                }

                modified = Modified(tempcard[0], timecard);
            }


            // If there have been changes from what's in the database, update it and add to the temp table
            if (modified)
            {
                TblTempTimecards editToTemp = new TblTempTimecards
                {
                    EmployeeId = 77,
                    TimeId = timecard.TimeId,
                    TimePeriodId = (int)timecard.TimePeriodId,
                    TimeDate = timecard.TimeDate,
                    ProjectId = timecard.ProjectId,
                    SpecId = timecard.SpecId,
                    HourTime = timecard.HourTime,
                    HourType = timecard.HourType,
                    OnRoad = false,
                    EditedDate = DateTime.UtcNow,
                    Edit = true
                };

                context.TblTempTimecards.Add(editToTemp);
                context.SaveChanges();
                return RedirectToAction("Index", new { actionDone = "edit" });
            }


            return RedirectToAction("Index");
        }

        public IActionResult DeleteConfirmation(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            // if the TimeId exists in the temp table
            bool inTempTable = context.TblTempTimecards.Any(x => x.TimeId == id);

            if (inTempTable)
            {
                // If in the temp table, get the entry and send it to the DeleteByTempID confirmation page
                var tempcard = context.TblTempTimecards.Include(x => x.Spec).Include(x => x.Project).Include(x => x.HourTypeNavigation).Where(x => x.TimeId == id).GroupBy(x => x.TimeId).Select(x => x.LastOrDefault()).FirstOrDefault();
                return PartialView("DeleteByTempID", tempcard);

            }

            // If it's not in the temp table, it's in the live table. Get the entry and then send to DeleteByTimeID page
            var timecard = context.TblTimecards.Include(tc => tc.TblSpec)
                                                    .Include(tc => tc.TblProjects)
                                                    .Include(tc => tc.HourTypeNavigation)
                                                    .Where(x => x.TimeId == id).FirstOrDefault();

            return PartialView("DeleteByTimeID", timecard);

        }


        public IActionResult DeleteByTempID(int TempCardId)
        {
            // Grab the entry from the temp table. "Delete" entry by making an entry in the temp table with the same data and 
            // setting Delete to true
            var tempcard = context.TblTempTimecards.Find(TempCardId);

            TblTempTimecards deleteEntry = new TblTempTimecards
            {
                TimeId = tempcard.TimeId,
                EmployeeId = tempcard.EmployeeId,
                TimePeriodId = (int)tempcard.TimePeriodId,
                TimeDate = tempcard.TimeDate,
                ProjectId = tempcard.ProjectId,
                SpecId = tempcard.SpecId,
                HourType = tempcard.HourType,
                HourTime = tempcard.HourTime,
                EditedDate = DateTime.UtcNow,
                OnRoad = tempcard.OnRoad,
                Add = false,
                Edit = false,
                Delete = true
            };

            context.Add(deleteEntry);
            context.SaveChanges();


            return RedirectToAction("Index", new { actionDone = "delete" });
        }

        public IActionResult DeleteByTimeID(int TimeId)
        {
            // Grab the entry from the live table. "Delete" entry by making an entry in the temp table with the same data and 
            // setting Delete to true
            var timecard = context.TblTimecards.Include(tc => tc.TblSpec)
                                                    .Include(tc => tc.TblProjects)
                                                    .Include(tc => tc.HourTypeNavigation)
                                                    .Where(x => x.TimeId == TimeId).FirstOrDefault();

            TblTempTimecards deleteEntry = new TblTempTimecards
            {
                TimeId = timecard.TimeId,
                EmployeeId = timecard.EmployeeId,
                TimePeriodId = (int)timecard.TimePeriodId,
                TimeDate = timecard.TimeDate,
                ProjectId = timecard.ProjectId,
                SpecId = timecard.SpecId,
                HourType = timecard.HourType,
                HourTime = timecard.HourTime,
                EditedDate = DateTime.UtcNow,
                OnRoad = false,
                Add = false,
                Edit = false,
                Delete = true
            };

            context.Add(deleteEntry);
            context.SaveChanges();

            return RedirectToAction("Index", new { actionDone = "delete" });
        }

        public IActionResult Add()
        {
            return View("Index");
        }



        //////////////////////////////////////////
        ///Utility Methods //////////////////////
        ////////////////////////////////////////

        private bool Modified(TblTimecards timecard, TblTimecards original)
        {
            return (original.TimeDate != timecard.TimeDate ||
                    original.ProjectId != timecard.ProjectId ||
                    original.SpecId != timecard.SpecId ||
                    original.HourType != timecard.HourType ||
                    original.HourTime != timecard.HourTime);
        }

        private bool Modified(TblTempTimecards timecard, TblTimecards original)
        {
            return (original.TimeDate != timecard.TimeDate ||
                    original.ProjectId != timecard.ProjectId ||
                    original.SpecId != timecard.SpecId ||
                    original.HourType != timecard.HourType ||
                    original.HourTime != timecard.HourTime);
        }
    }
}
