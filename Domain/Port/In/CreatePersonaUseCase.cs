
namespace GetechnologiesMx.Domain.Port.In;
using GetechnologiesMx.Domain.Models;

public interface CreatePersonaUseCase
{
    Task<Persona> Create(Persona persona);
}