using Hangfire;
using HangFireBackgroundServices.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangFireBackgroundServices.Controllers
{
    [ApiController]
    [Route("api/v1/HangFire")]
    public class HangFireController : ControllerBase
    {
        [HttpPost]
        [Route("FireAndForget")]
        public IActionResult FireAndForgetJob()
        {
            var jobId = BackgroundJob.Enqueue(() => SendEmail.SendWelcomeEmail("anu8112@mailinator.com", "Test email", "Hello, this is a test email!"));
            return Ok($"JOB ID: {jobId} Welcome Email Send to the User!");
        }

        [HttpPost]
        [Route("DelayedJob")]
        public IActionResult DelayedJob()
        {
            var jobId = BackgroundJob.Schedule(() => SendEmail.SendWelcomeEmail("anu8112@mailinator.com", "Test email", "Hello, this is a test email!"), TimeSpan.FromMinutes(10));
            return Ok($"JOB ID: {jobId} Discount Email Send to the User!");
        }

        [HttpPost]
        [Route("RecurrentJob")]
        public IActionResult RecurrentJob()
        {
            RecurringJob.AddOrUpdate(() => Console.WriteLine("This Message from Recurring Job API"), Cron.Minutely);
            return Ok("Recurring Job Initiated");
        }

        [HttpPost]
        [Route("ContinuousJob")]
        public IActionResult ContinuousJob()
        {
            var ParentJobId= BackgroundJob.Schedule(() => SendEmail.SendWelcomeEmail("anu8112@mailinator.com", "Test email", "Hello, this is a test email!"), TimeSpan.FromMinutes(10));
            BackgroundJob.ContinueJobWith(ParentJobId, () => Console.WriteLine("You were Unsubscribed"));

            return Ok($"Continuous Job Created with ParentJobId : {ParentJobId}");
        }
    }
}
