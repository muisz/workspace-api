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
        private readonly IJWTService _jwtService;

        public AuthController(
            IAuthService authService, 
            IOTPService otpService, 
            IEmailService emailService,
            IJWTService jwtService
        )
        {
            _authService = authService;
            _otpService = otpService;
            _emailService = emailService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public ActionResult<PostRegisterResponseData> PostRegister(AuthRegisterData payload)
        {
            try
            {
                User user = _authService.Register(payload);
                return StatusCode(StatusCodes.Status201Created, new PostRegisterResponseData{ Id = user.Id });
            }
            catch (Exception error)
            {
                return Problem(error.Message, statusCode: 400);
            }
        }

        [HttpPost("login")]
        public ActionResult<PostLoginResponseData> PostLogin(AuthLoginData payload)
        {
            try
            {
                User user = _authService.Authenticate(payload.Email, payload.Password);
                if (!user.EmailVerified)
                    throw new Exception("Email not verified");
                
                return Ok(new PostLoginResponseData { 
                    Id = user.Id,
                    Name = user.Name,
                    Username = user.Username,
                    LastActive = user.LastActive,
                    Token = _jwtService.CreateTokenPair(user)
                });
            }
            catch (Exception error)
            {
                return Problem(error.Message, statusCode: StatusCodes.Status400BadRequest);
            }
        }

        [HttpPost("otp-registration/send")]
        public ActionResult PostSendOTPRegistration(SendOTPRegistrationData payload)
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
        public ActionResult<PostCheckOTPResponseData> PostCheckOTPRegistration(EmailOTPData payload)
        {
            try
            {
                OTP? otp = _otpService.GetOTP(OTPUsage.Registration, payload.Email, payload.Code);
                if (otp == null)
                    throw new Exception("OTP not found");
                
                _otpService.IsValid(otp);
                return Ok(new PostCheckOTPResponseData { Valid = true, Message = "OTP valid." });
            }
            catch (Exception error)
            {
                return BadRequest(new PostCheckOTPResponseData { Valid = false, Message = error.Message });
            }
        }

        [HttpPost("verify-email")]
        public ActionResult<TokenData> PostVerifyEmail(EmailOTPData payload)
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

                return Ok(_jwtService.CreateTokenPair(user));
            }
            catch (Exception error)
            {
                return Problem(error.Message, statusCode: StatusCodes.Status400BadRequest);
            }
        }

        [HttpPost("jwt/refresh")]
        public ActionResult<TokenData> PostRefreshJwtToken(RefreshTokenData payload)
        {
            try
            {
                string? email = _jwtService.ClaimRefreshToken(payload.Token);
                User? user = _authService.GetUserByEmail(email ?? "");
                if (user == null)
                    throw new Exception("User not found");
                
                return Ok(_jwtService.CreateTokenPair(user));
            }
            catch (Exception error)
            {
                return Problem(error.Message, statusCode: StatusCodes.Status400BadRequest);
            }
        }

        [HttpPost("jwt/verify")]
        public ActionResult<PostCheckOTPResponseData> PostVerifyJwtToken(RefreshTokenData payload)
        {
            try
            {
                string? email = _jwtService.ClaimAccessToken(payload.Token);
                if (email == null)
                    throw new Exception("Invalid token");
                
                return Ok(new PostCheckOTPResponseData { Valid = true, Message = "Token valid."});
            }
            catch (Exception error)
            {
                return BadRequest(new PostCheckOTPResponseData { Valid = false, Message = error.Message });
            }
        }
    }
}