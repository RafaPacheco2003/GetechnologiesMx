using GetechnologiesMx.Domain.Models;
using GetechnologiesMx.Domain.Port.In;

namespace GetechnologiesMx.Application.Services;


public class DirectorioService : 
    StorePersonaUseCase,
    RetrievePersonaUseCase,
    DeletePersonaUseCase
{
    private readonly StorePersonaUseCase _StorePersonaUseCase;
    private readonly RetrievePersonaUseCase _retrievePersonaUseCase;
    private readonly DeletePersonaUseCase _deletePersonaUseCase;

    public DirectorioService(
        StorePersonaUseCase StorePersonaUseCase,
        RetrievePersonaUseCase retrievePersonaUseCase,
        DeletePersonaUseCase deletePersonaUseCase)
    {
        _StorePersonaUseCase = StorePersonaUseCase;
        _retrievePersonaUseCase = retrievePersonaUseCase;
        _deletePersonaUseCase = deletePersonaUseCase;
    }

    public async Task<Persona> StorePersona(Persona persona)
    {
        return await _StorePersonaUseCase.StorePersona(persona);
    }





    public async Task<Persona?> FindById(int id)
    {
        return await _retrievePersonaUseCase.FindById(id);
    }

    public async Task<Persona?> FindByIdentification(string identificacion)
    {
        return await _retrievePersonaUseCase.FindByIdentification(identificacion);
    }

    public async Task<IEnumerable<Persona>> FindAll()
    {
        return await _retrievePersonaUseCase.FindAll();
    }

    public async Task<bool> Delete(int id)
    {
        return await _deletePersonaUseCase.Delete(id);
    }
}
