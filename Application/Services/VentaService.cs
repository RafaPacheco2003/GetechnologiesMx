using GetechnologiesMx.Domain.Models;
using GetechnologiesMx.Domain.Port.In.Factura;

namespace GetechnologiesMx.Application.Services;

public class VentaService : 
    StoreFacturaUseCase,
    RetrieveFacturaUseCase,
    DeleteFacturaUseCase
{
    private readonly StoreFacturaUseCase _StoreFacturaUseCase;
    private readonly RetrieveFacturaUseCase _retrieveFacturaUseCase;
    private readonly DeleteFacturaUseCase _deleteFacturaUseCase;

    public VentaService(
        StoreFacturaUseCase StoreFacturaUseCase,
        RetrieveFacturaUseCase retrieveFacturaUseCase,
        DeleteFacturaUseCase deleteFacturaUseCase)
    {
        _StoreFacturaUseCase = StoreFacturaUseCase;
        _retrieveFacturaUseCase = retrieveFacturaUseCase;
        _deleteFacturaUseCase = deleteFacturaUseCase;
    }

    public async Task<Factura> StoreFactura(Factura factura)
    {
        return await _StoreFacturaUseCase.StoreFactura(factura);
    }

    public async Task<Factura?> FindById(int id)
    {
        return await _retrieveFacturaUseCase.FindById(id);
    }

    public async Task<Factura?> FindByNumeroFactura(string numeroFactura)
    {
        return await _retrieveFacturaUseCase.FindByNumeroFactura(numeroFactura);
    }

    public async Task<IEnumerable<Factura>> FindAll()
    {
        return await _retrieveFacturaUseCase.FindAll();
    }

    public async Task<IEnumerable<Factura>> FindByPersona(int personaId)
    {
        return await _retrieveFacturaUseCase.FindByPersona(personaId);
    }

    public async Task<bool> Delete(int id)
    {
        return await _deleteFacturaUseCase.Delete(id);
    }
}
