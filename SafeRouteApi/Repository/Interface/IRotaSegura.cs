using SafeRoute.Models;
using System.Collections;

namespace SafeRoute.Repository.Interface
{
    public interface IRotaSegura
    {
        Task<IEnumerable> GetRotasSeguras();
        Task<RotasSegurancas> GetRotaSeguraById(int id);
        Task<RotasSegurancas> AdicionarRotaSegura(RotasSegurancas rotaSegura);
        Task<RotasSegurancas> AtualizarRotaSegura(RotasSegurancas rotaSegura);
        Task<RotasSegurancas> DeletarRotaSegura(int id);
    }
}
