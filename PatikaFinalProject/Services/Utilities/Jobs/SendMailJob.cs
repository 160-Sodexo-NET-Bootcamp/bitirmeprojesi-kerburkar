using Hangfire;
using Services.Utilities.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utilities.Jobs
{
    //kayıtları 5 kez deneyip çalışması için
    [AutomaticRetry(Attempts = 5)]
    public static class SendMailJob
    {
        public static void SendMailEnqueue(MailRequest mailMessageDto)
        {
            Hangfire.BackgroundJob.Enqueue<EmailSendingScheduleJobManager>
                (
                 job => job.Process(mailMessageDto)
                 );
        }
    }
}
