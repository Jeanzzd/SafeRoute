using Microsoft.AspNetCore.Mvc;
using SafeRoute.Models;
using SafeRoute.Repository;
using SafeRoute.Repository.Interface;
using System.Collections;

namespace SafeRouteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotaSegurancaController : Controller
    {
        private readonly IRotaSegura _rotaSeguraRepository;

        public RotaSegurancaController(IRotaSegura rotasegura)
        {
            _rotaSeguraRepository = rotasegura;
        }

        /// <summary>
        /// Retorna todas as rotas de segurança cadastradas no sistema.
        /// </summary>
        /// <returns>Lista de rotas de segurança</returns>
        /// <response code="200">Retorna a lista de rotas</response>
        /// <response code="500">Erro interno ao buscar as rotas</response>
        /// <response code="404">Nenhuma rota encontrada</response>



        [HttpGet]
        public async Task<ActionResult<IEnumerable>> GetRota()
        {
            try
            {
                var Area = await _rotaSeguraRepository.GetRotasSeguras();

                return Ok(Area);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao obter areas: " + ex.Message);
            }
        }


        /// <summary>
        /// Cadastra uma nova rota de segurança.
        /// </summary>
        /// <param name="rotaSegura">Dados da rota a ser cadastrada</param>
        /// <returns>Rota cadastrada</returns>
        /// <response code="201">Rota cadastrada com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="500">Erro interno ao cadastrar rota</response>
        [HttpPost]
        public async Task<ActionResult> PostRotaSegura([FromBody] RotasSegurancas rotaSegura)
        {
            try
            {
                if (rotaSegura == null)
                {
                    return BadRequest("Dados inválidos");
                }

                var rota = await _rotaSeguraRepository.AdicionarRotaSegura(rotaSegura);
                return CreatedAtAction(nameof(GetRota), new { id = rota.RotaId }, rota);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao salvar a rota: " + ex.Message);
            }
        }

        /// <summary>
        /// Retorna uma rota de segurança específica pelo ID.
        /// </summary>
        /// <param name="id">ID da rota</param>
        /// <returns>Rota correspondente ao ID</returns>
        /// <response code="200">Retorna a rota encontrada</response>
        /// <response code="404">Rota não encontrada</response>
        /// <response code="500">Erro interno ao buscar a rota</response>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RotasSegurancas>> GetRota(int id)
        {
            try
            {
                var result = await _rotaSeguraRepository.GetRotaSeguraById(id);
                if (result == null) return NotFound();

                return result;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter Rota");
            }
        }

        /// <summary>
        /// Remove uma rota de segurança pelo ID.
        /// </summary>
        /// <param name="id">ID da rota a ser deletada</param>
        /// <returns>Mensagem de confirmação</returns>
        /// <response code="200">Rota deletada com sucesso</response>
        /// <response code="404">Rota não encontrada</response>

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletarRota(int id)
        {
            try
            {
                var RotaToDelete = await _rotaSeguraRepository.GetRotaSeguraById(id);

                if (RotaToDelete == null)
                    return NotFound($"Rota com id {id} não encontrado");

                await _rotaSeguraRepository.DeletarRotaSegura(id);

                return Ok($"Rota com id {id} deletado");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar Rota");
            }
        }

        /// <summary>
        /// Atualiza os dados de uma rota de segurança existente.
        /// </summary>
        /// <param name="id">ID da rota</param>
        /// <param name="rotaSegura">Dados atualizados da rota</param>
        /// <returns>Rota atualizada</returns>
        /// <response code="200">Rota atualizada com sucesso</response>
        /// <response code="400">ID da URL não corresponde ao ID do corpo da requisição</response>
        /// <response code="404">Rota não encontrada</response>
        /// <response code="500">Erro interno ao atualizar a rota</response>

        [HttpPut("{id:int}")]
        public async Task<ActionResult<RotasSegurancas>> AtualizarRota(int id, [FromBody] RotasSegurancas rotaSegura)
        {
            try
            {
                if (id != rotaSegura.RotaId)
                {
                    return BadRequest("ID da rota não corresponde ao ID fornecido na URL.");
                }
                var existingRota = await _rotaSeguraRepository.GetRotaSeguraById(id);
                if (existingRota == null)
                {
                    return NotFound($"Rota com id {id} não encontrado");
                }
                var updatedRota = await _rotaSeguraRepository.AtualizarRotaSegura(rotaSegura);
                return Ok(updatedRota);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar Rota: " + ex.Message);
            }
        }
    }
}
