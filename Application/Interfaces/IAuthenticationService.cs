
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Application.Core;

namespace Application.Interfaces
{
    public interface IAuthenticationService
    {  
        Result<string> GetOTPKey(string userid);
        Result<string> GenerateQrCode(string userid, string otpKey);
        Result<string> ValidateOTP(string otpcode, string otpKey);

    }
}
