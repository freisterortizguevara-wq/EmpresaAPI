using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpresaAPI.Models
{
    public class Empleado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEmpleado { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Apellido { get; set; }
        
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }
        
        public DateTime? FechaNacimiento { get; set; }
        
        public DateTime FechaContratacion { get; set; }
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal? Salario { get; set; }
        
        public int? IdDepartamento { get; set; }
        
        // Relaciones
        [ForeignKey("IdDepartamento")]
        public virtual Departamento Departamento { get; set; }
        
        public virtual ICollection<EmpleadoProyecto> EmpleadoProyectos { get; set; }
    }
}