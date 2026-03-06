using Microsoft.AspNetCore.Mvc;
using MvcNetCoreUtilidades.Models;
using MvcNetCoreUtilidades.Repositories;

namespace MvcNetCoreUtilidades.Controllers
{
    public class CochesController : Controller
    {
        private List<Coche> Cars;
        private RepositoryCoches repo;
        public CochesController()
        {
            this.repo = new RepositoryCoches();
            this.Cars = repo.GetAllCars();
        }       

         
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult _CochesPartial()
        {
            return PartialView("_CochesPartial", this.Cars);
        }


        public IActionResult _CocheDetails(int idcoche)
        {
            Coche car= this.repo.GetCarById(idcoche);
            return PartialView("_CocheDetailsView", car);
        }

        public IActionResult Details(int idcoche)
        {
            Coche car = this.repo.GetCarById(idcoche);
            return View(car);
        }

        }
}
