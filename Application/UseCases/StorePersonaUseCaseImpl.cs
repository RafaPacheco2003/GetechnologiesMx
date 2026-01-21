using GetechnologiesMx.Domain.Exceptions;
using GetechnologiesMx.Domain.Models;
using GetechnologiesMx.Domain.Port.In;
using GetechnologiesMx.Domain.Port.Out;

namespace GetechnologiesMx.Application.UseCases;

public class StorePersonaUseCaseImpl : StorePersonaUseCase
{
    private readonly PersonaRepositoryPort _personaRepositoryPort;

    public StorePersonaUseCaseImpl(PersonaRepositoryPort personaRepositoryPort)
    {
        _personaRepositoryPort = personaRepositoryPort;
    }

    public async Task<Persona> StorePersona(Persona persona)
    {
        
        return await _personaRepositoryPort.StorePersona(persona);
    }
}