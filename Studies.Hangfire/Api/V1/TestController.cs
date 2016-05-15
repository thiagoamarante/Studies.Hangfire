using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Hangfire;

namespace Studies.Hangfire.Api.V1
{
    [RoutePrefix("api/v1/test")]
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("simple")]
        public HttpResponseMessage Simples()
        {
            string result = BackgroundJob.Enqueue(() => System.Diagnostics.Trace.WriteLine("simple"));
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("delay")]
        public HttpResponseMessage Delay()
        {
            string result = BackgroundJob.Schedule(() => System.Diagnostics.Trace.WriteLine("delay"),TimeSpan.FromSeconds(5));
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("addrecurring/{name}")]
        public HttpResponseMessage AddRecurring(string name)
        {
            RecurringJob.AddOrUpdate(() => System.Diagnostics.Trace.WriteLine("recurring - " + name), Cron.Minutely());
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("manager")]
        public HttpResponseMessage Manager()
        {
            var manager = new RecurringJobManager();
            
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
