using ManterCursosApi.Models;

namespace ManterCursosApi.Repository.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> FindAll();
        Task<Categoria> FindById(int categoriaId);
        Task<Categoria> Create(Categoria categoria);
        Task<Categoria> Update(Categoria categoria);
        Task<bool> Delete(int categoriaId);
     
    }
}
