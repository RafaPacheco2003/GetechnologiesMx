using GetechnologiesMx.Domain.Models;

namespace GetechnologiesMx.Domain.Port.Out;

public interface FacturaRepositoryPort
{
    Task<Factura> StoreFactura(Factura factura);
    Task<Factura?> FindById(int id);
    Task<Factura?> FindByNumeroFactura(string numeroFactura);
    Task<IEnumerable<Factura>> FindAll();
    Task<IEnumerable<Factura>> FindByPersona(int personaId);
    Task<bool> Delete(int id);
    Task<bool> ExistsByNumeroFactura(string numeroFactura);
}
