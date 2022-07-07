using AndradeCursosApi.Data;
using AndradeCursosApi.Models;
using AndradeCursosApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AndradeCursosApi.Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly DataContext _context;

        public LogRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Log> Create(Log log)
        {
            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
            return log;
        }

        public async Task<bool> Delete(int logId)
        {
            try
            {
                var log = await _context.Logs.FirstOrDefaultAsync(c => c.LogId == logId);
                if (log == null) return false;
                _context.Logs.Remove(log);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<IEnumerable<Log>> FindAll()
        {
            return await _context.Logs.ToListAsync();
        }

        public async Task<Log> FindByCursoId(int cursoId)
        {
            var log = await _context.Logs.FirstOrDefaultAsync(c => c.CursoId == cursoId);
            if(log == null) return null;
            return log;
        }

        public async Task<Log> FindById(int logId)
        {
            var log = await _context.Logs.FindAsync(logId);
            if (log == null) return null;
            return log;
        }

        public async Task<Log> Update(Log log)
        {
            _context.Logs.Update(log);
            await _context.SaveChangesAsync();
            return log;
        }
    }
}
