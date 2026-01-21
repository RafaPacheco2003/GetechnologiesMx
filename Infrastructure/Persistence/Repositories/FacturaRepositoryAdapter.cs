using GetechnologiesMx.Domain.Models;
using GetechnologiesMx.Domain.Port.Out;
using GetechnologiesMx.Infrastructure.Mappers;
using GetechnologiesMx.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace GetechnologiesMx.Infrastructure.Persistence.Repositories;

public class FacturaRepositoryAdapter : FacturaRepositoryPort
{
    private readonly AppDbContext _context;

    public FacturaRepositoryAdapter(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Factura> StoreFactura(Factura factura)
    {
        FacturaEntity entity = factura.ToEntity();
        _context.Facturas.Add(entity);
        await _context.SaveChangesAsync();
        
        return entity.ToModel();
    }

    public async Task<Factura?> FindById(int id)
    {
        FacturaEntity? entity = await _context.Facturas
            .Include(f => f.Persona)
            .FirstOrDefaultAsync(f => f.Id == id);
        
        return entity?.ToModel();
    }

    public async Task<Factura?> FindByNumeroFactura(string numeroFactura)
    {
        FacturaEntity? entity = await _context.Facturas
            .Include(f => f.Persona)
            .FirstOrDefaultAsync(f => f.NumeroFactura == numeroFactura);
        
        return entity?.ToModel();
    }

    public async Task<IEnumerable<Factura>> FindAll()
    {
        List<FacturaEntity> entities = await _context.Facturas
            .Include(f => f.Persona)
            .ToListAsync();
        
        return entities.ToModelList();
    }

    public async Task<IEnumerable<Factura>> FindByPersona(int personaId)
    {
        List<FacturaEntity> entities = await _context.Facturas
            .Include(f => f.Persona)
            .Where(f => f.PersonaId == personaId)
            .ToListAsync();
        
        return entities.ToModelList();
    }

    public async Task<bool> Delete(int id)
    {
        FacturaEntity? entity = await _context.Facturas.FindAsync(id);
        
        if (entity == null)
        {
            return false;
        }
        
        _context.Facturas.Remove(entity);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> ExistsByNumeroFactura(string numeroFactura)
    {
        return await _context.Facturas
            .AnyAsync(f => f.NumeroFactura == numeroFactura);
    }
}
