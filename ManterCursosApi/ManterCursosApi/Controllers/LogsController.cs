using ManterCursosApi.Models;
using ManterCursosApi.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

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


    }
}
