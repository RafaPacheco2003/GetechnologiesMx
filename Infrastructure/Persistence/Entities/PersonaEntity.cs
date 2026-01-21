namespace GetechnologiesMx.Infrastructure.Persistence.Entities;

public class PersonaEntity
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string ApellidoPaterno { get; set; } = string.Empty;
    public string? ApellidoMaterno { get; set; }
    public string Identificacion { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaActualizacion { get; set; }
    public ICollection<FacturaEntity> Facturas { get; set; } = new List<FacturaEntity>();
}
