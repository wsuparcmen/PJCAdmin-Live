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
    public class ParentController : ApiController
    {
        private UserInfoHelper helper;
        private Auth auth;

        public ParentController()
        {
            helper = new UserInfoHelper();
            DbHelper context = helper.getDBHelper();
            auth = new Auth(context);
        }

        public UserInfoModel Get(string token)
        {
            auth.authorizeToken(token);
            string userName = auth.getUserNameFromToken(token);

            return helper.getParentInfo(userName);
        }
    }
}
