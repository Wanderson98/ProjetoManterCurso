using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndradeCursosApi.Data;
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
            if (id != curso.CursoId)
            {
                return BadRequest();
            }
            if (curso.CursoDataInicial.Date < DateTime.Now.Date)
            {
                return BadRequest();
            }

            if (curso.CursoDataFinal.Date < curso.CursoDataInicial.Date)
            {
                return BadRequest();
            }

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

            if (curso.CursoDataInicial.Date < DateTime.Now.Date)
            {
                return BadRequest("Data inicial do curso não pode ser menor do que hoje");
            }

            if(curso.CursoDataFinal.Date < curso.CursoDataInicial.Date)
            {
                return BadRequest("Data final não pode ser menor que a data inicial");
            }

            if (await VerificarCursosPeriodo(curso))
            {
                return BadRequest("Existe(m) curso(s) planejados(s) dentro do período informado");
            }
           
            await _repository.Create(curso);
            CreatedAtAction("GetCurso", new { id = curso.CursoId }, curso);
          
            CriarLog(curso);

            return Ok(curso);
        }

        // DELETE: api/Cursos/5
        [HttpPut("exclusaolog/")]
        public async Task<IActionResult> DeleteCurso(int id)
        {
            var curso = await _repository.FindById(id);
            if (curso == null)
            {
                return NotFound();
            }

            if(curso.CursoDataFinal.Date < DateTime.Now.Date)
            {
                return BadRequest();
            }

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

        private async Task<bool> VerificarCursosPeriodo(Curso curso)
        {
            var cursos = await _repository.FindAllTeste(curso);
            if(cursos.Count() < 1) return false;
            return true;
        }
    }
}
