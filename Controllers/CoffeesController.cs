using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using vue_experiments.Models;
using vue_experiments.Services;

namespace vue_experiments.Controllers
{
    public class CoffeesController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private readonly ICoffeeService _coffeeService;

        public CoffeesController(IHostingEnvironment environment, ICoffeeService coffeeService)
        {
            _environment = environment;
            _coffeeService = coffeeService;
        }

        public async Task<IActionResult> SaveCoffee(Coffee coffee)
        {
            // Uploading files
            var fileName = await UploadFiles();

            // Saving data
            coffee.Image = fileName;
            _coffeeService.Add(coffee);
            _coffeeService.SaveChanges();

            return Json("Coffee has been saved!");
        }

        private async Task<string> UploadFiles()
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
                    return file.FileName;
                }
            }

            return string.Empty;
        }
    }
}