using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManterCursosApi.Models;
using ManterCursosApi.Repository.Interfaces;

namespace ManterCursosApi.Controllers
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

                await AtualizarLog(curso, 1);
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

            if (curso.CursoDataFinal.Date < DateTime.Now.Date) return BadRequest(Validacoes.ErrorExclusaoCursoConcluido);

            if (!mensagem.Equals("Ok")) return BadRequest(mensagem);

            curso.IsAtivo = false;
            await _repository.Update(curso);
            await AtualizarLog(curso, 2);

            return NoContent();
        }

        private bool CursoExists(int id)
        {
            var curso = _repository.FindById(id);
            if (curso == null) return false;
            return true;

        }

        private void CriarLog(Curso curso )
        {
            var log = new Log()
            {
                CursoId = curso.CursoId,
                LogDataInclusao = DateTime.Now,
                Usuario = "Admin",
                Modificacao = "Criação Do Curso"
              
            };

            _logRepository.Create(log);
        }

        private async Task<ActionResult> AtualizarLog(Curso curso, int cod)
        {
            var log = await _logRepository.FindByCursoId(curso.CursoId);
            log.LogDataAtualizacao = DateTime.Now;

            if (cod == 1) log.Modificacao = "Alteração do Curso";
            if (cod == 2) log.Modificacao = "Exclusão do Curso";

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
                return Validacoes.ErrorNaoEncontrado;
            }

            if (curso.CursoDataInicial.Date < DateTime.Now.Date)
            {
                return Validacoes.ErrorDataInicialMenorAtual;
            }

            if (curso.CursoDataFinal.Date < curso.CursoDataInicial.Date)
            {
                return Validacoes.ErrorDataFinalMenorInicial;
            }

            if (await _repository.VerificarCursosPeriodo(curso))
            {
                return Validacoes.ErrorCursoPeriodo;
            }

            if (await _repository.VerificarCursosDuplicados(curso))
            {
                
                return Validacoes.ErrorCursoJaCadastrado;
            }

            return Validacoes.CursoOK;
        }
    }
}
