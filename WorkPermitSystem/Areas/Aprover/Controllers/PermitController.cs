using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkPermitSystem.Services;

namespace WorkPermitSystem.Areas.Aprover.Controllers
{
    [Authorize(Roles = "Aprover")]
    [Area("Aprover")]
    public class PermitController : Controller
    {
        private readonly IFileUploader _fileUploader;

        public PermitController(IFileUploader fileUploader)
        {
            _fileUploader = fileUploader;
        }

        public async Task<IActionResult> Index()
        {
            var document = await _fileUploader.GetFileByKeyAsync("");

            var file = File(document, "application/octet-stream", "");

            return file;
        }
    }
}