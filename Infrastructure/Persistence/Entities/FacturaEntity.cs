namespace GetechnologiesMx.Infrastructure.Persistence.Entities;

public class FacturaEntity
{
    public int Id { get; set; }
    public string NumeroFactura { get; set; } = string.Empty;
    public decimal Monto { get; set; }    
    public DateTime Fecha { get; set; }
    
    // Foreign Key: Una Factura pertenece a una Persona
    public int PersonaId { get; set; }
    
    // Navigation Property: Relación ManyToOne
    public PersonaEntity Persona { get; set; } = null!;
}