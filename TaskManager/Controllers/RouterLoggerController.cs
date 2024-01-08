using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace TaskManager_UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class RouterLoggerController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public RouterLoggerController(IWebHostEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("logger")]
        public async Task<IActionResult> Index()
        {

            string? logMessage = null;
            StreamReader streamReader = new StreamReader(HttpContext.Request.Body, Encoding.ASCII);
            logMessage = await streamReader.ReadToEndAsync(CancellationToken.None) + "\n";
           
            string filePath = this._hostingEnvironment.ContentRootPath + "\\RouterLogger.txt";
            System.IO.File.AppendAllText(filePath, logMessage);
            return Ok();
        }

    }
}
