using Microsoft.AspNetCore.Mvc.RazorPages;
using SafeRoute.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class RotasSegurancaModel : PageModel
{
    private readonly HttpClient _httpClient;

    public RotasSegurancaModel(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public List<RotasSegurancas> Rotas { get; set; }

    public async Task OnGetAsync()
    {
        // URL da API que retorna as rotas de segurança
        var apiUrl = "https://localhost:7141/api/RotaSeguranca"; // ajuste conforme sua rota real

        Rotas = await _httpClient.GetFromJsonAsync<List<RotasSegurancas>>(apiUrl);
    }
}
