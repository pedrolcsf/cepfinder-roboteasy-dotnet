using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http;
using System.Threading.Tasks;

namespace CepFinder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CepController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;

        public CepController(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }

        [HttpGet("{cep}")]
        public async Task<IActionResult> GetEnderecoPorCep(string cep)
        {
            if(_cache.TryGetValue(cep, out var enderecoCache))
            {
                return Ok(enderecoCache);
            }   

            if (string.IsNullOrEmpty(cep) || cep.Length != 8)
                return BadRequest("O CEP deve ter 8 caracteres.");

            var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");

            if (!response.IsSuccessStatusCode)
                return NotFound("CEP não encontrado.");

            var endereco = await response.Content.ReadAsStringAsync();
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(5)
            };

            _cache.Set(cep, endereco, cacheEntryOptions);

            return Ok(endereco);
        }
    }
}
