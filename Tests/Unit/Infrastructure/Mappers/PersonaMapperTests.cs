using GetechnologiesMx.Domain.Models;
using GetechnologiesMx.Infrastructure.DTOs.Request;
using GetechnologiesMx.Infrastructure.DTOs.Response;
using GetechnologiesMx.Infrastructure.Mappers;
using GetechnologiesMx.Infrastructure.Persistence.Entities;
using Xunit;

namespace GetechnologiesMx.Tests.Unit.Infrastructure.Mappers;


public class PersonaMapperTests
{
    [Fact] 
    public void ToModel_FromRequest_ShouldMapCorrectly()
    {
        
        CreatePersonaRequest request = new CreatePersonaRequest(
            Nombre: "Juan",
            ApellidoPaterno: "Pérez",
            ApellidoMaterno: "García",
            Identificacion: "RFC123456"
        );

        
        Persona persona = request.ToModel();

        Assert.NotNull(persona);
        Assert.Equal("Juan", persona.Nombre);
        Assert.Equal("Pérez", persona.ApellidoPaterno);
        Assert.Equal("García", persona.ApellidoMaterno);
        Assert.Equal("RFC123456", persona.Identificacion);
        Assert.Null(persona.Id); // Nuevo modelo no tiene Id
    }

    [Fact]
    public void ToEntity_FromModel_ShouldMapCorrectly()
    {
        Persona persona = new Persona(
            nombre: "María",
            apellidoPaterno: "López",
            identificacion: "CURP789012",
            apellidoMaterno: "Hernández"
        )
        {
            Id = 5
        };

        PersonaEntity entity = persona.ToEntity();

        Assert.NotNull(entity);
        Assert.Equal(5, entity.Id);
        Assert.Equal("María", entity.Nombre);
        Assert.Equal("López", entity.ApellidoPaterno);
        Assert.Equal("Hernández", entity.ApellidoMaterno);
        Assert.Equal("CURP789012", entity.Identificacion);
    }

    [Fact]
    public void ToModel_FromEntity_ShouldMapCorrectly()
    {
        PersonaEntity entity = new PersonaEntity
        {
            Id = 10,
            Nombre = "Pedro",
            ApellidoPaterno = "Ramírez",
            ApellidoMaterno = "Sánchez",
            Identificacion = "ID987654",
            FechaCreacion = DateTime.UtcNow,
            FechaActualizacion = DateTime.UtcNow
        };

        Persona persona = entity.ToModel();

        Assert.NotNull(persona);
        Assert.Equal(10, persona.Id);
        Assert.Equal("Pedro", persona.Nombre);
        Assert.Equal("Ramírez", persona.ApellidoPaterno);
        Assert.Equal("Sánchez", persona.ApellidoMaterno);
        Assert.Equal("ID987654", persona.Identificacion);
    }

    [Fact]
    public void ToResponse_FromEntity_ShouldMapCorrectly()
    {
        
        PersonaEntity entity = new PersonaEntity
        {
            Id = 15,
            Nombre = "Ana",
            ApellidoPaterno = "Torres",
            ApellidoMaterno = null,
            Identificacion = "RFC456789"
        };

        
        PersonaResponse response = entity.ToResponse();

        
        Assert.NotNull(response);
        Assert.Equal(15, response.Id);
        Assert.Equal("Ana", response.Nombre);
        Assert.Equal("Torres", response.ApellidoPaterno);
        Assert.Null(response.ApellidoMaterno);
        Assert.Equal("RFC456789", response.Identificacion);
    }

    [Fact]
    public void ToResponse_FromModel_ShouldMapCorrectly()
    {
        
        Persona persona = new Persona(
            nombre: "Carlos",
            apellidoPaterno: "Gómez",
            identificacion: "ID111222",
            apellidoMaterno: "Díaz"
        )
        {
            Id = 20
        };

        
        PersonaResponse response = persona.ToResponse();

        
        Assert.NotNull(response);
        Assert.Equal(20, response.Id);
        Assert.Equal("Carlos", response.Nombre);
        Assert.Equal("Gómez", response.ApellidoPaterno);
        Assert.Equal("Díaz", response.ApellidoMaterno);
        Assert.Equal("ID111222", response.Identificacion);
    }

    [Fact]
    public void ToResponseList_FromEntityList_ShouldMapCorrectly()
    {
        
        List<PersonaEntity> entities = new List<PersonaEntity>
        {
            new PersonaEntity { Id = 1, Nombre = "Persona1", ApellidoPaterno = "Apellido1", Identificacion = "ID1" },
            new PersonaEntity { Id = 2, Nombre = "Persona2", ApellidoPaterno = "Apellido2", Identificacion = "ID2" },
            new PersonaEntity { Id = 3, Nombre = "Persona3", ApellidoPaterno = "Apellido3", Identificacion = "ID3" }
        };

        
        IEnumerable<PersonaResponse> responses = entities.ToResponseList();

        
        Assert.NotNull(responses);
        Assert.Equal(3, responses.Count());
        Assert.Equal("Persona1", responses.First().Nombre);
        Assert.Equal("Persona3", responses.Last().Nombre);
    }

    [Fact]
    public void ToModelList_FromEntityList_ShouldMapCorrectly()
    {
        
        List<PersonaEntity> entities = new List<PersonaEntity>
        {
            new PersonaEntity { Id = 1, Nombre = "Test1", ApellidoPaterno = "Test", Identificacion = "T1" },
            new PersonaEntity { Id = 2, Nombre = "Test2", ApellidoPaterno = "Test", Identificacion = "T2" }
        };

        
        IEnumerable<Persona> personas = entities.ToModelList();

        
        Assert.NotNull(personas);
        Assert.Equal(2, personas.Count());
        Assert.All(personas, p => Assert.NotNull(p.Id));
    }
}
