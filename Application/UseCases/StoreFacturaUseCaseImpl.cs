using GetechnologiesMx.Domain.Exceptions;
using GetechnologiesMx.Domain.Models;
using GetechnologiesMx.Domain.Port.In.Factura;
using GetechnologiesMx.Domain.Port.Out;

namespace GetechnologiesMx.Application.UseCases;

public class StoreFacturaUseCaseImpl : StoreFacturaUseCase
{
    private readonly FacturaRepositoryPort _facturaRepositoryPort;

    public StoreFacturaUseCaseImpl(FacturaRepositoryPort facturaRepositoryPort)
    {
        _facturaRepositoryPort = facturaRepositoryPort;
    }

    public async Task<Factura> StoreFactura(Factura factura)
    {
        return await _facturaRepositoryPort.StoreFactura(factura);
    }
}
