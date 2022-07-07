using AndradeCursosApi.Models;

namespace AndradeCursosApi.Repository.Interfaces
{
    public interface ICursoRepository
    {
        
        Task<IEnumerable<Curso>> FindAll();
        Task<IEnumerable<Curso>> FindAllTeste(Curso curso);
        Task<IEnumerable<Curso>> FindAllActive();
        Task<Curso> FindById(int cursoId);
        Task<Curso> Create(Curso curso);
        Task<Curso> Update(Curso curso);
        Task<bool> Delete(int cursoId);
       
    }
}
