using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpresaAPI.Models
{
    public class Departamento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDepartamento { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        
        [MaxLength(255)]
        public string Descripcion { get; set; }
        
        public DateTime FechaCreacion { get; set; }
        
        // Relación: Un departamento tiene muchos empleados
        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}