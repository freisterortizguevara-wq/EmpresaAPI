using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpresaAPI.Models
{
    public class Proyecto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProyecto { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        
        [MaxLength(255)]
        public string Descripcion { get; set; }
        
        [Column(TypeName = "decimal(15,2)")]
        public decimal? Presupuesto { get; set; }
        
        public DateTime? FechaInicio { get; set; }
        
        public DateTime? FechaFin { get; set; }
        
        [MaxLength(20)]
        public string Estado { get; set; }
        
        // Relaciones
        public virtual ICollection<EmpleadoProyecto> EmpleadoProyectos { get; set; }
    }
}