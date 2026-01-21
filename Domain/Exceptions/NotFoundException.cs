namespace GetechnologiesMx.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string entityName, int id) 
        : base($"{entityName} no encontrada con ID: {id}")
    {
    }

    public NotFoundException(string entityName, string propertyName, string value)
        : base($"{entityName} no encontrada con {propertyName}: {value}")
    {
    }
}
