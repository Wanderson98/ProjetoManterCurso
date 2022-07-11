using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndradeCursosApi.Models;
using AndradeCursosApi.Repository.Interfaces;

namespace AndradeCursosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly ICursoRepository _repository;
        private readonly ILogRepository _logRepository;

        public CursosController(ICursoRepository repository, ILogRepository logRepository)
        {
            _repository = repository;
            _logRepository = logRepository;
        }

        // GET: api/Cursos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCursos()
        {
            var cursos = await _repository.FindAll();
            return Ok(cursos);
        }

        [HttpGet("ativos/")]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCursosAtivos()
        {
            var cursos = await _repository.FindAllActive();
            return Ok(cursos);
        }

        // GET: api/Cursos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetCurso(int id)
        {
            var curso = await _repository.FindById(id);

            if (curso == null)
            {
                return NotFound();
            }

            return curso;
        }

        // PUT: api/Cursos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurso(int id, Curso curso)
        {
            string mensagem = await ValidacoesCurso(curso);
           
            if (!mensagem.Equals("Ok")) return BadRequest(mensagem);
        
            try
            {
                await _repository.Update(curso);
                await AtualizarLog(curso);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cursos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Curso>> PostCurso(Curso curso)
        {
            string mensagem = await ValidacoesCurso(curso);

            if (!mensagem.Equals("Ok")) return BadRequest(mensagem);

            await _repository.Create(curso);
            CreatedAtAction("GetCurso", new { id = curso.CursoId }, curso);
          
            CriarLog(curso);

            return Ok(curso);
        }

        // DELETE: api/Cursos/5
        [HttpPut("exclusaologica/{id}")]
        public async Task<IActionResult> DeleteCurso(int id)
        {


            var curso = await _repository.FindById(id);
            string mensagem = await ValidacoesCurso(curso);

            if (curso.CursoDataFinal.Date < DateTime.Now.Date) return BadRequest("Não é permitida a exclusão de um curso concluido");

            if (!mensagem.Equals("Ok")) return BadRequest(mensagem);

            curso.IsAtivo = false;
            await _repository.Update(curso);
            await AtualizarLog(curso);

            return NoContent();
        }

        private bool CursoExists(int id)
        {
            var curso = _repository.FindById(id);
            if (curso == null) return false;
            return true;

        }

        private void CriarLog(Curso curso)
        {
            var log = new Log()
            {
                CursoId = curso.CursoId,
                LogDataInclusao = DateTime.Now,
                Usuario = "Admin"
              
            };

            _logRepository.Create(log);
        }

        private async Task<ActionResult> AtualizarLog(Curso curso)
        {
            var log = await _logRepository.FindByCursoId(curso.CursoId);

            log.LogDataAtualizacao = DateTime.Now;
            try
            {
                await _logRepository.Update(log);
            }
            catch (Exception)
            {

                return BadRequest();
            }

            return NoContent();

        }
        
        private async Task<string> ValidacoesCurso(Curso curso)
        {
           
            if (curso == null)
            {
                return "Nao Encontrado";
            }

            if (curso.CursoDataInicial.Date < DateTime.Now.Date)
            {
                return "Data inicial do curso não pode ser menor do que hoje";
            }

            if (curso.CursoDataFinal.Date < curso.CursoDataInicial.Date)
            {
                return "Data final não pode ser menor que a data inicial";
            }

            if (await _repository.VerificarCursosPeriodo(curso))
            {
                return "Existe(m) curso(s) planejados(s) dentro do período informado";
            }

            if (await _repository.VerificarCursosDuplicados(curso))
            {
                
                return "Já existe um curso com este nome cadastrado";
            }

            return "Ok";
        }
    }
}
