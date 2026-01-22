using System.ComponentModel.DataAnnotations;

namespace GetechnologiesMx.Infrastructure.Persistence.Entities;

public class PersonaEntity
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El nombre es requerido")]
    [MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
    [MinLength(2, ErrorMessage = "El nombre debe tener al menos 2 caracteres")]
    public string Nombre { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El apellido paterno es requerido")]
    [MaxLength(100, ErrorMessage = "El apellido paterno no puede exceder 100 caracteres")]
    [MinLength(2, ErrorMessage = "El apellido paterno debe tener al menos 2 caracteres")]
    public string ApellidoPaterno { get; set; } = string.Empty;
    
    [MaxLength(100, ErrorMessage = "El apellido materno no puede exceder 100 caracteres")]
    public string? ApellidoMaterno { get; set; }
    
    [Required(ErrorMessage = "La identificación es requerida")]
    [MaxLength(50, ErrorMessage = "La identificación no puede exceder 50 caracteres")]
    [RegularExpression(@"^[A-Z0-9]+$", ErrorMessage = "La identificación solo puede contener letras mayúsculas y números")]
    public string Identificacion { get; set; } = string.Empty;
    
    [Required]
    public DateTime FechaCreacion { get; set; }
    
    [Required]
    public DateTime FechaActualizacion { get; set; }
    
    public ICollection<FacturaEntity> Facturas { get; set; } = new List<FacturaEntity>();
}
