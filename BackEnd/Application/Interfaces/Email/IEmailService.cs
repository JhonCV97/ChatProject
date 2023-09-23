using Application.Common.Response;
using Application.DTOs.Email;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Email
{
    public  interface IEmailService
    {
        void SendEmail(EmailDto request);
    }
}
