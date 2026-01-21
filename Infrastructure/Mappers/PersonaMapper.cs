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
            Id = model.Id ?? 0, // Si es null (nuevo), EF asignará el Id
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
            Id = entity.Id // Asignar el Id de la Entity al Model
        };
    }

    //  ENTITY -> RESPONSE DTO
        public static PersonaResponse ToResponse(this PersonaEntity entity)
    {
        return new PersonaResponse(
            Id: entity.Id,
            Nombre: entity.Nombre,
            ApellidoPaterno: entity.ApellidoPaterno,
            ApellidoMaterno: entity.ApellidoMaterno,
            Identificacion: entity.Identificacion
        );
    }

    //  DOMAIN MODEL -> RESPONSE DTO
    public static PersonaResponse ToResponse(this Persona model)
    {
        return new PersonaResponse(
            Id: model.Id ?? 0, // Si es null, usa 0 (no debería pasar si viene de BD)
            Nombre: model.Nombre,
            ApellidoPaterno: model.ApellidoPaterno,
            ApellidoMaterno: model.ApellidoMaterno,
            Identificacion: model.Identificacion
        );
    }

    //  CONVERSIONES
    public static IEnumerable<PersonaResponse> ToResponseList(this IEnumerable<PersonaEntity> entities)
    {
        return entities.Select(entity => entity.ToResponse());
    }

    public static IEnumerable<Persona> ToModelList(this IEnumerable<PersonaEntity> entities)
    {
        return entities.Select(entity => entity.ToModel());
    }

    // LISTA DE MODELS -> LISTA DE RESPONSES
    public static IEnumerable<PersonaResponse> ToResponseList(this IEnumerable<Persona> models)
    {
        return models.Select(model => model.ToResponse());
    }
}
