using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpresaAPI.Models
{
    public class EmpleadoProyecto
    {
        [Key]
        [Column(Order = 0)]
        public int IdEmpleado { get; set; }
        
        [Key]
        [Column(Order = 1)]
        public int IdProyecto { get; set; }
        
        public DateTime FechaAsignacion { get; set; }
        
        [MaxLength(50)]
        public string Rol { get; set; }
        
        public int? HorasAsignadas { get; set; }
        
        // Relaciones
        [ForeignKey("IdEmpleado")]
        public virtual Empleado Empleado { get; set; }
        
        [ForeignKey("IdProyecto")]
        public virtual Proyecto Proyecto { get; set; }
    }
}