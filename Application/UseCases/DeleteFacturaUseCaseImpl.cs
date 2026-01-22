using GetechnologiesMx.Domain.Exceptions;
using GetechnologiesMx.Domain.Models;
using GetechnologiesMx.Domain.Port.In.Factura;
using GetechnologiesMx.Domain.Port.Out;

namespace GetechnologiesMx.Application.UseCases;

public class DeleteFacturaUseCaseImpl : DeleteFacturaUseCase
{
    private readonly FacturaRepositoryPort _facturaRepositoryPort;
    private readonly ILoggerPort _logger;

    public DeleteFacturaUseCaseImpl(
        FacturaRepositoryPort facturaRepositoryPort,
        ILoggerPort logger)
    {
        _facturaRepositoryPort = facturaRepositoryPort;
        _logger = logger;
    }

    public async Task<bool> Delete(int id)
    {
        _logger.LogInformation("Intentando eliminar factura con ID: {Id}", id);
        
        Factura? factura = await _facturaRepositoryPort.FindById(id);
        
        if (factura == null)
        {
            _logger.LogWarning("No se puede eliminar. Factura con ID: {Id} no encontrada", id);
            throw new NotFoundException("Factura", id);
        }
        
        var result = await _facturaRepositoryPort.Delete(id);
        
        if (result)
        {
            _logger.LogInformation("Factura con ID: {Id} eliminada exitosamente", id);
        }
        else
        {
            _logger.LogError("Falló la eliminación de factura con ID: {Id}", id);
        }
        
        return result;
    }
}
