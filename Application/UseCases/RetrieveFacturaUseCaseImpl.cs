using GetechnologiesMx.Domain.Exceptions;
using GetechnologiesMx.Domain.Models;
using GetechnologiesMx.Domain.Port.In.Factura;
using GetechnologiesMx.Domain.Port.Out;

namespace GetechnologiesMx.Application.UseCases;

public class RetrieveFacturaUseCaseImpl : RetrieveFacturaUseCase
{
    private readonly FacturaRepositoryPort _facturaRepositoryPort;
    private readonly ILoggerPort _logger;

    public RetrieveFacturaUseCaseImpl(
        FacturaRepositoryPort facturaRepositoryPort,
        ILoggerPort logger)
    {
        _facturaRepositoryPort = facturaRepositoryPort;
        _logger = logger;
    }

    public async Task<Factura?> FindById(int id)
    {
        _logger.LogInformation("Buscando factura con ID: {Id}", id);
        
        Factura? factura = await _facturaRepositoryPort.FindById(id);
        
        if (factura == null)
        {
            _logger.LogWarning("Factura con ID: {Id} no encontrada", id);
            throw new NotFoundException("Factura", id);
        }
        
        _logger.LogInformation("Factura encontrada con ID: {Id}", id);
        return factura;
    }

    public async Task<Factura?> FindByNumeroFactura(string numeroFactura)
    {
        _logger.LogInformation("Buscando factura con número: {NumeroFactura}", numeroFactura);
        
        Factura? factura = await _facturaRepositoryPort.FindByNumeroFactura(numeroFactura);
        
        if (factura == null)
        {
            _logger.LogWarning("Factura con número: {NumeroFactura} no encontrada", numeroFactura);
            throw new NotFoundException("Factura", "NumeroFactura", numeroFactura);
        }
        
        _logger.LogInformation("Factura encontrada con número: {NumeroFactura}", numeroFactura);
        return factura;
    }

    public async Task<IEnumerable<Factura>> FindAll()
    {
        _logger.LogInformation("Obteniendo todas las facturas");
        IEnumerable<Factura> facturas = await _facturaRepositoryPort.FindAll();
        _logger.LogInformation("Se obtuvieron {Count} facturas", facturas.Count());
        return facturas;
    }

    public async Task<IEnumerable<Factura>> FindByPersona(int personaId)
    {
        _logger.LogInformation("Buscando facturas de persona con ID: {PersonaId}", personaId);
        IEnumerable<Factura> facturas = await _facturaRepositoryPort.FindByPersona(personaId);
        _logger.LogInformation("Se encontraron {Count} facturas para persona ID: {PersonaId}", facturas.Count(), personaId);
        return facturas;
    }
}
