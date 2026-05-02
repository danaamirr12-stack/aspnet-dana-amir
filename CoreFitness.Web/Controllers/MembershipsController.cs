using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Web.Controllers
{
    public class MembershipsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}