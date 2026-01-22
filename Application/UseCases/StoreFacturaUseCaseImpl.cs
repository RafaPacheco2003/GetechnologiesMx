using GetechnologiesMx.Domain.Exceptions;
using GetechnologiesMx.Domain.Models;
using GetechnologiesMx.Domain.Port.In.Factura;
using GetechnologiesMx.Domain.Port.Out;

namespace GetechnologiesMx.Application.UseCases;

public class StoreFacturaUseCaseImpl : StoreFacturaUseCase
{
    private readonly FacturaRepositoryPort _facturaRepositoryPort;
    private readonly ILoggerPort _logger;

    public StoreFacturaUseCaseImpl(
        FacturaRepositoryPort facturaRepositoryPort,
        ILoggerPort logger)
    {
        _facturaRepositoryPort = facturaRepositoryPort;
        _logger = logger;
    }

    public async Task<Factura> StoreFactura(Factura factura)
    {
        _logger.LogInformation("Iniciando almacenamiento de factura: {NumeroFactura}", factura.NumeroFactura);
        
        bool exists = await _facturaRepositoryPort.ExistsByNumeroFactura(factura.NumeroFactura);
        if (exists)
        {
            _logger.LogWarning("Intento de crear factura con número duplicado: {NumeroFactura}", factura.NumeroFactura);
            throw new AlreadyExistsException("Factura", "NumeroFactura", factura.NumeroFactura);
        }
        
        var result = await _facturaRepositoryPort.StoreFactura(factura);
        _logger.LogInformation("Factura almacenada exitosamente con ID: {Id}", result.Id);
        return result;
    }
}
