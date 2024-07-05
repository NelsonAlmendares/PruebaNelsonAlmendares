using Microsoft.AspNetCore.Mvc;

namespace Administracion.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
