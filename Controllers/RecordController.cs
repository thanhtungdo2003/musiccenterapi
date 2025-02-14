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
        [HttpGet("{id}")]
        public IActionResult Record(string id)
        {
            Guid idValue;
            if (Guid.TryParse(id, out idValue))
            {
                var record = new RecordData(_api, idValue);
                if (record != null)
                {
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
                var recordEditor = new RecordData(_api, idValue);
                if (recordEditor != null)
                {
                    int oldAmount = recordEditor.views;
                    recordEditor.views = oldAmount + amount;
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
