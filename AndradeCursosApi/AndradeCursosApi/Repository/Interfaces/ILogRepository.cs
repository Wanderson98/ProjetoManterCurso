using AndradeCursosApi.Models;

namespace AndradeCursosApi.Repository.Interfaces
{
    public interface ILogRepository
    {
        Task<IEnumerable<Log>> FindAll();
        Task<Log> FindById(int logId);
        Task<Log> Create(Log log);
        Task<Log> Update(Log log);
        Task<bool> Delete(int logId);
        
    }
}
