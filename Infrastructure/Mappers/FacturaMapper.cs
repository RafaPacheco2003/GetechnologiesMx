using GetechnologiesMx.Domain.Models;
using GetechnologiesMx.Infrastructure.DTOs.Request;
using GetechnologiesMx.Infrastructure.DTOs.Response;
using GetechnologiesMx.Infrastructure.Persistence.Entities;

namespace GetechnologiesMx.Infrastructure.Mappers;

public static class FacturaMapper
{
    // REQUEST DTO -> DOMAIN MODEL
    public static Factura ToModel(this CreateFacturaRequest request)
    {
        return new Factura(
            numeroFactura: request.NumeroFactura,
            monto: request.Monto,
            fecha: request.Fecha,
            personaId: request.PersonaId
        );
    }

    // DOMAIN MODEL -> ENTITY
    public static FacturaEntity ToEntity(this Factura model)
    {
        return new FacturaEntity
        {
            Id = model.Id,
            NumeroFactura = model.NumeroFactura,
            Monto = model.Monto,
            Fecha = model.Fecha,
            PersonaId = model.PersonaId
        };
    }

    // ENTITY -> DOMAIN MODEL
    public static Factura ToModel(this FacturaEntity entity)
    {
        return new Factura(
            numeroFactura: entity.NumeroFactura,
            monto: entity.Monto,
            fecha: entity.Fecha,
            personaId: entity.PersonaId
        )
        {
            Id = entity.Id
        };
    }

    // ENTITY -> RESPONSE DTO
    public static FacturaResponse ToResponse(this FacturaEntity entity)
    {
        return new FacturaResponse(
            Id: entity.Id,
            NumeroFactura: entity.NumeroFactura,
            Monto: entity.Monto,
            Fecha: entity.Fecha,
            PersonaId: entity.PersonaId
        );
    }

    // DOMAIN MODEL -> RESPONSE DTO
    public static FacturaResponse ToResponse(this Factura model)
    {
        return new FacturaResponse(
            Id: model.Id,
            NumeroFactura: model.NumeroFactura,
            Monto: model.Monto,
            Fecha: model.Fecha,
            PersonaId: model.PersonaId
        );
    }

    // LIST ENTITY -> LIST RESPONSE
    public static IEnumerable<FacturaResponse> ToResponseList(this IEnumerable<FacturaEntity> entities)
    {
        return entities.Select(entity => entity.ToResponse());
    }

    // LIST ENTITY -> LIST MODEL
    public static IEnumerable<Factura> ToModelList(this IEnumerable<FacturaEntity> entities)
    {
        return entities.Select(entity => entity.ToModel());
    }

    // LIST MODEL -> LIST RESPONSE
    public static IEnumerable<FacturaResponse> ToResponseList(this IEnumerable<Factura> models)
    {
        return models.Select(model => model.ToResponse());
    }
}
