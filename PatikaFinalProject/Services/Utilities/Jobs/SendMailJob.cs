using Hangfire;
using Services.Utilities.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utilities.Jobs
{
    //kayıtları 5 kez deneyip çalışması için;
    [AutomaticRetry(Attempts = 5)]
    public static class SendMailJob
    {
        //hangfire üzerinden mail işi kuyruğa gönderilir. (fireAndForget)
        public static void SendMailEnqueue(MailRequest mailRequest)
        {
            //mail detayları mailRequest üzerinden EmailSendingScheduleJobManager'a gider.
            Hangfire.BackgroundJob.Enqueue<EmailSendingScheduleJobManager>
                (
                 job => job.Process(mailRequest)
                 );
        }
    }
}
