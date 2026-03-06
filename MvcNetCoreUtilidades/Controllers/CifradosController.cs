using Microsoft.AspNetCore.Mvc;
using MvcNetCoreUtilidades.Helpers;

namespace MvcNetCoreUtilidades.Controllers
{
    public class CifradosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CifradoEficiente()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CifradoEficiente(string contenido, string resultado, string accion)
        {
            if (accion.ToLower() == "cifrar")
            {
                string response = HelperCrytography.CifrarContenido(contenido, false);
                ViewData["TEXTOCIFRADO"] = response;
                ViewData["SALT"] = HelperCrytography.Salt;
            }
            else if (accion.ToLower() == "comparar")
            {
                string response = HelperCrytography.CifrarContenido(contenido, true);
                if (response != resultado)
                {
                    ViewData["MENSAJE"] = "El texto es incorrecto";
                }
                else
                {
                    ViewData["MENSAJE"] = "El texto es correcto";
                }
            }
                return View();
        }
        public IActionResult CifradoBasico()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CifradoBasico(string contenido, string resultado, string accion)
        {
            string response = HelperCrytography.EncriptarTextoBasico(contenido);
            if (accion.ToLower() == "cifrar")
            {
                ViewData["TEXTOCIFRADO"] = response;
            }else if (accion.ToLower() == "comparar")
            {
                //Si el usuario quiere comprar nos estara enviando el texto para comprar en resultado
                if (response != resultado)
                {
                    ViewData["MENSAJE"] = "El texto es incorrecto";
                }
                else
                {
                    ViewData["MENSAJE"] = "El texto es correcto";
                }
            }
            return View();
        }
    }
}
