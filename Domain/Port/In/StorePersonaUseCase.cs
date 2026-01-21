namespace GetechnologiesMx.Domain.Port.In;
using GetechnologiesMx.Domain.Models;

public interface StorePersonaUseCase
{
    Task<Persona> StorePersona(Persona persona);
}