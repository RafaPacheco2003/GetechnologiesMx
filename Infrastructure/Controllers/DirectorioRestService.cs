using GetechnologiesMx.Application.Services;
using GetechnologiesMx.Domain.Models;
using GetechnologiesMx.Infrastructure.DTOs.Request;
using GetechnologiesMx.Infrastructure.DTOs.Response;
using GetechnologiesMx.Infrastructure.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace GetechnologiesMx.Infrastructure.Controllers;

[ApiController]
[Route("api/personas")]
public class DirectorioRestService : ControllerBase
{
    private readonly DirectorioService _DirectorioService;

    public DirectorioRestService(DirectorioService DirectorioService)
    {
        _DirectorioService = DirectorioService;
    }

    [HttpPost]
    public async Task<ActionResult<PersonaResponse>> StorePersona([FromBody] CreatePersonaRequest request)
    {
        Persona persona = request.ToModel();
        Persona personaCreated = await _DirectorioService.StorePersona(persona);
        PersonaResponse response = personaCreated.ToResponse();
        
        return CreatedAtAction(nameof(FindByIdentification), new { identificacion = response.Identificacion }, response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonaResponse>>> FindAll()
    {
        IEnumerable<Persona> personas = await _DirectorioService.FindAll();
        IEnumerable<PersonaResponse> response = personas.ToResponseList();
        
        return Ok(response);
    }

    [HttpGet("identificacion/{identificacion}")]
    public async Task<ActionResult<PersonaResponse>> FindByIdentification(string identificacion)
    {
        Persona? persona = await _DirectorioService.FindByIdentification(identificacion);
        PersonaResponse response = persona.ToResponse();
        
        return Ok(response);
    }

    [HttpDelete("identificacion/{identificacion}")]
    public async Task<ActionResult> DeleteByIdentification(string identificacion)
    {
        Persona? persona = await _DirectorioService.FindByIdentification(identificacion);
        await _DirectorioService.Delete(persona.Id ?? 0);
        return NoContent();
    }
}
