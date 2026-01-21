namespace GetechnologiesMx.Domain.Port.In.Factura;
using GetechnologiesMx.Domain.Models;

public interface StoreFacturaUseCase
{
    Task<Factura> StoreFactura(Factura factura);
}
