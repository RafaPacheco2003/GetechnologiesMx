using GetechnologiesMx.Domain.Models;

namespace GetechnologiesMx.Domain.Port.Out;

public interface PersonaRepositoryPort
{
    Task<Persona> Create(Persona persona);
    Task<Persona?> FindById(int id);
    Task<Persona?> FindByIdentification(string identificacion);
    Task<IEnumerable<Persona>> FindAll();
    Task<bool> Delete(int id);
    Task<bool> ExistsByIdentification(string identificacion);
}
