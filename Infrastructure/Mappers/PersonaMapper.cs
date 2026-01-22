using GetechnologiesMx.Domain.Models;
using GetechnologiesMx.Infrastructure.DTOs.Request;
using GetechnologiesMx.Infrastructure.DTOs.Response;
using GetechnologiesMx.Infrastructure.Persistence.Entities;

namespace GetechnologiesMx.Infrastructure.Mappers;

public static class PersonaMapper
{
    //  REQUEST DTO -> DOMAIN MODEL
    public static Persona ToModel(this CreatePersonaRequest request)
    {
        return new Persona(
            nombre: request.Nombre,
            apellidoPaterno: request.ApellidoPaterno,
            identificacion: request.Identificacion,
            apellidoMaterno: request.ApellidoMaterno
        );
    }

    //  DOMAIN MODEL -> ENTITY
    public static PersonaEntity ToEntity(this Persona model)
    {
        return new PersonaEntity
        {
            Id = model.Id ?? 0, 
            Nombre = model.Nombre,
            ApellidoPaterno = model.ApellidoPaterno,
            ApellidoMaterno = model.ApellidoMaterno,
            Identificacion = model.Identificacion,
            FechaCreacion = DateTime.UtcNow,
            FechaActualizacion = DateTime.UtcNow
        };
    }

 

    //  ENTITY -> DOMAIN MODEL
    public static Persona ToModel(this PersonaEntity entity)
    {
        return new Persona(
            nombre: entity.Nombre,
            apellidoPaterno: entity.ApellidoPaterno,
            identificacion: entity.Identificacion,
            apellidoMaterno: entity.ApellidoMaterno
        )
        {
            Id = entity.Id 
        };
    }

    //  ENTITY -> RESPONSE DTO
        public static PersonaResponse ToResponse(this PersonaEntity entity)
    {
        string nombreCompleto = $"{entity.Nombre} {entity.ApellidoPaterno}";
        if (!string.IsNullOrEmpty(entity.ApellidoMaterno))
        {
            nombreCompleto += $" {entity.ApellidoMaterno}";
        }
        
        return new PersonaResponse(
            Id: entity.Id,
            NombreCompleto: nombreCompleto,
            Identificacion: entity.Identificacion
        );
    }

    //  DOMAIN MODEL -> RESPONSE DTO
    public static PersonaResponse ToResponse(this Persona model)
    {
        string nombreCompleto = $"{model.Nombre} {model.ApellidoPaterno}";
        if (!string.IsNullOrEmpty(model.ApellidoMaterno))
        {
            nombreCompleto += $" {model.ApellidoMaterno}";
        }
        
        return new PersonaResponse(
            Id: model.Id ?? 0, 
            NombreCompleto: nombreCompleto,
            Identificacion: model.Identificacion
        );
    }

    public static IEnumerable<PersonaResponse> ToResponseList(this IEnumerable<PersonaEntity> entities)
    {
        return entities.Select(entity => entity.ToResponse());
    }

    public static IEnumerable<Persona> ToModelList(this IEnumerable<PersonaEntity> entities)
    {
        return entities.Select(entity => entity.ToModel());
    }

    public static IEnumerable<PersonaResponse> ToResponseList(this IEnumerable<Persona> models)
    {
        return models.Select(model => model.ToResponse());
    }
}
