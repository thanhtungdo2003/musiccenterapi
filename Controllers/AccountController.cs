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
        [HttpPost("sginin/username={userName}&password={password}")]
        public IActionResult SignIn(string userName, string password)
        {
            if (api.getValueByKey(DatabaseStruct.AccountTable, "UserName", "UserName", userName) != null) return Conflict("Tài khoản đã tồn tại.");
            var accountData = new AccountData(api)
            {
                userName = userName,
                hashPass = MusicCenterAPI.ComputeSha256Hash(password),
                Status = "ACTIVE",
                signInDate = DateTime.Today
            };
            accountData.Save();
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(10),
                Secure = true,
                SameSite = SameSiteMode.None
            };
            AccountData account = api.Authenticate(userName, MusicCenterAPI.ComputeSha256Hash(password));
            Response.Cookies.Append("token_login", account.token, cookieOptions);
            return Ok();
        }
        [HttpPost("login/username={userName}&password={password}")]
        public IActionResult Login(string userName, string password)
        {
            if (api.getValueByKey(DatabaseStruct.AccountTable, "UserName", "UserName", userName) == null)
            {
                return Conflict("Tài khoản không tồn tại.");
            }
            AccountData account = api.Authenticate(userName, MusicCenterAPI.ComputeSha256Hash(password));
            if (MusicCenterAPI.ComputeSha256Hash(password) == account.hashPass)
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(10),
                    Secure = true,
                    SameSite = SameSiteMode.None
                };
                Response.Cookies.Append("token_login", account.token, cookieOptions);
                return Ok();
            }
            else
            {
                return Conflict("Mật khẩu không chính xác!");
            }
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
                return Ok(principal.FindFirst(ClaimTypes.Name)?.Value);
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
