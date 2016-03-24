using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using PJCAdmin.Models;

namespace PJCAdmin.Classes.Helpers.APIModelHelpers
{
    public class UserInfoHelper
    {
        private DbHelper helper = new DbHelper();

        public UserInfoModel getJobCoachInfo(string username)
        {
            return getUserInfo(helper.findUserName(username).jobCoachUserName);
        }
        public UserInfoModel getParentInfo(string username)
        {
            return getUserInfo(helper.findUserName(username).guardianUserName);
        }
        public UserInfoModel getUserInfo(string username)
        {
            MembershipUser user = System.Web.Security.Membership.GetUser(username);
            ProfileBase profile = ProfileBase.Create(username);

            UserInfoModel info = new UserInfoModel();
            info.userName = username;
            info.phone = profile.GetPropertyValue("PhoneNumber").ToString();
            info.email = user.Email;

            return info;
        }
    }
}