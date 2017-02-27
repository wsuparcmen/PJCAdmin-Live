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
    public class JobCoachController : ApiController
    {
        private UserInfoHelper helper;
        private Auth auth;

        public JobCoachController()
        {
            helper = new UserInfoHelper();
            DbHelper context = helper.getDBHelper();
            auth = new Auth(context);
        }
        //Unnecessary code
        /*public UserInfoModel Get(string token)
        {
            DbHelper context = helper.getDBHelper();
            auth = new Auth(context);
            auth.authorizeToken(token);
            string userName = auth.getUserNameFromToken(token);

            return helper.getJobCoachInfo(userName);
        }*/

        //GET ../api/JobCoach?token=<token>
        public IEnumerable<UserInfoModel> GetUserList(string token)
        {
            auth.authorizeToken(token);
            string userName = auth.getUserNameFromToken(token);

            var userList = helper.getListOfUsersAssignedToCoach(userName);
            return userList;
        }

        //GET ../api/JobCoach?token=<token>&username=<username>
        public UserInfoModel GetUserInfo(string token, string username)
        {
            auth.authorizeToken(token);
            UserInfoModel userInfo = helper.getUserInfo(username);
            return userInfo;
        }
    }
}
