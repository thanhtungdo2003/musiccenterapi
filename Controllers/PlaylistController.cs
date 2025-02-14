using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuscicCenter.Storage;
using MusicBusniess;
using MusicCenterAPI.Data;
using MusicCenterAPI.Models;
using MusicCenterAPI.ProcedureStorage;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace MusicCenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private IMusicCenterAPI api;
        public PlaylistController(IMusicCenterAPI api)
        {
            this.api = api;
        }
        [Authorize]
        [HttpGet("{userName}")]
        public IActionResult GetAllByUserName(string userName)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserName", SqlDbType.NVarChar) { Value = userName }
            };
            DataTable playlist = api.ProcedureCall(ProcedureName.GET_PLAYLISTS_BY_USER_NAME, parameters);

            return Ok(api.CoventToDictionarysWithDataTable(playlist));
        }
        [Authorize]
        [HttpPost("userName={userName}&id={name}")]
        public IActionResult AddPlayList(string userName, string name)
        {

            PlayListData newPlayList = new PlayListData(api)
            {
                playlistUid = Guid.NewGuid(),
                playlistName = name,
                userName = userName,
            };
            newPlayList.Save();
            return Ok();
        }
        [Authorize]
        [HttpGet("record/{id}")]
        public IActionResult GetRecords(string id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@PlaylistUid", SqlDbType.NVarChar) { Value = id }
            };
            DataTable playlist = api.ProcedureCall(ProcedureName.GET_RECORDS_FROM_PLAYLIST, parameters);
            if (playlist == null) return NotFound();
            List<RecordData> datas = new List<RecordData>();
            foreach (DataRow row in playlist.Rows)
            {
                datas.Add(new RecordData(api, Guid.Parse(row["RecordUid"].ToString())));
            }
            return Ok(datas);
        }
        [Authorize]
        [HttpGet("record/{id}/amount")]
        public IActionResult GetTotalRecords(string id)
        {
            SqlParameter[] parameters = new SqlParameter[]
           {
                new SqlParameter("@Uid", SqlDbType.NVarChar) { Value = id }
           };
            DataTable playlist = api.ProcedureCall(ProcedureName.GET_AMOUNT_RECORD_IN_PLAYLIST_BY_UID, parameters);
            return Ok(api.CoventToDictionarysWithDataTable(playlist)[0]);
        }
        [Authorize]
        [HttpPost("record/add/playlistUid={pid}&recordUid={rid}")]
        public IActionResult AddRecord(string pid, string rid)
        {
            Guid pUid;
            if (Guid.TryParse(pid, out pUid))
            {
                Guid recordUid;
                if (Guid.TryParse(rid, out recordUid))
                {
                    var data = new InfoPlayListData(api)
                    {
                        playlistUid = pUid,
                        uid = recordUid
                    };
                    data.Save();
                    return Ok();
                }
            }
            return BadRequest();
        }
        [Authorize]
        [HttpPost("rename/sessionLogin={sessionUid}&username={userName}&id={pid}&content={content}")]
        public IActionResult rename(string sessionUid, string userName, string pid, string content)
        {
            var par = api.DecodeToken(sessionUid);
            string? userNamer = par.FindFirst(JwtRegisteredClaimNames.Name)?.Value;
            if (userName != null)
                {
                    if (Guid.TryParse(pid, out Guid playlistid))
                    {
                        PlayListData playList = new PlayListData(api, playlistid);
                        playList.playlistName = content;
                        playList.Save();
                        return Ok();
                    }
                }            
            return NotFound();
        }
        [Authorize]
        [HttpDelete("record/delete/playlistUid={pid}&recordUid={rid}")]
        public IActionResult removeRecord(string pid, string rid)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                        new SqlParameter("@PlaylistUid", SqlDbType.NVarChar){Value = pid},
                        new SqlParameter("@RecordUid", SqlDbType.NVarChar){Value = rid}
            };
            api.ProcedureCall(ProcedureName.REMOVE_RECORD_IN_PLAYLIST, parameters);
            return Ok();
        }
        [Authorize]
        [HttpDelete("delete/playlistUid={pid}")]
        public IActionResult removePlaylist(string pid)
        {
            api.DeleteDataByValue(DatabaseStruct.InfoPlaylistTable, "PlaylistUid", pid);
            api.DeleteDataByValue(DatabaseStruct.PlaylistTable, "PlaylistUid", pid);
            return Ok();
        }
    }
}
