namespace GetechnologiesMx.Domain.Port.In;
using GetechnologiesMx.Domain.Models;

public interface RetrievePersonaUseCase
{
    Task<Persona?> FindById(int id);
    Task<Persona?> FindByIdentification(string identificacion);
    Task<IEnumerable<Persona>> FindAll();
    
}