using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcNetCoreUtilidades.Helpers;
using System.IO;
using System.Threading.Tasks;

namespace MvcNetCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        private HelperPathProvider helper;
        public UploadFilesController(HelperPathProvider helper)
        {
            this.helper = helper;
        }
        public IActionResult SubirFile()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SubirFile(IFormFile fichero)
        {
            string fileName = fichero.FileName;
            string path = 
                this.helper.MapPath(fileName, Folders.Images);

            //PARA SUBIR FICHEROS SE UTILIZA Stream
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            string urlPath = this.helper.MapUrlPath(fileName, Folders.Images);
            ViewData["MENSAJE"] = "Fichero subido a " + path;
            ViewData["PATH"] = urlPath;
            return View();
        }
    }
}
