using ManterCursosApi.Data;
using ManterCursosApi.Models;
using ManterCursosApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ManterCursosApi.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly DataContext _context;

        public CategoriaRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<Categoria> Create(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<bool> Delete(int categoriaId)
        {
            try
            {
                var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == categoriaId);
                if (categoria == null) return false;
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<IEnumerable<Categoria>> FindAll()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categoria> FindById(int categoriaId)
        {
            var categoria = await _context.Categorias.FindAsync(categoriaId);

            if (categoria == null) return null;

            return categoria;
        }

        public async Task<Categoria> Update(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }
    }
}
