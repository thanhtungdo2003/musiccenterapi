using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuscicCenter.Storage;
using MusicBusniess;
using MusicCenterAPI.Data;
using MusicCenterAPI.Pages;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Net.Mail;
using MusicCenterAPI.ProcedureStorage;
using System.Data.SqlClient;

namespace MusicCenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IMusicCenterAPI api;
        public AccountController(IMusicCenterAPI api)
        {
            this.api = api;
        }
        [HttpPost("sginin/username={userName}&email={email}&password={password}")]
        public IActionResult SignIn(string userName, string email, string password)
        {
            if (!email.Contains("@"))
            {
                return BadRequest("Email không hợp lệ!");
            }
            if (api.getStringColumns("Account", "email").Contains(email))
            {
                return BadRequest("Email đã tồn tại");

            }
            if (api.getValueByKey(DatabaseStruct.AccountTable, "UserName", "UserName", userName) != null) return Conflict("Tài khoản đã tồn tại.");
            var accountData = new AccountData(api)
            {
                UserName = userName,
                email = email,
                Password = MusicCenterAPI.ComputeSha256Hash(password),
                Status = "ACTIVE",
                JoinDay = DateTime.Today
            };
            accountData.Save();
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(10),
                Secure = true,
                SameSite = SameSiteMode.None
            };
            AccountData account = api.Authenticate(userName, MusicCenterAPI.ComputeSha256Hash(password));
            Response.Cookies.Append("token_login", account.Token, cookieOptions);
            return Ok();
        }
        [HttpPost("login/username={userName}&password={password}")]
        public IActionResult Login(string userName, string password)
        {
            AccountData accountData = new AccountData(api, userName);
            if (!accountData.Exist())
            {
                return Conflict("Tài khoản không tồn tại.");
            }
            Console.WriteLine(accountData.Status);
            if (accountData.Status == "BAN") return BadRequest(new { ban_messager = "Rất tiếc, tài khoản này đã bị cấm vô thời hạn!" });
            AccountData account = api.Authenticate(userName, MusicCenterAPI.ComputeSha256Hash(password));
            if (MusicCenterAPI.ComputeSha256Hash(password) == account.Password)
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(10),
                    Secure = true,
                    SameSite = SameSiteMode.None
                };
                Response.Cookies.Append("token_login", account.Token, cookieOptions);
                return Ok();
            }
            else
            {
                return Conflict("Mật khẩu không chính xác!");
            }
        }

        [HttpGet("login-with-gg")]
        public IActionResult LoginWithGoogle()
        {
            var redirectUrl = Url.Action("GoogleResponse", "Account");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
        [HttpGet("login-gg-res")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault()?.Claims;
            // Xử lý thông tin người dùng ở đây (email, name, picture...)
            return RedirectToAction("Index", "Home");
        }


        [HttpPost("verify-email/{to}")]
        public async Task<IActionResult> SendEmail(string to)
        {
            List<object> usernames = api.GetColumnValues("Account", "email");
            string emailToCheck = to.ToString();

            bool exists = usernames
                .Select(u => u.ToString())
                .Any(email => email.Equals(emailToCheck, StringComparison.OrdinalIgnoreCase));

            if (exists)
            {
                int verifyCode = 100000 + Random.Shared.Next(900000);
                EmailService.verifyCodes[to] = verifyCode;
                var res = await EmailService.SendGamil(to, "MusicCenter Verify Code", "Nhập mã xác nhận để đổi lại mật khẩu. \n Mã của bạn là: " + verifyCode);
                return Ok(res);
            }
            return BadRequest(new { status = 400, message = "Tài khoản không tồn tại" });
        }

        [HttpPost("verify-account/{email}&{password}&{code}")]
        public async Task<IActionResult> verify(string email, string password, int code)
        {
            if (email == null || !email.Contains("@"))
            {
                return BadRequest(new { status = 400, message = "email không hợp lệ" });

            }
            if (password == null || password.Length == 0)
            {
                return BadRequest(new { status = 400, message = "mật khẩu không hợp lệ" });

            }
            List<object> usernames = api.GetColumnValues("Account", "email");
            string emailToCheck = email.ToString();

            bool exists = usernames
                .Select(u => u.ToString())
                .Any(email => email.Equals(emailToCheck, StringComparison.OrdinalIgnoreCase));

            if (exists)
            {
                if (EmailService.verifyCodes.ContainsKey(email))
                {
                    if (EmailService.verifyCodes[email] == code)
                    {

                        SqlParameter[] parameters = new SqlParameter[]
                        {
                            new SqlParameter("@email", System.Data.SqlDbType.NVarChar){Value=email},
                            new SqlParameter("@hasspass", System.Data.SqlDbType.NVarChar){Value=MusicCenterAPI.ComputeSha256Hash(password)}
                        };
                        api.ProcedureCall(ProcedureName.UPDATE_PASSWORD, parameters);

                        return Ok(new { status = 200, message = "đổi mật khẩu thành công" });
                    }
                }
            }
            return BadRequest(new { status = 400, message = "xác nhận thất bại" });
        }

        [HttpGet("token_login")]
        public IActionResult HasSessionLogin()
        {

            if (Request.Cookies["token_login"] != null)
            {
                string? token = Request.Cookies["token_login"];
                if (token == null) return Conflict("Phiên đăng nhập đã hết hạn!");
                var principal = api.DecodeToken(token);
                if (principal == null) return BadRequest();
                return Ok(new
                {
                    username = principal.FindFirst(ClaimTypes.Name)?.Value,
                    premium_ex = principal.FindFirst("PremiumEx")?.Value
                });
            }
            return Conflict("Phiên đăng nhập đã hết hạn!");

        }

        [HttpPost("visit/add/{userName}")]
        public IActionResult visit(string userName)
        {

            var account = new AccountData(api, userName);
            if (account == null)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPost("visit/logout/{id}")]
        public IActionResult Logout(string id)
        {
            return Ok();
        }
    }
}
