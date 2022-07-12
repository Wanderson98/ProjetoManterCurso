using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManterCursosApi.Data;
using ManterCursosApi.Models;
using ManterCursosApi.Repository.Interfaces;

namespace ManterCursosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILogRepository _logRepository;

        public LogsController(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }



        // GET: api/Logs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Log>>> GetLogs()
        {
            var logs = await _logRepository.FindAllOrderDate();
            return Ok(logs);
        }

        //// GET: api/Logs/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Log>> GetLog(int id)
        //{
        //    var log = await _context.Logs.FindAsync(id);

        //    if (log == null)
        //    {
        //        return NotFound();
        //    }

        //    return log;
        //}

        //// PUT: api/Logs/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutLog(int id, Log log)
        //{
        //    if (id != log.LogId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(log).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!LogExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Logs
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Log>> PostLog(Log log)
        //{
        //    _context.Logs.Add(log);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetLog", new { id = log.LogId }, log);
        //}

        //// DELETE: api/Logs/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteLog(int id)
        //{
        //    var log = await _context.Logs.FindAsync(id);
        //    if (log == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Logs.Remove(log);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool LogExists(int id)
        //{
        //    return _context.Logs.Any(e => e.LogId == id);
        //}
    }
}
