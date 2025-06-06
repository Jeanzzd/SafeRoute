using Microsoft.AspNetCore.Mvc;
using SafeRoute.Models;
using SafeRoute.Repository;
using SafeRoute.Repository.Interface;
using System.Collections;

namespace SafeRouteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaComRiscoController : Controller
    {
        private readonly IAreaDeRisco _areaDeRiscoRepository;

        public AreaComRiscoController(IAreaDeRisco arearepository)
        {
            _areaDeRiscoRepository = arearepository;
        }

        /// <summary>
        /// Lista todas as áreas com risco cadastradas.
        /// </summary>
        /// <remarks>
        /// Exemplo de resposta:
        /// 
        ///     [
        ///         {
        ///             "areaId": 1,
        ///             "latitude": -23.55052,
        ///             "longitude": -46.633308,
        ///             "descricao": "Área com alto índice de assaltos"
        ///         }
        ///     ]
        /// </remarks>
        /// <returns>Lista de áreas com risco</returns>
        /// <response code="200">Retorna a lista de áreas</response>
        /// <response code="500">Erro interno ao obter as áreas</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable>> GetArea()
        {
            try
            {
                var Area = await _areaDeRiscoRepository.GetAreas();

                return Ok(Area);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao obter areas: " + ex.Message);
            }

        }

        /// <summary>
        /// Cadastra uma nova área com risco.
        /// </summary>  
        /// <param name="area">Objeto da área com risco a ser cadastrada</param>
        /// <returns>Área criada</returns>
        /// <response code="201">Área criada com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="500">Erro interno ao salvar a área</response>
        [HttpPost]
        public async Task<IActionResult> PostAreacomRisco([FromBody] AreasComRiscos area)
        {
            try
            {
                if (area == null)
                {
                    return BadRequest("Dados inválidos");
                }

                var areas = await _areaDeRiscoRepository.AdicionarArea(area);
                return CreatedAtAction(nameof(GetArea), new { id = areas.AreaId }, areas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao salvar a area: " + ex.Message);
            }

        }

        /// <summary>
        /// Retorna uma área com risco específica pelo ID.
        /// </summary>
        /// <param name="id">ID da área</param>
        /// <response code="200">Área encontrada</response>
        /// <response code="404">Área não encontrada</response>
        /// <response code="500">Erro interno ao obter a área</response>

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AreasComRiscos>> GetArea(int id)
        {
            try
            {
                var result = await _areaDeRiscoRepository.GetAreaById(id);
                if (result == null) return NotFound();

                return result;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter Area");
            }
        }


        /// <summary>
        /// Remove uma área com risco pelo ID.
        /// </summary>
        /// <param name="id">ID da área</param>
        /// <response code="200">Área deletada com sucesso</response>
        /// <response code="404">Área não encontrada</response>
        /// <response code="500">Erro interno ao deletar a área</response>
        /// 
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletarArea(int id)
        {
            try
            {
                var AreaToDelete = await _areaDeRiscoRepository.GetAreaById(id);

                if (AreaToDelete == null)
                    return NotFound($"Area com id {id} não encontrado");

                await _areaDeRiscoRepository.DeletarArea(id);

                return Ok($"Area com id {id} deletado");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar Area");
            }
        }

        /// <summary>
        /// Atualiza os dados de uma área com risco.
        /// </summary>
        /// <param name="id">ID da área a ser atualizada</param>
        /// <param name="areas">Objeto da área com novos dados</param>
        /// <response code="200">Área atualizada com sucesso</response>
        /// <response code="400">ID da área não corresponde ao fornecido na URL</response>
        /// <response code="404">Área não encontrada</response>
        /// <response code="500">Erro interno ao atualizar a área</response>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<AreasComRiscos>> AtualizarArea(int id, [FromBody] AreasComRiscos areas)
        {
            try
            {
                if (id != areas.AreaId)
                {
                    return BadRequest("ID da area não corresponde ao ID fornecido na URL.");
                }
                var existingRota = await _areaDeRiscoRepository.GetAreaById(id);
                if (existingRota == null)
                {
                    return NotFound($"area com id {id} não encontrado");
                }
                var updatedRota = await _areaDeRiscoRepository.AtualizarArea(areas);
                return Ok(updatedRota);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar Area: " + ex.Message);
            }
        }
    }
}
