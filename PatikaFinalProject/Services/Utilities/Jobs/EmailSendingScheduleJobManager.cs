using Services.Utilities.Services;
using Services.Utilities.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utilities.Jobs
{
    public class EmailSendingScheduleJobManager
    {
        private readonly IMailService _mailService;

        public EmailSendingScheduleJobManager(IMailService mailService)
        {
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        public async Task Process(MailRequest mailMessageDto)
        {
            await _mailService.SendEmailAsync(mailMessageDto);
        }
    }
}
