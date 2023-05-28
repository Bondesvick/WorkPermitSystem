using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace WorkPermitSystem.Areas.Aprover.Controllers
{
    [Authorize(Roles = "Aprover")]
    [Area("Aprover")]
    public class PermitController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}