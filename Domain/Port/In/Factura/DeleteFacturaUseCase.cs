namespace GetechnologiesMx.Domain.Port.In.Factura;

public interface DeleteFacturaUseCase
{
    Task<bool> Delete(int id);
}
