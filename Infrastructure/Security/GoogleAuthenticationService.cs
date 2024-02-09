using Application.Core;
using Application.Core.Logging;
using Application.Interfaces;
//using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Authenticator;
using IronBarCode;
using System.IO;
//using NetTopologySuite.Triangulate.Tri;


namespace Infrastructure.Security
{
    public class GoogleAuthenticationService : IAuthenticationService
    {
        public Result<string> GetOTPKey(string userid)
        {

            string otpKey = userid + DateTime.Now.ToString("fffssmmHHddMMyy");

            return Result<string>.Success(otpKey);
        }

        public Result<string> GenerateQrCode(string userid, string otpKey)
        {
            TwoFactorAuthenticator TwoFA = new TwoFactorAuthenticator();

            string myAccount = "KENLINK(" + userid + ")";
            string mySecretKey = otpKey;

            SetupCode mySetup_Info = TwoFA.GenerateSetupCode(myAccount, mySecretKey, 300, 300); //"300",true ,300);

            string myUrl = Uri.UnescapeDataString(mySetup_Info.QrCodeSetupImageUrl);

            myUrl = myUrl.Substring(myUrl.IndexOf("otpauth"));

            GeneratedBarcode Qrcode = IronBarCode.QRCodeWriter.CreateQrCode(myUrl, 300, QRCodeWriter.QrErrorCorrectionLevel.Low);

            string image_string = Convert.ToBase64String(Qrcode.Image.GetBytes());

            var ImageUrl = "data:image/jpeg;base64," + image_string;

            var f = Qrcode.Image;  //File(Qrcode, "image/png");

            return Result<string>.Success(ImageUrl);
        }

        public Result<string> ValidateOTP(string otpcode, string otpKey)
        {
            otpcode = otpcode.Trim();
            TwoFactorAuthenticator TwoFA = new TwoFactorAuthenticator();
            if (TwoFA.ValidateTwoFactorPIN(otpKey, otpcode) == false){

                return Result<string>.Failure("OTP code validation failed.");

            }
           
            return Result<string>.Success("Success");

        }

    }
}
