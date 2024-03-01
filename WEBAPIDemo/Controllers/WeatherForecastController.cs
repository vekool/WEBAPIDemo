using Microsoft.AspNetCore.Mvc;

namespace WEBAPIDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
       
       ///<summary>Get weather information (dummy</summary>
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("Test")]
        //Attribute routing <--- modern way
        public  string ShowSample()
        {
            return "Sample Data";
        }

        [HttpGet("Factorial")]
        public ActionResult<int> Factorial(int? num)
        {
            if (!num.HasValue)
            {
                return BadRequest("Number not provided");
            }
            if(num.Value < 0)
            {
                return BadRequest("Number must be positive");
            }
            int n = num.Value;
            int ans = 1;
            while (n > 1)
            {
                ans = ans * n;
                n--;
            }
            return Ok(ans);
        }
    }
}
