namespace GetechnologiesMx.Domain.Exceptions;

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException(string message) : base(message)
    {
    }

    public AlreadyExistsException(string entityName, string propertyName, string value)
        : base($"{entityName} ya existe con {propertyName}: {value}")
    {
    }
}
