using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SafeRoute.Models;
using SafeRoute.Repository.Interface;
using SafeRouteApi.Data;

namespace SafeRoute.Repository
{
    public class AreaDeRiscoRepository : IAreaDeRisco
    {
        private readonly dbContext _context;

        public AreaDeRiscoRepository(dbContext context)
        {
            _context = context;
        }

        public async Task<AreasComRiscos> AdicionarArea(AreasComRiscos area)
        {
            var result = await _context.AreasComRiscos.AddAsync(area);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<AreasComRiscos> AtualizarArea(AreasComRiscos areaDeRisco)
        {
            var existing = await _context.AreasComRiscos.FirstOrDefaultAsync(a => a.AreaId == areaDeRisco.AreaId);
            if (existing != null)
            {
                existing.Nome = areaDeRisco.Nome;
                existing.Descricao = areaDeRisco.Descricao;
                existing.TipoDeRisco = areaDeRisco.TipoDeRisco;
                existing.Latitude = areaDeRisco.Latitude;
                existing.Longitude = areaDeRisco.Longitude;

                await _context.SaveChangesAsync();
                return existing;
            }
            return null;
        }

        public async Task<AreasComRiscos> DeletarArea(int id)
        {
            var area = await _context.AreasComRiscos.FirstOrDefaultAsync(a => a.AreaId == id);
            if (area != null)
            {
                _context.AreasComRiscos.Remove(area);
                await _context.SaveChangesAsync();
                return area;
            }
            return null;
        }

        public async Task<AreasComRiscos> GetAreaById(int id)
        {
             return await _context.AreasComRiscos
        .Include(a => a.RotasSeguras) // <- isso é essencial
        .FirstOrDefaultAsync(a => a.AreaId == id);
        }

        public async Task<IEnumerable> GetAreas()
        {
            return await _context.AreasComRiscos
                .Include(a => a.RotasSeguras) // Inclui as rotas seguras associadas
                .ToListAsync();

        }
    }
}
