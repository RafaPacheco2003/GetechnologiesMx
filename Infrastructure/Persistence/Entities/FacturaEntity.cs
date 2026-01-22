using System.ComponentModel.DataAnnotations;

namespace GetechnologiesMx.Infrastructure.Persistence.Entities;

public class FacturaEntity
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El número de factura es requerido")]
    [MaxLength(50, ErrorMessage = "El número de factura no puede exceder 50 caracteres")]
    [RegularExpression(@"^[A-Z0-9-]+$", ErrorMessage = "El número de factura solo puede contener letras mayúsculas, números y guiones")]
    public string NumeroFactura { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El monto es requerido")]
    [Range(0.01, 999999999.99, ErrorMessage = "El monto debe ser mayor a 0")]
    public decimal Monto { get; set; }
    
    [Required(ErrorMessage = "La fecha es requerida")]
    public DateTime Fecha { get; set; }
    
    [Required(ErrorMessage = "El PersonaId es requerido")]
    [Range(1, int.MaxValue, ErrorMessage = "El PersonaId debe ser mayor a 0")]
    public int PersonaId { get; set; }
    
    public PersonaEntity Persona { get; set; } = null!;
}