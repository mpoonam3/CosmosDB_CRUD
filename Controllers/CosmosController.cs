using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmosDB_CRUD.DataAccess;
using CosmosDB_CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CosmosDB_CRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CosmosController : ControllerBase
    {
        /// <summary>
        /// </summary>
        ICosmosDataAdapter _adapter;
        public CosmosController(ICosmosDataAdapter adapter)
        {
            _adapter = adapter;
        }
                
        [HttpPost("createnew")]
        public async Task<IActionResult> CreateNew([FromBody] StudentDetails details)
        {
            var result = await _adapter.AddNewStudent("students", "StudentDetails", details);
            return Ok(result);
        }
                
        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var result = await _adapter.GetStudentDetails("students", "StudentDetails");
            return Ok(result);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Post([FromBody] StudentDetails details)
        {
            var result = await _adapter.UpsertStudentDetails("students", "StudentDetails", details);
            return Ok();
        }
                
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _adapter.DeleteStudentDetails("students", "StudentDetails", id);
            return Ok(result);
        }
    }
}
