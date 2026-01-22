using GetechnologiesMx.Domain.Exceptions;
using GetechnologiesMx.Domain.Models;
using GetechnologiesMx.Domain.Port.In;
using GetechnologiesMx.Domain.Port.Out;

namespace GetechnologiesMx.Application.UseCases;

public class DeletePersonaUseCaseImpl : DeletePersonaUseCase
{
    private readonly PersonaRepositoryPort _personaRepositoryPort;
    private readonly ILoggerPort _logger;

    public DeletePersonaUseCaseImpl(
        PersonaRepositoryPort personaRepositoryPort,
        ILoggerPort logger)
    {
        _personaRepositoryPort = personaRepositoryPort;
        _logger = logger;
    }

    public async Task<bool> Delete(int id)
    {
        _logger.LogInformation("Intentando eliminar persona con ID: {Id}", id);
        
        Persona? persona = await _personaRepositoryPort.FindById(id);
        
        if (persona == null)
        {
            _logger.LogWarning("No se puede eliminar. Persona con ID: {Id} no encontrada", id);
            throw new NotFoundException("Persona", id);
        }

        var result = await _personaRepositoryPort.Delete(id);
        
        
        
        return result;
    }
}
