using Services.Utilities.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utilities.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
