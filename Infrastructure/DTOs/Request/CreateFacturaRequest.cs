namespace GetechnologiesMx.Infrastructure.DTOs.Request;

public record CreateFacturaRequest(
    string NumeroFactura,
    decimal Monto,
    DateTime Fecha,
    int PersonaId
);
