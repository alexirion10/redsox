using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace redsox
{
    [ApiVersion("1.0")]
    [ApiController]
    [AllowAnonymous]
    [Route("[controller/v{version:apiVersion}")]
    public class TestController : Controller
    {
        private static readonly string[] s_summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = s_summaries[rng.Next(s_summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public WeatherForecast Get(int index)
        {
            return new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = new Random().Next(0, 100),
                Summary = s_summaries[index]
            };
        }

        [HttpGet("hello-world")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public string helloWorld()
        {
            return "Hello World!";
        }

        [HttpGet("status-check")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult statusCheck()
        {
            return NoContent();
        }

        [HttpGet("about")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public string about()
        {
            var val = new StringBuilder();
            val.AppendLine(System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString());
            val.AppendLine(System.Reflection.Assembly.GetEntryAssembly().GetName().FullName);
            val.AppendLine(System.Reflection.Assembly.GetEntryAssembly().GetName().CodeBase);
            val.AppendLine(System.Reflection.Assembly.GetEntryAssembly().ImageRuntimeVersion);
            return val.ToString();
        }
    }

    namespace v2
    {
        [ApiVersion("2.0")]
        [ApiController]
        [AllowAnonymous]
        [Route("[controller/v{version:apiVersion}")]
        public class TestController : Controller
        {
            [HttpGet("hello-world")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public string helloWorld()
            {
                return "Hello World!";
            }
        }
    }


}
