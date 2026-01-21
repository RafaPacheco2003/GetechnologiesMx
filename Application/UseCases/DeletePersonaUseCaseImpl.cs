using GetechnologiesMx.Domain.Exceptions;
using GetechnologiesMx.Domain.Models;
using GetechnologiesMx.Domain.Port.In;
using GetechnologiesMx.Domain.Port.Out;

namespace GetechnologiesMx.Application.UseCases;

public class DeletePersonaUseCaseImpl : DeletePersonaUseCase
{
    private readonly PersonaRepositoryPort _personaRepositoryPort;

    public DeletePersonaUseCaseImpl(PersonaRepositoryPort personaRepositoryPort)
    {
        _personaRepositoryPort = personaRepositoryPort;
    }

    public async Task<bool> Delete(int id)
    {
        
        Persona? persona = await _personaRepositoryPort.FindById(id);
        
        if (persona == null)
        {
            throw new NotFoundException("Persona", id);
        }

        return await _personaRepositoryPort.Delete(id);
    }
}
