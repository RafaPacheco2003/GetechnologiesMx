namespace GetechnologiesMx.Infrastructure.DTOs.Response;

public record FacturaResponse(
    int Id,
    string NumeroFactura,
    decimal Monto,
    DateTime Fecha,
    int PersonaId
);
