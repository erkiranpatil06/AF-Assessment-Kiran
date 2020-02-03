using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace AF.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Weather : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> forecast(string city)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var response = await client.GetAsync($"/data/2.5/forecast?q={city}&appid=fcadd28326c90c3262054e0e6ca599cd&units=metric");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(stringResult);
                    var rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(stringResult);
                    return Ok(new
                    {
                        City = rawWeather.City.Name,
                        List = rawWeather.List
                    });
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
            }

        }
    }
}