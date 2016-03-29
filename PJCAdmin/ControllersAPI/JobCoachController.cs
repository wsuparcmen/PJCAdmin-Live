using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PJCAdmin.Models;
using PJCAdmin.Classes.Helpers.APIModelHelpers;

namespace PJCAdmin.ControllersAPI
{
    public class JobCoachController : ApiController
    {
        private UserInfoHelper helper = new UserInfoHelper();
        private Auth auth = new Auth();

        public UserInfoModel Get(string token)
        {
            auth.authorizeToken(token);
            string userName = auth.getUserNameFromToken(token);

            return helper.getJobCoachInfo(userName);
        }
    }
}
