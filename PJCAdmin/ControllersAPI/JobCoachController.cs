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

        public UserInfoModel Get(string token)
        {
            Auth.authorizeToken(token);
            string userName = Auth.getUserNameFromToken(token);

            return helper.getJobCoachInfo(userName);
        }
    }
}
