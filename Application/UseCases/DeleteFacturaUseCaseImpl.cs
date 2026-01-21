using GetechnologiesMx.Domain.Exceptions;
using GetechnologiesMx.Domain.Models;
using GetechnologiesMx.Domain.Port.In.Factura;
using GetechnologiesMx.Domain.Port.Out;

namespace GetechnologiesMx.Application.UseCases;

public class DeleteFacturaUseCaseImpl : DeleteFacturaUseCase
{
    private readonly FacturaRepositoryPort _facturaRepositoryPort;

    public DeleteFacturaUseCaseImpl(FacturaRepositoryPort facturaRepositoryPort)
    {
        _facturaRepositoryPort = facturaRepositoryPort;
    }

    public async Task<bool> Delete(int id)
    {
        Factura? factura = await _facturaRepositoryPort.FindById(id);
        
        if (factura == null)
        {
            throw new NotFoundException("Factura", id);
        }
        
        return await _facturaRepositoryPort.Delete(id);
    }
}
