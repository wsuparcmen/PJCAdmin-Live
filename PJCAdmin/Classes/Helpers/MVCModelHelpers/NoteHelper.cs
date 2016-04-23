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

        public Note getNote(int noteID)
        {
            return helper.getNote(noteID);
        }

        public bool createdByUser(Note note, string userName)
        {
            if (isUserNote(note))
            {
                if (userName.Equals(note.UserNames.First().userName1))
                    return true;
            }
            else if (isJobNote(note))
            {
                if (userName.Equals(note.Jobs.First().Routine.assigneeUserName))
                    return true;
            }
            else if (isStepNote(note))
            {
                if (userName.Equals(note.Steps.First().Job.Routine.assigneeUserName))
                    return true;
            }

            return false;
        }

        public bool isUserNote(Note note)
        {
            return note.UserNames.Count() > 0;
        }

        public bool isJobNote(Note note)
        {
            return note.Jobs.Count() > 0;
        }

        public bool isStepNote(Note note)
        {
            return note.Steps.Count() > 0;
        }

        public void deleteNote(Note note)
        {
            helper.deleteNote(note);
        }
    }
}