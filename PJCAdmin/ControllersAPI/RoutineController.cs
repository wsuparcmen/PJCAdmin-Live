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
        
        //POST api/Routine?token=<token>&create=<c,m,or d>&routineModel=<[routine object]>
        [HttpPost]
        public HttpResponseMessage PostJob(string token, string create, RoutineModel model)
        {
            auth.authorizeToken(token);
            PJCAdmin.Classes.Helpers.MVCModelHelpers.RoutineHelper helper = new Classes.Helpers.MVCModelHelpers.RoutineHelper();
            switch (create)
            {
                case "c": //create new job
                    //create the model in the database
                    helper.createRoutine(model);
                    break;

                case "m": //modify existing job
                    //find the routine that matches
                    //update the database for routine
                    helper.updateRoutine(model.routineTitle, model);
                    break;

                case "d": //delete existing job
                    //find the routine that matches
                    //delete the matching routine from the database
                    helper.deleteRoutine(model.routineTitle, auth.getUserNameFromToken(token), false);
                    return Request.CreateResponse<string>(HttpStatusCode.OK, "Job Deleted");

                default: return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Create Code Not Recognized");
            }

            return Request.CreateResponse<string>(HttpStatusCode.Created, "Job Created");
        }

        protected override void Dispose(bool disposing)
        {
            helper.dispose();
            base.Dispose(disposing);
        }

        public IEnumerable<string> GetRoutineInfo(string token, string username, string routineTitle)
        {
            List<string> routineInfo = new List<string>();
            if (username == null) return null;
            auth.authorizeToken(token);
            //UserInfoModel temp = helper.getRoutineInfo(username);
            //userInfo.Add(temp.userName);
            //userInfo.Add(temp.phone);
            //userInfo.Add(temp.email);


            Routine temp = helper.getRoutineAssignedToByName(routineTitle);
            routineInfo.Add(temp.routineID.ToString());
            routineInfo.Add(temp.creatorUserName);
            routineInfo.Add(temp.assigneeUserName);
            routineInfo.Add(temp.routineTitle);
            routineInfo.Add(temp.isTimed.ToString());
            routineInfo.Add(temp.expectedDuration.ToString());
            routineInfo.Add(temp.isNotifiable.ToString());
            routineInfo.Add(temp.updatedDate.ToString());
            routineInfo.Add(temp.isDisabled.ToString());

            return routineInfo;
        }
    }
}
