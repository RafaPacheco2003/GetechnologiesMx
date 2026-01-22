using GetechnologiesMx.Domain.Exceptions;
using GetechnologiesMx.Domain.Models;
using GetechnologiesMx.Domain.Port.In;
using GetechnologiesMx.Domain.Port.Out;

namespace GetechnologiesMx.Application.UseCases;

public class StorePersonaUseCaseImpl : StorePersonaUseCase
{
    private readonly PersonaRepositoryPort _personaRepositoryPort;
    private readonly ILoggerPort _logger;

    public StorePersonaUseCaseImpl(
        PersonaRepositoryPort personaRepositoryPort,
        ILoggerPort logger)
    {
        _personaRepositoryPort = personaRepositoryPort;
        _logger = logger;
    }

    public async Task<Persona> StorePersona(Persona persona)
    {
        _logger.LogInformation("Iniciando almacenamiento de persona con identificación: {Identificacion}", persona.Identificacion);
        
        bool exists = await _personaRepositoryPort.ExistsByIdentification(persona.Identificacion);
        if (exists)
        {
            _logger.LogWarning("Intento de crear persona con identificación duplicada: {Identificacion}", persona.Identificacion);
            throw new AlreadyExistsException("Persona", "Identificacion", persona.Identificacion);
        }
        
        var result = await _personaRepositoryPort.StorePersona(persona);
        _logger.LogInformation("Persona almacenada exitosamente con ID: {Id}", result.Id);
        return result;
    }
}