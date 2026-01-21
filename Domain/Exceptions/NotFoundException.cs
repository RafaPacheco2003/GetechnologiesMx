namespace GetechnologiesMx.Domain.Exceptions;

public class NotFoundException : Exception
{

    public NotFoundException(int id) : base($"Entidad no encontrada con ID: {id}")
    {
    }

}
