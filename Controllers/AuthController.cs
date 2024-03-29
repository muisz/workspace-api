using Microsoft.AspNetCore.Mvc;
using WorkspaceAPI.Data;
using WorkspaceAPI.Enums;
using WorkspaceAPI.Models;
using WorkspaceAPI.Services;

namespace WorkspaceAPI.Controllers
{
    [Route("/api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IOTPService _otpService;
        private readonly IEmailService _emailService;

        public AuthController(IAuthService authService, IOTPService otpService, IEmailService emailService)
        {
            _authService = authService;
            _otpService = otpService;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public ActionResult<PostRegisterResponse> PostRegister(AuthRegister payload)
        {
            try
            {
                User user = _authService.Register(payload);
                return StatusCode(StatusCodes.Status201Created, new PostRegisterResponse{ Id = user.Id });
            }
            catch (Exception error)
            {
                return Problem(error.Message, statusCode: 400);
            }
        }

        [HttpPost("login")]
        public ActionResult<PostLoginResponse> PostLogin(AuthLogin payload)
        {
            try
            {
                User user = _authService.Authenticate(payload.Email, payload.Password);
                if (!user.EmailVerified)
                    throw new Exception("Email not verified");
                
                return Ok(new PostLoginResponse { 
                    Id = user.Id,
                    Name = user.Name,
                    Username = user.Username,
                    LastActive = user.LastActive
                });
            }
            catch (Exception error)
            {
                return Problem(error.Message, statusCode: StatusCodes.Status400BadRequest);
            }
        }

        [HttpPost("otp-registration/send")]
        public ActionResult PostSendOTPRegistration(SendOTPRegistration payload)
        {
            User? user = _authService.GetUserByEmail(payload.Email);
            if (user == null)
                return Problem("Email not found.", statusCode: StatusCodes.Status404NotFound);
            
            if (user.EmailVerified)
                return Problem("Email already verified", statusCode: StatusCodes.Status400BadRequest);
            
            OTP otp = _otpService.CreateOTP(OTPUsage.Registration, payload.Email);
            _emailService.SendOTPRegistration(otp, user);
            return Ok();
        }

        [HttpPost("otp-registration/check")]
        public ActionResult<PostCheckOTPResponse> PostCheckOTPRegistration(EmailOTP payload)
        {
            try
            {
                OTP? otp = _otpService.GetOTP(OTPUsage.Registration, payload.Email, payload.Code);
                if (otp == null)
                    throw new Exception("OTP not found");
                
                _otpService.IsValid(otp);
                return Ok(new PostCheckOTPResponse { Valid = true, Message = "OTP valid." });
            }
            catch (Exception error)
            {
                return BadRequest(new PostCheckOTPResponse { Valid = false, Message = error.Message });
            }
        }

        [HttpPost("verify-email")]
        public ActionResult<Token> PostVerifyEmail(EmailOTP payload)
        {
            try
            {
                OTP? otp = _otpService.GetOTP(OTPUsage.Registration, payload.Email, payload.Code);
                if (otp == null)
                    throw new Exception("OTP not found.");
                
                _otpService.IsValid(otp);

                User? user = _authService.GetUserByEmail(payload.Email);
                if (user == null)
                    throw new Exception("Email not found.");
                
                _authService.VerifyEmail(user);
                _otpService.Deactivate(otp);

                Token token = new Token
                {
                    Access = "",
                    Refresh = "",
                };
                return Ok(token);
            }
            catch (Exception error)
            {
                return Problem(error.Message, statusCode: StatusCodes.Status400BadRequest);
            }
        }
    }

}