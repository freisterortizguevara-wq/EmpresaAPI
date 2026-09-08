using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmpresaAPI.Models;
using EmpresaAPI.Data;

namespace EmpresaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProyectosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Proyectos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proyecto>>> GetProyectos()
        {
            return await _context.Proyectos
                .Include(p => p.EmpleadoProyectos)
                .ThenInclude(ep => ep.Empleado)
                .ToListAsync();
        }

        // GET: api/Proyectos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proyecto>> GetProyecto(int id)
        {
            var proyecto = await _context.Proyectos
                .Include(p => p.EmpleadoProyectos)
                .ThenInclude(ep => ep.Empleado)
                .FirstOrDefaultAsync(p => p.IdProyecto == id);

            if (proyecto == null)
            {
                return NotFound();
            }

            return proyecto;
        }

        // POST: api/Proyectos
        [HttpPost]
        public async Task<ActionResult<Proyecto>> PostProyecto(Proyecto proyecto)
        {
            _context.Proyectos.Add(proyecto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProyecto), new { id = proyecto.IdProyecto }, proyecto);
        }

        // PUT: api/Proyectos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProyecto(int id, Proyecto proyecto)
        {
            if (id != proyecto.IdProyecto)
            {
                return BadRequest();
            }

            _context.Entry(proyecto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProyectoExists(id))
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

        // DELETE: api/Proyectos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProyecto(int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto == null)
            {
                return NotFound();
            }

            _context.Proyectos.Remove(proyecto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProyectoExists(int id)
        {
            return _context.Proyectos.Any(e => e.IdProyecto == id);
        }
    }
}