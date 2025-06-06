using System.Collections;
using SafeRoute.Models;

namespace SafeRoute.Repository.Interface
{
    public interface IAreaDeRisco
    {

        Task<IEnumerable> GetAreas();
        Task<AreasComRiscos> GetAreaById(int id);
        Task<AreasComRiscos> AdicionarArea(AreasComRiscos areaDeRisco);
        Task<AreasComRiscos> AtualizarArea(AreasComRiscos areaDeRisco);

        Task<AreasComRiscos> DeletarArea(int id);
    }
}
