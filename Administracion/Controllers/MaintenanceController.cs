using Microsoft.AspNetCore.Mvc;
using Administracion.Data;
using Administracion.Models;

namespace Administracion.Controllers
{
    public class MaintenanceController : Controller
    {

        EmployeeInfo _employeeInfo = new EmployeeInfo();
        public IActionResult List()
        {
            // Mostraremos los registros de la base de datos en una vista 
            var ObjList = _employeeInfo.Shown();
            return View(ObjList);
        }

        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save(EmpleadoModelo emReq)
        {
            if(!ModelState.IsValid)
                return View();

            var response = _employeeInfo.SaveData(emReq);
            if (response)
            {
                return RedirectToAction("List");
            }
            else {
                return View();
            }
        }

        public IActionResult Update(int IdEmployee)
        {
            var ObjEmployee = _employeeInfo.GetDataInfo(IdEmployee);
            return View(ObjEmployee);
        }

        [HttpPost]
        public IActionResult Update(EmpleadoModelo emReq)
        {
            if (!ModelState.IsValid)
                return View();

            var response = _employeeInfo.UpdateData(emReq);
            if (response)
                return RedirectToAction("List");
            else
                return View();
        }

        public IActionResult Delete(int IdEmployee)
        {
            var ObjEmployee = _employeeInfo.GetDataInfo(IdEmployee);
            return View(ObjEmployee);
        }

        [HttpPost]
        public IActionResult Delete(EmpleadoModelo emReq)
        {
            if (!ModelState.IsValid)
                return View();

            var response = _employeeInfo.DeleteData(emReq);
            if (response)
                return RedirectToAction("List");
            else
                return View();
        }

        // --------------------------- Control de Nóminas ----------------------------
        public IActionResult ListNomina(int IdEmployee)
        {
            var ObjEmployee = _employeeInfo.GetDataInfo(IdEmployee);
            return View(ObjEmployee);
        }

        [HttpPost]
        public IActionResult ListNomina(EmpleadoModelo emReq)
        {
            if (!ModelState.IsValid)
                return View();

            var response = _employeeInfo.UpdateData(emReq);
            if (response)
                return RedirectToAction("List");
            else
                return View();
        }
    }
}
