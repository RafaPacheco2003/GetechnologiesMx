using GetechnologiesMx.Domain.Exceptions;
using GetechnologiesMx.Domain.Models;
using GetechnologiesMx.Domain.Port.In.Factura;
using GetechnologiesMx.Domain.Port.Out;

namespace GetechnologiesMx.Application.UseCases;

public class RetrieveFacturaUseCaseImpl : RetrieveFacturaUseCase
{
    private readonly FacturaRepositoryPort _facturaRepositoryPort;

    public RetrieveFacturaUseCaseImpl(FacturaRepositoryPort facturaRepositoryPort)
    {
        _facturaRepositoryPort = facturaRepositoryPort;
    }

    public async Task<Factura?> FindById(int id)
    {
        Factura? factura = await _facturaRepositoryPort.FindById(id);
        
        if (factura == null)
        {
            throw new NotFoundException("Factura", id);
        }
        
        return factura;
    }

    public async Task<Factura?> FindByNumeroFactura(string numeroFactura)
    {
        Factura? factura = await _facturaRepositoryPort.FindByNumeroFactura(numeroFactura);
        
        if (factura == null)
        {
            throw new NotFoundException("Factura", "NumeroFactura", numeroFactura);
        }
        
        return factura;
    }

    public async Task<IEnumerable<Factura>> FindAll()
    {
        IEnumerable<Factura> facturas = await _facturaRepositoryPort.FindAll();
        return facturas;
    }

    public async Task<IEnumerable<Factura>> FindByPersona(int personaId)
    {
        IEnumerable<Factura> facturas = await _facturaRepositoryPort.FindByPersona(personaId);
        return facturas;
    }
}
