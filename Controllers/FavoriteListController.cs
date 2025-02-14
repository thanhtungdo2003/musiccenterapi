using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicBusniess;
using MusicCenterAPI.Data;
using MusicCenterAPI.ProcedureStorage;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MusicCenterAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteListController : ControllerBase
    {
        private IMusicCenterAPI api;
        public FavoriteListController(IMusicCenterAPI api)
        {
            this.api = api;
        }
        [HttpGet("get/{sid}")]
        public IActionResult get(string sid)
        {
            var par = api.DecodeToken(sid);
            string? userNamer = par.FindFirst(ClaimTypes.Name)?.Value;
            if (userNamer != null)
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                        new SqlParameter("@UserName", System.Data.SqlDbType.NVarChar){ Value = userNamer}
                };
                return Ok(api.CoventToDictionarysWithDataTable(api.ProcedureCall(ProcedureName.GET_FAVORITELIST_BY_USERNAME, parameters)));
            }
            return NotFound();
        }
        [HttpPost("add/session_login_uid={sid}record_uid={rid}")]
        public IActionResult add(string sid, string rid)
        {
            var par = api.DecodeToken(sid);
            string? userNamer = par.FindFirst(ClaimTypes.Name)?.Value;
            if (userNamer != null)
            {
                if (Guid.TryParse(rid, out Guid recordUid))
                {
                    SqlParameter[] parameters = new SqlParameter[] {
                            new SqlParameter("@UserName", System.Data.SqlDbType.NVarChar){Value=userNamer },
                            new SqlParameter("@RecordUid", System.Data.SqlDbType.NVarChar){Value=rid }
                        };
                    api.ProcedureCall(ProcedureName.ADD_TO_FAVORITELIST, parameters);
                    return Ok();
                }
            }
            return NotFound();
        }
        [HttpDelete("remove/session_login_uid={sid}record_uid={rid}")]
        public IActionResult remove(string sid, string rid)
        {
            var par = api.DecodeToken(sid);
            string? userNamer = par.FindFirst(ClaimTypes.Name)?.Value;
            if (userNamer != null)
            {
                if (userNamer != null)
                {
                    if (Guid.TryParse(rid, out Guid recordUid))
                    {
                        SqlParameter[] parameters = new SqlParameter[] {
                            new SqlParameter("@UserName", System.Data.SqlDbType.NVarChar){Value=userNamer },
                            new SqlParameter("@RecordUid", System.Data.SqlDbType.NVarChar){Value=rid }
                        };
                        api.ProcedureCall(ProcedureName.REMOVE_RECORD_FROM_FAVORITE, parameters);
                        return Ok();
                    }
                }
            }
            return NotFound();
        }
    }
}
