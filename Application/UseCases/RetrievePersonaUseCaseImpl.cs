using GetechnologiesMx.Domain.Exceptions;
using GetechnologiesMx.Domain.Models;
using GetechnologiesMx.Domain.Port.In;
using GetechnologiesMx.Domain.Port.Out;

namespace GetechnologiesMx.Application.UseCases;

public class RetrievePersonaUseCaseImpl : RetrievePersonaUseCase
{
    private readonly PersonaRepositoryPort _personaRepositoryPort;

    public RetrievePersonaUseCaseImpl(PersonaRepositoryPort personaRepositoryPort)
    {
        _personaRepositoryPort = personaRepositoryPort;
    }

    public async Task<Persona?> FindById(int id)
    {
        Persona? persona = await _personaRepositoryPort.FindById(id);
        
        if (persona == null)
        {
            throw new NotFoundException("Persona", id);
        }
        
        return persona;
    }

    public async Task<Persona?> FindByIdentification(string identificacion)
    {
       

        Persona? persona = await _personaRepositoryPort.FindByIdentification(identificacion);
        
        if (persona == null)
        {
            throw new NotFoundException("Persona", "Identificacion", identificacion);
        }
        
        return persona;
    }

    public async Task<IEnumerable<Persona>> FindAll()
    {
        IEnumerable<Persona> personas = await _personaRepositoryPort.FindAll();
        return personas;
    }
}