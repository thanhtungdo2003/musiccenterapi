using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicBusniess;
using MusicCenterAPI.Data;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MusicCenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private IMusicCenterAPI api;
        public CommentsController(IMusicCenterAPI api)
        {
            this.api = api;
        }
        [HttpGet("{rid}")]
        public IActionResult getCommetsByRecordUid(string rid)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@RecordUid", System.Data.SqlDbType.NVarChar){Value = rid}
            };
            return Ok(api.CoventToDictionarysWithDataTable(api.ProcedureCall(ProcedureStorage.ProcedureName.GET_COMMENTS_BY_RECORDUID, parameters)));
        }
        [HttpPost("comment/record_uid={rid}session_login_uid={session_login_uid}content={content}")]
        public IActionResult comment(string rid, string session_login_uid, string content)
        {
            if (content == "")
            {
                return BadRequest();
            }
            if (Guid.TryParse(rid, out Guid recordUid))
            {
                RecordData record = new RecordData(api, recordUid.ToString());
                if (record.RecordUid != null)
                {
                    var principal = api.DecodeToken(session_login_uid);

                    string userName = principal.FindFirst(ClaimTypes.Name)?.Value;
                    if (userName != null)
                    {
                        CommentsData comments = new CommentsData(api)
                        {
                            CommentUid = Guid.NewGuid(),
                            Content = content,
                            UserName = userName,
                            RecordUid = recordUid,

                        };
                        comments.Save();
                        return Ok();
                    }
                }
            }
            return BadRequest();
        }
    }
}
