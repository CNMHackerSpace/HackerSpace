using Blazorise;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data.Interfaces;
using Shared.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<UploadController> _logger;

        public UploadController(ILogger<UploadController> logger, IWebHostEnvironment env, IBadgesRepo badgesRepo)
        {
            _env = env;
            _logger = logger;
        }

        [HttpPost]
        public async Task<string> Post([FromBody] ImageFile file, int badgeId)
        {
            _logger.Log(LogLevel.Information, "UploadeController post executed.");

            var buf = Convert.FromBase64String(file.base64data);
            string path = _env.ContentRootPath + System.IO.Path.DirectorySeparatorChar+ "UploadedImages" + System.IO.Path.DirectorySeparatorChar + Guid.NewGuid().ToString("N") + "-" + file.fileName;
            await System.IO.File.WriteAllBytesAsync(path, buf);
            return path;
        }
    }
}
