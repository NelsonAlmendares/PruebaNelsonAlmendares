using Administracion.Data;
using Administracion.Models;
using Microsoft.AspNetCore.Mvc;

namespace Administracion.Controllers
{
    public class NominaController : Controller
    {
        /*
         * NOTA: Se podría desarrollar una función que tome como una matriz de arreglo matemático 
         * para poder calcular los días que se trabajan de un mes con un librería de JQuery, por tiempo se asume
         * que serán 25 días de pago al mes, con el salario asignado desde el contrato, restando los días no 
         * trabajados desde la consulta de SQL SERVER por medio de un procedimiento almancenado
         */

        Nomina _nominaData = new Nomina();

        public IActionResult ListNomina()
        {
            var ObjeList = _nominaData.Shown();
            return View(ObjeList);
        }

        // Cargar datos en un SELECT de Bootstrap
        public IActionResult ListEmployee()
        {
            var ObjeData = _nominaData.ShownEmployees();
            return View(ObjeData);
        }

        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save(NominaModelo nomReq)
        {
            if (!ModelState.IsValid)
                return View();

            var response = _nominaData.Save(nomReq);
            if (response)
            {
                return RedirectToAction("ListNomina");
            }
            else
            {
                return View();
            }
        }
    }
}
