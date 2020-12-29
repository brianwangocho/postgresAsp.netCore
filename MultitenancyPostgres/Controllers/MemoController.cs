using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultitenancyPostgres.DataLayer;
using MultitenancyPostgres.Models;
using MultitenancyPostgres.Services.Memo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultitenancyPostgres.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemoController : ControllerBase
    {
        MemoService memoService = new MemoService();

        [HttpPost()]
        [Route("add_memo")]
        public async Task< IActionResult> AddMemo([FromForm] MemoRequest memo)
        {
            if(memo == null)
            {
                return BadRequest();
            }
            var response = await memoService.AddMemo(memo);
            if(response.Status == "01")
            {
                return StatusCode(500,response);
            }

            return Ok(response);
        }
        [HttpGet()]
        [Route("get_memos")]
        public IActionResult MemoList()
        {
            MemoRepository memoRepository = new MemoRepository("isuzu");

            return Ok(memoRepository.Memos());
        }
    }
}
