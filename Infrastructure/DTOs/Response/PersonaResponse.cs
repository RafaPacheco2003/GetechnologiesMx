namespace GetechnologiesMx.Infrastructure.DTOs.Response;

public record PersonaResponse(
    int Id,
    string Nombre,
    string ApellidoPaterno,
    string? ApellidoMaterno,
    string Identificacion
);
