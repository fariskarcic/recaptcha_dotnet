using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using recaptcha_dotnet.Models;

namespace recaptcha_dotnet.Controllers
{
    // [Produces ("application/json")]
    [Route ("api/[controller]")]
    [ApiController]
    public class RecaptchaController : ControllerBase
    {
        [HttpPost]
        [Route("verify")]
        public async Task<IActionResult> Verify([FromQuery]string token){
            var data = new {
                secret = "6LeAc4YUAAAAAExP-dEJ34pIODZgo6G48l8HKiS8",
                response = token
            };
            var toSend = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response =  await client.PostAsync("https://www.google.com/recaptcha/api/siteverify?secret="+data.secret+"&response="+token, toSend);
            if (response != null)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<object>(jsonString);
                return Ok(obj);
            }

            return NotFound();
        }
    }
}
