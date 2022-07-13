using ManterCursosApi.Models;

namespace ManterCursosApi.Repository.Interfaces
{
    public interface ILogRepository
    {
        Task<IEnumerable<Log>> FindAll();
        Task<IEnumerable<Log>> FindAllOrderDate();
        Task<Log> FindById(int logId);
        Task<Log> FindByCursoId(int cursoId);
        Task<Log> Create(Log log);
        Task<Log> Update(Log log);
        Task<bool> Delete(int logId);

    }
}
