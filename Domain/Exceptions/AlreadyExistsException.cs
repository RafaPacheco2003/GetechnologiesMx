namespace GetechnologiesMx.Domain.Exceptions;

/// <summary>
/// Excepción cuando ya existe una entidad (duplicado)
/// Equivalente a DuplicateException o ConflictException en Spring Boot
/// </summary>
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
