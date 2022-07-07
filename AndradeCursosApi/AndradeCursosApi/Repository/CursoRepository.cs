using AndradeCursosApi.Data;
using AndradeCursosApi.Models;
using AndradeCursosApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AndradeCursosApi.Repository
{
    public class CursoRepository : ICursoRepository
    {
        private readonly DataContext _context;

        public CursoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Curso> Create(Curso curso)
        {
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();
            return curso;
        }

        public async Task<bool> Delete(int cursoId)
        {
            try
            {
                var curso = await _context.Cursos.FirstOrDefaultAsync(c => c.CursoId == cursoId);
                if (curso == null) return false;
                _context.Cursos.Remove(curso);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<IEnumerable<Curso>> FindAll()
        {
            return await _context.Cursos.Include(c => c.Categoria).ToListAsync();
        }

        public async Task<IEnumerable<Curso>> FindAllActive()
        {
           return await _context.Cursos.Where(c=> c.IsAtivo).Include(c=>c.Categoria).ToListAsync();
        }

        public async Task<bool> VerificarCursosPeriodo(Curso curso)
        {
            var cursos = await _context.Cursos.Where(x => (x.CursoDataFinal.Date >= curso.CursoDataInicial.Date)
            && (x.CursoDataInicial.Date <= curso.CursoDataFinal.Date) 
            && x.IsAtivo && x.CursoId != curso.CursoId).ToListAsync();

            if (cursos.Count() < 1) return false;
            return true;
        }

        public async Task<Curso> FindById(int cursoId)
        {
            var curso = await _context.Cursos.FindAsync(cursoId);

            if (curso == null) return null;
           
            return curso;
        }

        public async Task<Curso> Update(Curso curso)
        {
            _context.Cursos.Update(curso);
            await _context.SaveChangesAsync();
            return curso;
        }

        public async Task<bool> VerificarCursosDuplicados(Curso curso)
        {
           var cursos = await _context.Cursos.Where(x => x.CursoDescricao.Equals(curso.CursoDescricao, StringComparison.OrdinalIgnoreCase )
            && x.IsAtivo && x.CursoId != curso.CursoId ).ToListAsync();
            if (cursos.Count() < 1) return false;
            return true;
        }
    }
}
