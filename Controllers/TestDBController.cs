using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EmpresaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestDBController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TestDBController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult TestConnection()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var results = new List<string>();
            
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    results.Add($"✅ Conectado a: {connection.DataSource}");
                    results.Add($"✅ Base de datos: {connection.Database}");
                    results.Add($"✅ Estado: {connection.State}");
                    
                    // Probar una consulta simple
                    using (var command = new SqlCommand("SELECT 1", connection))
                    {
                        var result = command.ExecuteScalar();
                        results.Add($"✅ Consulta de prueba: {result}");
                    }
                    
                    return Ok(new { 
                        success = true,
                        message = "Conexión exitosa",
                        details = results,
                        connectionString = connectionString
                    });
                }
            }
            catch (SqlException ex)
            {
                results.Add($"❌ Error SQL: {ex.Message}");
                results.Add($"❌ Número de error: {ex.Number}");
                return BadRequest(new { 
                    success = false,
                    message = "Error de conexión",
                    details = results,
                    error = ex.Message,
                    errorNumber = ex.Number
                });
            }
            catch (Exception ex)
            {
                results.Add($"❌ Error general: {ex.Message}");
                return BadRequest(new { 
                    success = false,
                    message = "Error de conexión",
                    details = results,
                    error = ex.Message
                });
            }
        }
    }
}