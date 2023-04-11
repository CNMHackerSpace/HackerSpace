using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment env;

        public UploadController(IWebHostEnvironment env)
        {
            this.env = env;
        }

        [HttpPost]
        public async Task Post([FromBody] ImageFile file)
        {
            var buf = Convert.FromBase64String(file.base64data);
            string path = env.ContentRootPath + System.IO.Path.DirectorySeparatorChar+"images" + System.IO.Path.DirectorySeparatorChar + Guid.NewGuid().ToString("N") + "-" + file.fileName;
            await System.IO.File.WriteAllBytesAsync(path, buf);
        }
    }
}
