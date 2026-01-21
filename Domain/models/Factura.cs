namespace GetechnologiesMx.Domain.Models;

public class Factura
{
    public int Id { get; set; }
    public string NumeroFactura { get; set; } = string.Empty;


    public decimal Monto { get; set; }    

    public DateTime Fecha{ get; set; }
    

    public int PersonaId { get; set; }


    public Factura()
    {
    }

    public Factura(string numeroFactura, decimal monto, DateTime fecha, int personaId)
    {
        NumeroFactura = numeroFactura;
        Monto = monto;
        Fecha = fecha;
        PersonaId = personaId;
    }
}

