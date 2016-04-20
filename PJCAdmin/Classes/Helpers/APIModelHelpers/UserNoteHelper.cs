using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PJCAdmin.Models;
using PJCAdmin.Classes.Helpers.MVCModelHelpers;

namespace PJCAdmin.Classes.Helpers.APIModelHelpers
{
    public class UserNoteHelper
    {
        private DbHelper helper;
        private AccountHelper accountHelper;

        public UserNoteHelper(DbHelper h = null)
        {
            if (h == null)
                helper = new DbHelper();
            else
                helper = h;

            accountHelper = new AccountHelper(helper);
        }

        public DbHelper getDBHelper()
        {
            return helper;
        }

        public bool createUserNote(string creatorUserName, NoteModel note)
        {
            if (!accountHelper.userExists(creatorUserName))
                return false;

            Note n = new Note()
            {
                noteTitle = note.noteTitle,
                noteMessage = note.noteMessage
            };

            helper.createUserNote(creatorUserName, n);

            return true;
        }
    }
}