namespace GetechnologiesMx.Domain.Models;

public class Persona
{
    public string Nombre { get; set; } = string.Empty;
    public string ApellidoPaterno { get; set; } = string.Empty;
    public string? ApellidoMaterno { get; set; }
    public string Identificacion { get; set; } = string.Empty;

    public Persona()
    {
    }

    public Persona(string nombre, string apellidoPaterno, string identificacion, string? apellidoMaterno = null)
    {
        Nombre = nombre;
        ApellidoPaterno = apellidoPaterno;
        Identificacion = identificacion;
        ApellidoMaterno = apellidoMaterno;
    }
}
