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

            if (un.Notes.Count() == 0)
                return new List<Note>();

            return un.Notes.ToList();
        }

        public List<Note> getJobNotes(string userName)
        {
            UserName un = helper.findUserName(userName);
            List<Note> lst = new List<Note>();

            foreach (Routine r in un.Routines)
            {
                foreach (Job j in r.Jobs)
                {
                    lst.AddRange(j.Notes);
                }
            }

            return lst;
        }

        public List<Note> getStepNotes(string userName)
        {
            UserName un = helper.findUserName(userName);
            List<Note> lst = new List<Note>();

            foreach (Routine r in un.Routines)
            {
                foreach (Job j in r.Jobs)
                {
                    foreach (Step s in j.Steps)
                    {
                        lst.AddRange(s.Notes);
                    }
                }
            }

            return lst;
        }
    }
}