using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SafeRoute.Models;
using SafeRoute.Repository.Interface;
using SafeRouteApi.Data;

namespace SafeRoute.Repository
{
    public class RotaSeguraRepository : IRotaSegura
    {
        private readonly dbContext _context;

        public RotaSeguraRepository(dbContext context)
        {
            _context = context;
        }

        public async Task<RotasSegurancas> AdicionarRotaSegura(RotasSegurancas rotaSegura)
        {
            var result = await _context.RotasSegurancas.AddAsync(rotaSegura);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<RotasSegurancas> AtualizarRotaSegura(RotasSegurancas rotaSegura)
        {
            var existe = await _context.RotasSegurancas.FirstOrDefaultAsync(r => r.RotaId == rotaSegura.RotaId);
            if (existe != null)
            {
                existe.Nome = rotaSegura.Nome;
                existe.Instrucoes = rotaSegura.Instrucoes;
                existe.PontosDePassagem = rotaSegura.PontosDePassagem;
                existe.AreaDeRiscoId = rotaSegura.AreaDeRiscoId;

                await _context.SaveChangesAsync();
                return existe;
            }
            return null;
        }

        public async Task<RotasSegurancas> DeletarRotaSegura(int id)
        {
            var rota = await _context.RotasSegurancas.FirstOrDefaultAsync(r => r.RotaId == id);
            if (rota != null)
            {
                _context.RotasSegurancas.Remove(rota);
                await _context.SaveChangesAsync();
                return rota;
            }
            return null;
        }

        public async Task<RotasSegurancas> GetRotaSeguraById(int id)
        {
            return await _context.RotasSegurancas.FirstOrDefaultAsync(x => x.RotaId == id);
        }

        public async Task<IEnumerable> GetRotasSeguras()
        {
            return await _context.RotasSegurancas.ToListAsync();
        }
    }
}
