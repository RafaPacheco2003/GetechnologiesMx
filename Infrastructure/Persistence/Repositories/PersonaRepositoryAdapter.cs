using GetechnologiesMx.Domain.Models;
using GetechnologiesMx.Domain.Port.Out;
using GetechnologiesMx.Infrastructure.Mappers;
using GetechnologiesMx.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace GetechnologiesMx.Infrastructure.Persistence.Repositories;

public class PersonaRepositoryAdapter : PersonaRepositoryPort
{
    private readonly AppDbContext _context;

    public PersonaRepositoryAdapter(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Persona> StorePersona(Persona persona)
    {
        PersonaEntity entity = persona.ToEntity();
        _context.Personas.Add(entity);
        await _context.SaveChangesAsync();
        
        return entity.ToModel();
    }

    public async Task<Persona?> FindById(int id)
    {
        PersonaEntity? entity = await _context.Personas
            .Include(p => p.Facturas)
            .FirstOrDefaultAsync(p => p.Id == id);
        
        return entity?.ToModel();
    }

    public async Task<Persona?> FindByIdentification(string identificacion)
    {
        PersonaEntity? entity = await _context.Personas
            .FirstOrDefaultAsync(p => p.Identificacion == identificacion);
        
        return entity?.ToModel();
    }

    public async Task<IEnumerable<Persona>> FindAll()
    {
        List<PersonaEntity> entities = await _context.Personas
            .Include(p => p.Facturas)
            .ToListAsync();
        
        return entities.ToModelList();
    }

    public async Task<bool> Delete(int id)
    {
        PersonaEntity? entity = await _context.Personas.FindAsync(id);
        
        _context.Personas.Remove(entity);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> ExistsByIdentification(string identificacion)
    {
        return await _context.Personas
            .AnyAsync(p => p.Identificacion == identificacion);
    }
}
