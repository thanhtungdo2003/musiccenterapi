using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MusicCenterAPI.Data;
using MusicCenterAPI.Models;
using MusicCenterAPI.Pages;
using System.Data.SqlClient;
using System.Data;
using MusicCenterAPI.ProcedureStorage;
using MusicBusniess;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Globalization;

namespace MusicCenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController: Controller
    {

        public static Dictionary<string, ViralMusic> viewsStatic = new Dictionary<string, ViralMusic>();
        public IMusicCenterAPI _api;
        public RecordController(IMusicCenterAPI api)
        {
            _api = api;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_api.CoventToDictionarysWithDataTable(_api.ProcedureCall(ProcedureName.GET_ALL_RECORDS, null)));
        }
        [HttpGet("page/{page}&{rows}")]
        public IActionResult GetByPage(int page, int rows)
        {
            SqlParameter[] parameters = new SqlParameter[]
           {
                new SqlParameter("@Page", SqlDbType.Int) { Value = page },
                new SqlParameter("@Rows", SqlDbType.Int) { Value = rows }
           };
            return Ok(_api.CoventToDictionarysWithDataTable(_api.ProcedureCall(ProcedureName.GET_RECORD_BY_PAGE, parameters)));
        }
        [HttpGet("top_audio")]
        public IActionResult GetTop()
        {
            SqlParameter[] parameters = new SqlParameter[]
           {
                new SqlParameter("@TopRows", SqlDbType.Int) { Value = 6 }
           };
            return Ok(_api.CoventToDictionarysWithDataTable(_api.ProcedureCall(ProcedureName.GET_TOP_RECORD_BY_VIEW, parameters)));
        }
        [HttpGet("new_audio")]
        public IActionResult GetNew()
        {
            SqlParameter[] parameters = new SqlParameter[]
           {
                new SqlParameter("@TopRows", SqlDbType.Int) { Value = 6 }
           };
            return Ok(_api.CoventToDictionarysWithDataTable(_api.ProcedureCall(ProcedureName.GET_NEW_RECORDS, parameters)));
        }
        [HttpGet("search_query={keyword}&{page}")]
        public IActionResult Search(string keyword, int page)
        {
            SqlParameter[] parameters = new SqlParameter[]
           {
                new SqlParameter("@keyword", SqlDbType.NVarChar) { Value = keyword },
                new SqlParameter("@Page", SqlDbType.Int) { Value = page }
           };
            return Ok(_api.CoventToDictionarysWithDataTable(_api.ProcedureCall(ProcedureName.RECORD_SEARCH_BY_KEYWORD, parameters)));
        }
        [HttpGet("{id}/{token}")]
        public IActionResult Record(string id, string token)
        {   
            Guid idValue;
            if (Guid.TryParse(id, out idValue))
            {
                var record = new RecordData(_api ,idValue.ToString());
                Console.WriteLine("Lấy bài hát: " + record.DisplayName);
                record.SetUp(_api);
                if (record != null)
                {
                    if (record.Payfee == "TRUE")
                    {
                        bool isCancel = false;
                        var principal = _api.DecodeToken(token);
                        if (token == null || principal == null)
                        {
                            record.Record = "EXPIRED";
                            return Ok(record);
                        }
                        AccountData account = new AccountData(_api, principal.FindFirst(ClaimTypes.Name)?.Value);
                        if (account.PremiumEx == "NOEX")
                        {
                            return Ok(record);
                        }
                        if (account.PremiumEx == "NONE")
                        {
                            record.Record = "EXPIRED";
                            return Ok(record);
                        }
                        DateTime now = DateTime.Now;
                        DateTime exDate = DateTime.ParseExact(account.PremiumEx, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        if (now > exDate)
                        {
                            record.Record = "EXPIRED";
                        }
                        return Ok(record);
                    }
                    return Ok(record);
                }
            }
            return NotFound();
        }
        
        [HttpPut("AddViews/{id}")]
        public IActionResult addViews(string id, int amount)
        {
            if (Guid.TryParse(id, out Guid idValue))
            {
                var recordEditor = new RecordData(_api, idValue.ToString());
                if (recordEditor != null)
                {
                    int oldAmount = recordEditor.Views;
                    recordEditor.Views = oldAmount + amount;
                    if (viewsStatic.ContainsKey(id))
                    {
                        viewsStatic[id].views = viewsStatic[id].views + amount;
                    }
                    else
                    {
                        viewsStatic[id] = new ViralMusic(id, amount, recordEditor);
                    }
                    recordEditor.Save();
                    return Ok();
                }
            }
            return NotFound();
        }
        [HttpGet("Viral")]
        public IActionResult GetViral()
        {
            return Ok(viewsStatic.Values);
        }
    }
}
