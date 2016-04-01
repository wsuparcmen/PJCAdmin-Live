using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using PJCAdmin.Models;
using PJCAdmin.Classes.Helpers;
using PJCAdmin.Classes.Helpers.MVCModelHelpers;

namespace PJCAdmin.Controllers
{
    public class JobController : Controller
    {
        private AccountHelper accountHelper;
        private RoutineHelper routineHelper;
        private JobHelper helper;
        private pjcEntities db = new pjcEntities();

        public JobController()
        {
            helper = new JobHelper();
            DbHelper context = helper.getDBHelper();
            accountHelper = new AccountHelper(context);
            routineHelper = new RoutineHelper(context);
        }

        //
        // GET: /Job/

        public ActionResult Index() //Lists mockUsers or redirect
        {
            if (!(Roles.IsUserInRole("Administrator") || Roles.IsUserInRole("Job Coach") || Roles.IsUserInRole("Parent")))
            {
                Response.Redirect("~/Unauthorized");
                return View();
            }

            if (!Roles.IsUserInRole("Administrator"))
                return RedirectToAction("ListRoutines", "Job");

            ViewData["JobCoaches"] = accountHelper.getListOfUsersInRole("Job Coach");
            ViewData["Parents"] = accountHelper.getListOfUsersInRole("Parent");

            return View();
        }

        public ActionResult ListRoutines(string mockUser = "")
        {
            if (!(Roles.IsUserInRole("Administrator") || Roles.IsUserInRole("Job Coach") || Roles.IsUserInRole("Parent")))
            {
                Response.Redirect("~/Unauthorized");
                return View();
            }

            if (Roles.IsUserInRole("Administrator"))
            {
                if (mockUser == null || mockUser.Equals("") || !accountHelper.userExists(mockUser))
                {
                    return RedirectToAction("Index", "Job");
                }

                ViewData["mockUser"] = mockUser;
                return View(routineHelper.getMostRecentRoutines(mockUser));
            }
            else
            {
                return View(routineHelper.getMostRecentRoutines());
            }
        }

        public ActionResult ListRoutineVersions(string routineName, string assigneeName, string mockUser = "")
        {
            if (!(Roles.IsUserInRole("Administrator") || Roles.IsUserInRole("Job Coach") || Roles.IsUserInRole("Parent")))
            {
                Response.Redirect("~/Unauthorized");
                return View();
            }

            ViewData["routineName"] = routineName;
            ViewData["assigneeName"] = assigneeName;

            if (Roles.IsUserInRole("Administrator"))
            {
                if (mockUser == null || mockUser.Equals("") || !accountHelper.userExists(mockUser))
                    return RedirectToAction("Index", "Job");

                if (!routineHelper.routineExists(mockUser, routineName, assigneeName))
                    return RedirectToAction("ListRoutines", "Job", new { mockUser = mockUser });

                ViewData["mockUser"] = mockUser;
                return View(routineHelper.getRoutinesAssignedToByName(mockUser, routineName, assigneeName));
            }
            else
            {
                if (!routineHelper.routineExists(routineName, assigneeName))
                    return RedirectToAction("ListRoutines", "Job");

                return View(routineHelper.getRoutinesAssignedToByName(routineName, assigneeName));
            }
        }

        public ActionResult ListJobs(string routineName, string assigneeName, DateTime updatedDate, string mockUser = "")
        {
            if (!(Roles.IsUserInRole("Administrator") || Roles.IsUserInRole("Job Coach") || Roles.IsUserInRole("Parent")))
            {
                Response.Redirect("~/Unauthorized");
                return View();
            }

            ViewData["routineName"] = routineName;
            ViewData["assigneeName"] = assigneeName;
            ViewData["updatedDate"] = updatedDate;

            if (Roles.IsUserInRole("Administrator"))
            {
                if (mockUser == null || mockUser.Equals("") || !accountHelper.userExists(mockUser))
                    return RedirectToAction("Index", "Job");

                if (!routineHelper.routineExists(mockUser, routineName, assigneeName))
                    return RedirectToAction("ListRoutines", "Job", new { mockUser = mockUser });

                if (!routineHelper.routineVersionExists(mockUser, assigneeName, routineName, updatedDate))
                    return RedirectToAction("ListRoutineVersions", "Job", new
                    {
                        mockUser = mockUser,
                        routineName = routineName,
                        assigneeName = assigneeName
                    });

                ViewData["mockUser"] = mockUser;
                return View(helper.getAllJobsForRoutineVersion(mockUser, assigneeName, routineName, updatedDate));
            }
            else
            {
                if (!routineHelper.routineExists(mockUser, routineName, assigneeName))
                    return RedirectToAction("ListRoutines", "Job");

                if (!routineHelper.routineVersionExists(mockUser, assigneeName, routineName, updatedDate))
                    return RedirectToAction("ListRoutineVersions", "Job", new
                    {
                        routineName = routineName,
                        assigneeName = assigneeName
                    });

                string thisUsername = AccountHelper.getCurrentUsername();
                return View(helper.getAllJobsForRoutineVersion(thisUsername, assigneeName, routineName, updatedDate));
            }
        }

        //
        // GET: /Job/Details/5

        public ActionResult Details(string routineName, string assigneeName, DateTime startDate, string mockUser = "")
        {
            if (!(Roles.IsUserInRole("Administrator") || Roles.IsUserInRole("Job Coach") || Roles.IsUserInRole("Parent")))
            {
                Response.Redirect("~/Unauthorized");
                return View();
            }

            ViewData["routineName"] = routineName;
            ViewData["assigneeName"] = assigneeName;

            if (Roles.IsUserInRole("Administrator"))
            {
                if (mockUser == null || mockUser.Equals("") || !accountHelper.userExists(mockUser))
                    return RedirectToAction("Index", "Job");

                if (!routineHelper.routineExists(mockUser, routineName, assigneeName))
                    return RedirectToAction("ListRoutines", "Job", new { mockUser = mockUser });

                if (!helper.jobExists(mockUser, assigneeName, routineName, startDate))
                    return RedirectToAction("ListRoutineVersions", "Job", new
                    {
                        mockUser = mockUser,
                        routineName = routineName,
                        assigneeName = assigneeName
                    });

                ViewData["mockUser"] = mockUser;
                return View(helper.getJob(mockUser, assigneeName, routineName, startDate));
            }
            else
            {
                string thisUsername = AccountHelper.getCurrentUsername();
                if (!routineHelper.routineExists(thisUsername, routineName, assigneeName))
                    return RedirectToAction("ListRoutines", "Job");

                if (!helper.jobExists(thisUsername, assigneeName, routineName, startDate))
                    return RedirectToAction("ListRoutineVersions", "Job", new
                    {
                        routineName = routineName,
                        assigneeName = assigneeName
                    });

                return View(helper.getJob(thisUsername, assigneeName, routineName, startDate));
            }
        }

        //
        // GET: /Job/Delete/5

        public ActionResult Delete(string routineName, string assigneeName, DateTime startDate, string mockUser = "")
        {
            if (!(Roles.IsUserInRole("Administrator") || Roles.IsUserInRole("Job Coach") || Roles.IsUserInRole("Parent")))
            {
                Response.Redirect("~/Unauthorized");
                return View();
            }

            ViewData["routineName"] = routineName;
            ViewData["assigneeName"] = assigneeName;
            ViewData["startDate"] = startDate;

            if (Roles.IsUserInRole("Administrator"))
            {
                if (mockUser == null || mockUser.Equals("") || !accountHelper.userExists(mockUser))
                    return RedirectToAction("Index", "Job");

                if (!routineHelper.routineExists(mockUser, routineName, assigneeName))
                    return RedirectToAction("ListRoutines", "Job", new { mockUser = mockUser });

                if (!helper.jobExists(mockUser, assigneeName, routineName, startDate))
                    return RedirectToAction("ListRoutineVersions", "Job", new
                    {
                        mockUser = mockUser,
                        routineName = routineName,
                        assigneeName = assigneeName
                    });

                ViewData["mockUser"] = mockUser;
                return View();
            }
            else
            {
                string thisUsername = AccountHelper.getCurrentUsername();
                if (!routineHelper.routineExists(thisUsername, routineName, assigneeName))
                    return RedirectToAction("ListRoutines", "Job");

                if (!helper.jobExists(thisUsername, assigneeName, routineName, startDate))
                    return RedirectToAction("ListRoutineVersions", "Job", new
                    {
                        routineName = routineName,
                        assigneeName = assigneeName
                    });

                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string routineName, string assigneeName, DateTime startDate, string mockUser = "")
        {
            /* TODO */
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}