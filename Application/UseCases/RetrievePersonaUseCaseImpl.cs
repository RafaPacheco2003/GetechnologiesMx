using GetechnologiesMx.Domain.Exceptions;
using GetechnologiesMx.Domain.Models;
using GetechnologiesMx.Domain.Port.In;
using GetechnologiesMx.Domain.Port.Out;

namespace GetechnologiesMx.Application.UseCases;

public class RetrievePersonaUseCaseImpl : RetrievePersonaUseCase
{
    private readonly PersonaRepositoryPort _personaRepositoryPort;
    private readonly ILoggerPort _logger;

    public RetrievePersonaUseCaseImpl(
        PersonaRepositoryPort personaRepositoryPort,
        ILoggerPort logger)
    {
        _personaRepositoryPort = personaRepositoryPort;
        _logger = logger;
    }

    public async Task<Persona?> FindById(int id)
    {
        _logger.LogInformation("Buscando persona con ID: {Id}", id);
        
        Persona? persona = await _personaRepositoryPort.FindById(id);
        
        if (persona == null)
        {
            _logger.LogWarning("Persona con ID: {Id} no encontrada", id);
            throw new NotFoundException("Persona", id);
        }
        
        _logger.LogInformation("Persona encontrada con ID: {Id}", id);
        return persona;
    }

    public async Task<Persona?> FindByIdentification(string identificacion)
    {
        _logger.LogInformation("Buscando persona con Identificación: {Identificacion}", identificacion);

        Persona? persona = await _personaRepositoryPort.FindByIdentification(identificacion);
        
        if (persona == null)
        {
            _logger.LogWarning("Persona con Identificación: {Identificacion} no encontrada", identificacion);
            throw new NotFoundException("Persona", "Identificacion", identificacion);
        }
        
        _logger.LogInformation("Persona encontrada con Identificación: {Identificacion}", identificacion);
        return persona;
    }

    public async Task<IEnumerable<Persona>> FindAll()
    {
        _logger.LogInformation("Obteniendo todas las personas");
        IEnumerable<Persona> personas = await _personaRepositoryPort.FindAll();
        _logger.LogInformation("Se obtuvieron {Count} personas", personas.Count());
        return personas;
    }
}