
namespace GetechnologiesMx.Domain.Port.In;

using GetechnologiesMx.Domain.Models;

public interface DeletePersonaUseCase
{
    Task<bool> Delete(int id);
}