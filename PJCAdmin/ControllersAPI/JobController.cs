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
    public class JobController : ApiController
    {
        private JobHelper helper = new JobHelper();
        private Auth auth = new Auth();

        [HttpPost]
        public HttpResponseMessage UploadJob(JobModel job, string token)
        {
            auth.authorizeToken(token);
            string userName = auth.getUserNameFromToken(token);

            bool created = helper.createJob(userName, job);

            if (created)
                return Request.CreateResponse<JobModel>(HttpStatusCode.OK, job);
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The job cannot be created as is");
        }
    }
}
