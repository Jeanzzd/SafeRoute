using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using SafeRoute.Models; // ajuste conforme o seu projeto

public class AreaRiscosModel : PageModel
{
    private readonly HttpClient _httpClient;

    public List<AreasComRiscos> Areas { get; set; }

    public AreaRiscosModel(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task OnGetAsync()
    {
        var response = await _httpClient.GetAsync("https://localhost:7141/api/AreaComRisco"); // ajuste o endpoint conforme sua rota

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            Areas = JsonSerializer.Deserialize<List<AreasComRiscos>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        else
        {
            Areas = new List<AreasComRiscos>(); // evitar null
        }
    }
}
