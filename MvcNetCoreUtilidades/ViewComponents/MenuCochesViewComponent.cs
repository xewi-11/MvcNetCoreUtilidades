using Microsoft.AspNetCore.Mvc;
using MvcNetCoreUtilidades.Repositories;

namespace MvcNetCoreUtilidades.ViewComponents
{
    public class MenuCochesViewComponent : ViewComponent
    {
        private RepositoryCoches repo;

        public MenuCochesViewComponent()
        {
            this.repo = new RepositoryCoches();
        }
        //Podemos TENER TODOS LOS METOSOAS QUE QUERAMOS

        //PERO SI QUEREMOS DEVOOLVER DATOS TENE;OS QUE USAR EL METODO DE INVOKEASYNC

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cars = this.repo.GetAllCars();
            return View(cars);
        }
    }
}
