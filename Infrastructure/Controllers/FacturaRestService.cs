using GetechnologiesMx.Application.Services;
using GetechnologiesMx.Domain.Models;
using GetechnologiesMx.Infrastructure.DTOs.Request;
using GetechnologiesMx.Infrastructure.DTOs.Response;
using GetechnologiesMx.Infrastructure.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace GetechnologiesMx.Infrastructure.Controllers;

[ApiController]
[Route("api/facturas")]
public class FacturaRestService : ControllerBase
{
    private readonly VentaService _VentaService;

    public FacturaRestService(VentaService VentaService)
    {
        _VentaService = VentaService;
    }

    [HttpPost]
    public async Task<ActionResult<FacturaResponse>> Create([FromBody] CreateFacturaRequest request)
    {
        Factura factura = request.ToModel();
        Factura facturaCreated = await _VentaService.StoreFactura(factura);
        FacturaResponse response = facturaCreated.ToResponse();
        
        return CreatedAtAction(nameof(FindByPersona), new { personaId = response.PersonaId }, response);
    }

    [HttpGet("persona/{personaId}")]
    public async Task<ActionResult<IEnumerable<FacturaResponse>>> FindByPersona(int personaId)
    {
        IEnumerable<Factura> facturas = await _VentaService.FindByPersona(personaId);
        IEnumerable<FacturaResponse> response = facturas.ToResponseList();
        
        return Ok(response);
    }
}
