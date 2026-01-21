namespace GetechnologiesMx.Infrastructure.DTOs.Request;

public record CreatePersonaRequest(
    string Nombre,
    string ApellidoPaterno,
    string? ApellidoMaterno,
    string Identificacion
);
