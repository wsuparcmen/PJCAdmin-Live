using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PJCAdmin.Models;
using PJCAdmin.Classes.Helpers;
using PJCAdmin.Classes.Helpers.APIModelHelpers;

namespace PJCAdmin.ControllersAPI
{
    public class RoutineController : ApiController
    {
        private RoutineHelper helper;
        private Auth auth;

        public RoutineController()
        {
            helper = new RoutineHelper();
            DbHelper context = helper.getDBHelper();
            auth = new Auth(context);
        }

        // GET api/Routine?token=<token>
        public IEnumerable<Routine> Get(string token)
        {
            auth.authorizeToken(token);
            string userName = auth.getUserNameFromToken(token);
            
            return helper.getAllRoutinesAssignedToUserForSerialization(userName);
        }

        // GET api/Routine?token=<token>&assignedBy=<"Parent" or "Job Coach">
        /*public IEnumerable<Routine> Get(string token, string assignedBy)
        {
            auth.authorizeToken(token);
            string userName = auth.getUserNameFromToken(token);

            if (assignedBy.Equals("Parent"))
                return helper.getRoutinesAssignedByParentForSerialization(userName);
            if (assignedBy.Equals("Job Coach"))
                return helper.getRoutinesAssignedByJobCoachForSerialization(userName);

            //assignedBy is not a valid string
            throw new HttpResponseException(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest));
        }*/

        //GET api/Routine?token=<token>&username=<username>
        public IEnumerable<Routine> GetRoutineListForUser(string token, string username)
        {
            auth.authorizeToken(token);
            return helper.getAllRoutinesAssignedToUserForSerialization(username);
        }

        protected override void Dispose(bool disposing)
        {
            helper.dispose();
            base.Dispose(disposing);
        }

        public IEnumerable<string> GetRoutineInfo(string token, string username)
        {
            List<string> userInfo = new List<string>();
            if (username == null) return null;
            auth.authorizeToken(token);
            UserInfoModel temp = helper.getRoutineInfo(username);
            userInfo.Add(temp.userName);
            userInfo.Add(temp.phone);
            userInfo.Add(temp.email);
            return userInfo;
        }
    }
}
