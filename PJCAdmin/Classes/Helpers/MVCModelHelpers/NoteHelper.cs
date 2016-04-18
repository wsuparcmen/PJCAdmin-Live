using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PJCAdmin.Models;

namespace PJCAdmin.Classes.Helpers.MVCModelHelpers
{
    public class NoteHelper
    {
        private JobHelper jobHelper;
        private DbHelper helper;

        public NoteHelper(DbHelper h = null)
        {
            if (h == null)
                helper = new DbHelper();
            else
                helper = h;

            jobHelper = new JobHelper(helper);
        }

        public DbHelper getDBHelper()
        {
            return helper;
        }

        public List<Note> getUserNotes(string userName)
        {
            UserName un = helper.findUserName(userName);
            return helper.getUserNotes(un);            
        }

        public List<Note> getJobNotes(string userName)
        {
            UserName un = helper.findUserName(userName);
            return helper.getJobNotes(un);
        }

        public List<Note> getStepNotes(string userName)
        {
            UserName un = helper.findUserName(userName);
            return helper.getStepNotes(un);
        }

        public void deleteNote(Note note)
        {
            helper.deleteNote(note);
        }
    }
}