using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Config;
using Demo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Demo.Filters;

namespace Demo.Controllers
{
    [CustomFilter("controller")]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, 
            IGuidService guidService,
            TestService tService, 
           // IConfiguration config,
            IOptions<EmailConfig> emailConfig)
        {
            _guidService = guidService;
            _tService = tService;
           // _Config = config;
            _emailConfig = emailConfig.Value;
            _logger = logger;
        }

        private IGuidService _guidService { get; }

        private readonly TestService _tService;

        private IConfiguration _Config { get; }

        private readonly EmailConfig _emailConfig;

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            (DateTime.Now,rng.Next(-20, 55),Summaries[rng.Next(Summaries.Length)]
            ))
            .ToArray();

        }

        [HttpGet]
        [Route("GetConfig")]
        [CustomFilter("action")]
        public String GetConfig(string key)
        {
            _logger.Log(LogLevel.Information, "get config executing..");
            _logger.LogDebug("debug log");
            _logger.LogError("error log");
            _logger.LogWarning("worning log");
          Console.WriteLine("Action executing..");
          return _emailConfig.From + " "+ _emailConfig.Retry;
        }

    }
}
