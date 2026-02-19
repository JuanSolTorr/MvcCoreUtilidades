using Microsoft.AspNetCore.Mvc;

namespace MvcCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        private IWebHostEnvironment hostEnviroment;

        public UploadFilesController(IWebHostEnvironment hostEnviroment)
        {
            this.hostEnviroment = hostEnviroment;
        }

        public IActionResult SubirFiles()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubirFiles(IFormFile fichero)
        {
            // Vamos a subir el fichero a los elementos temporales del equipo
            string rootFolder = this.hostEnviroment.WebRootPath;
            string fileName = fichero.FileName;
            string path = Path.Combine(rootFolder, "uploads" ,fileName);
            using(Stream stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            ViewData["MENSAJE"] = "Fichero subido a " + path;
            ViewData["FILENAME"] = fileName;
            return View();
        }
    }
}
