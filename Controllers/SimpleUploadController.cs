using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace vue_experiments.Controllers
{
    public class SimpleUploadController : Controller
    {
        private readonly IHostingEnvironment _environment;
        public SimpleUploadController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            var uploadsRootFolder = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsRootFolder))
            {
                Directory.CreateDirectory(uploadsRootFolder);
            }

            var files = Request.Form.Files;
            foreach (var file in files)
            {
                if (file == null || file.Length == 0)
                {
                    continue;
                }

                var filePath = Path.Combine(uploadsRootFolder, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream).ConfigureAwait(false);
                }
            }

            return Content("Uploaded");
        }
    }
}