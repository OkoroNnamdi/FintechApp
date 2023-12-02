using FinTechCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTechCore.IService
{
    public interface IEmailService
    {
        Task SendEmailMassageAsync(EmailMessage message);
    }
}
