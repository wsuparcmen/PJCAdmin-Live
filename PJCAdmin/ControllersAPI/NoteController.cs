using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PJCAdmin.Classes.Helpers.APIModelHelpers;
using PJCAdmin.Models;

namespace PJCAdmin.ControllersAPI
{
    public class NoteController : ApiController
    {
        private UserNoteHelper helper;
        private Auth auth;

        public NoteController()
        {
            helper = new UserNoteHelper();
            auth = new Auth(helper.getDBHelper());
        }

        [HttpPost]
        public HttpResponseMessage UploadUserNote(NoteModel note, string token)
        {
            auth.authorizeToken(token);
            string userName = auth.getUserNameFromToken(token);

            bool created = helper.createUserNote(userName, note);

            if (created)
                return Request.CreateResponse<NoteModel>(HttpStatusCode.OK, note);
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The note cannot be created as is");
        }
    }
}
