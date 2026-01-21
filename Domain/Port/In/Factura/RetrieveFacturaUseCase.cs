namespace GetechnologiesMx.Domain.Port.In.Factura;
using GetechnologiesMx.Domain.Models;

public interface RetrieveFacturaUseCase
{
    Task<Factura?> FindById(int id);
    Task<Factura?> FindByNumeroFactura(string numeroFactura);
    Task<IEnumerable<Factura>> FindAll();
    Task<IEnumerable<Factura>> FindByPersona(int personaId);
}
