namespace GetechnologiesMx.Infrastructure.Persistence.Entities;

public class FacturaEntity
{
    public int Id { get; set; }
    public string NumeroFactura { get; set; } = string.Empty;
    public decimal Monto { get; set; }    
    public DateTime Fecha { get; set; }
    
    
    public int PersonaId { get; set; }
    
    
    public PersonaEntity Persona { get; set; } = null!;
}