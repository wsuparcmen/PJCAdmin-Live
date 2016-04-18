using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PJCAdmin.Classes.Helpers.MVCModelHelpers;
using PJCAdmin.Classes.Helpers;

namespace PJCAdmin.Controllers
{
    public class NoteController : Controller
    {
        //
        // GET: /Note/
        private AccountHelper accountHelper;
        private RoutineHelper routineHelper;

        public NoteController()
        {
            routineHelper = new RoutineHelper();
            DbHelper context = routineHelper.getDBHelper();
            accountHelper = new AccountHelper(context);
        }

        public ActionResult ListUsers(string mockUser = "")
        {
            if (!(Roles.IsUserInRole("Administrator") || Roles.IsUserInRole("Job Coach") || Roles.IsUserInRole("Parent")))
            {
                Response.Redirect("~/Unauthorized");
                return View();
            }

            if (Roles.IsUserInRole("Administrator"))
            {
                ViewData["Users"] = accountHelper.getListOfUsersInRole("User");

                return View();
            }

            string thisUsername = AccountHelper.getCurrentUsername();

            if (Roles.IsUserInRole("Job Coach"))
                ViewData["Users"] = accountHelper.getListOfUsersAssignedToJobCoach(thisUsername);

            if (Roles.IsUserInRole("Parent"))
                ViewData["Users"] = accountHelper.getListOfUsersChildOfParent(thisUsername);

            return View();
        }
    }
}
