using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PJCAdmin.Classes.Helpers.MVCModelHelpers;
using PJCAdmin.Classes.Helpers;
using PJCAdmin.Models;

namespace PJCAdmin.Controllers
{
    public class NoteController : Controller
    {
        //
        // GET: /Note/
        private AccountHelper accountHelper;
        private RoutineHelper routineHelper;
        private NoteHelper helper;

        public NoteController()
        {
            routineHelper = new RoutineHelper();
            DbHelper context = routineHelper.getDBHelper();
            accountHelper = new AccountHelper(context);
            helper = new NoteHelper(context);
        }

        public ActionResult ListUsers()
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

        public ActionResult List(string user)
        {
            if (!(Roles.IsUserInRole("Administrator") || Roles.IsUserInRole("Job Coach") || Roles.IsUserInRole("Parent")))
            {
                Response.Redirect("~/Unauthorized");
                return View();
            }

            if (!(Roles.IsUserInRole("Administrator") || accountHelper.isThisUserUsersParent(user) || accountHelper.isThisUserUsersJobCoach(user)))
            {
                Response.Redirect("~/Unauthorized");
                return View();
            }

            if (!accountHelper.userExists(user) || !Roles.IsUserInRole(user,"User"))
                return HttpNotFound();

            ViewData["UserNotes"] = helper.getUserNotes(user);
            ViewData["JobNotes"] = helper.getJobNotes(user);
            ViewData["StepNotes"] = helper.getStepNotes(user);
            ViewData["user"] = user;

            return View();
        }

        public ActionResult UserNoteDetails(string user, byte noteID)
        {
            if (!(Roles.IsUserInRole("Administrator") || Roles.IsUserInRole("Job Coach") || Roles.IsUserInRole("Parent")))
            {
                Response.Redirect("~/Unauthorized");
                return View();
            }

            if (!(Roles.IsUserInRole("Administrator") && !accountHelper.isThisUserUsersJobCoach(user) && !accountHelper.isThisUserUsersParent(user)))
            {
                Response.Redirect("~/Unauthorized");
                return View();
            }

            Note note = helper.getNote(noteID);
            if (note == null)
                return HttpNotFound();

            if (!helper.createdByUser(note, user))
                return HttpNotFound();

            if (!helper.isUserNote(note))
                return HttpNotFound();

            ViewData["user"] = user;

            return View(note);
        }

        public ActionResult JobNoteDetails(string user, byte noteID)
        {
            if (!(Roles.IsUserInRole("Administrator") || Roles.IsUserInRole("Job Coach") || Roles.IsUserInRole("Parent")))
            {
                Response.Redirect("~/Unauthorized");
                return View();
            }

            if (!(Roles.IsUserInRole("Administrator") && !accountHelper.isThisUserUsersJobCoach(user) && !accountHelper.isThisUserUsersParent(user)))
            {
                Response.Redirect("~/Unauthorized");
                return View();
            }

            Note note = helper.getNote(noteID);
            if (note == null)
                return HttpNotFound();

            if (!helper.createdByUser(note, user))
                return HttpNotFound();

            if (!helper.isJobNote(note))
                return HttpNotFound();

            ViewData["user"] = user;

            return View(note);
        }

        public ActionResult StepNoteDetails(string user, byte noteID)
        {
            if (!(Roles.IsUserInRole("Administrator") || Roles.IsUserInRole("Job Coach") || Roles.IsUserInRole("Parent")))
            {
                Response.Redirect("~/Unauthorized");
                return View();
            }

            if (!(Roles.IsUserInRole("Administrator") && !accountHelper.isThisUserUsersJobCoach(user) && !accountHelper.isThisUserUsersParent(user)))
            {
                Response.Redirect("~/Unauthorized");
                return View();
            }

            Note note = helper.getNote(noteID);
            if (note == null)
                return HttpNotFound();

            if (!helper.createdByUser(note, user))
                return HttpNotFound();

            if (!helper.isStepNote(note))
                return HttpNotFound();

            ViewData["user"] = user;

            return View(note);
        }

        public ActionResult Delete(string user, byte noteID)
        {
            if (!(Roles.IsUserInRole("Administrator") || Roles.IsUserInRole("Job Coach") || Roles.IsUserInRole("Parent")))
            {
                Response.Redirect("~/Unauthorized");
                return View();
            }

            if (!(Roles.IsUserInRole("Administrator") && !accountHelper.isThisUserUsersJobCoach(user) && !accountHelper.isThisUserUsersParent(user)))
            {
                Response.Redirect("~/Unauthorized");
                return View();
            }

            Note note = helper.getNote(noteID);
            if (note == null)
                return HttpNotFound();

            if (!helper.createdByUser(note, user))
                return HttpNotFound();

            ViewData["user"] = user;

            return View(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string user, byte noteID, int nothing = 0)
        {
            if (!(Roles.IsUserInRole("Administrator") || Roles.IsUserInRole("Job Coach") || Roles.IsUserInRole("Parent")))
            {
                Response.Redirect("~/Unauthorized");
                return View();
            }

            if (!(Roles.IsUserInRole("Administrator") && !accountHelper.isThisUserUsersJobCoach(user) && !accountHelper.isThisUserUsersParent(user)))
            {
                Response.Redirect("~/Unauthorized");
                return View();
            }

            Note note = helper.getNote(noteID);
            if (note == null)
                return HttpNotFound();

            if (!helper.createdByUser(note, user))
                return HttpNotFound();

            helper.deleteNote(note);

            Response.Redirect("~/Note/List?user=" + user);
            return View();
        }
    }
}
