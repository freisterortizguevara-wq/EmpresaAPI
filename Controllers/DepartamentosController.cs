using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmpresaAPI.Models;
using EmpresaAPI.Data;

namespace EmpresaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Departamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departamento>>> GetDepartamentos()
        {
            return await _context.Departamentos
                .Include(d => d.Empleados)
                .ToListAsync();
        }

        // GET: api/Departamentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Departamento>> GetDepartamento(int id)
        {
            var departamento = await _context.Departamentos
                .Include(d => d.Empleados)
                .FirstOrDefaultAsync(d => d.IdDepartamento == id);

            if (departamento == null)
            {
                return NotFound();
            }

            return departamento;
        }

        // POST: api/Departamentos
        [HttpPost]
        public async Task<ActionResult<Departamento>> PostDepartamento(Departamento departamento)
        {
            departamento.FechaCreacion = DateTime.Now;
            _context.Departamentos.Add(departamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartamento), new { id = departamento.IdDepartamento }, departamento);
        }

        // PUT: api/Departamentos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartamento(int id, Departamento departamento)
        {
            if (id != departamento.IdDepartamento)
            {
                return BadRequest();
            }

            _context.Entry(departamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartamentoExists(id))
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

        // DELETE: api/Departamentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartamento(int id)
        {
            var departamento = await _context.Departamentos.FindAsync(id);
            if (departamento == null)
            {
                return NotFound();
            }

            _context.Departamentos.Remove(departamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartamentoExists(int id)
        {
            return _context.Departamentos.Any(e => e.IdDepartamento == id);
        }
    }
}