using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkPermitSystem.Models.ViewModels;

namespace WorkPermitSystem.Controllers
{
    [Authorize]
    public class PermitController : Controller
    {
        public PermitController()
        {
        }

        public IActionResult Create(CreatePermitViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePermitViewModel model, IFormFile imageFile)
        {
            //if (ModelState.IsValid)
            //{
            //    var createAdvertModel = _mapper.Map<CreateAdvertModel>(model);
            //    createAdvertModel.UserName = User.Identity.Name;

            //    var apiCallResponse = await _advertApiClient.CreateAsync(createAdvertModel).ConfigureAwait(false);
            //    var id = apiCallResponse.Id;

            //    bool isOkToConfirmAd = true;
            //    string filePath = string.Empty;
            //    if (imageFile != null)
            //    {
            //        var fileName = !string.IsNullOrEmpty(imageFile.FileName) ? Path.GetFileName(imageFile.FileName) : id;
            //        filePath = $"{id}/{fileName}";

            //        try
            //        {
            //            using (var readStream = imageFile.OpenReadStream())
            //            {
            //                var result = await _fileUploader.UploadFileAsync(filePath, readStream)
            //                    .ConfigureAwait(false);
            //                if (!result)
            //                    throw new Exception(
            //                        "Could not upload the image to file repository. Please see the logs for details.");
            //            }
            //        }
            //        catch (Exception e)
            //        {
            //            isOkToConfirmAd = false;
            //            var confirmModel = new ConfirmAdvertRequest()
            //            {
            //                Id = id,
            //                FilePath = filePath,
            //                Status = AdvertStatus.Pending
            //            };
            //            await _advertApiClient.ConfirmAsync(confirmModel).ConfigureAwait(false);
            //            Console.WriteLine(e);
            //        }
            //    }

            //    if (isOkToConfirmAd)
            //    {
            //        var confirmModel = new ConfirmAdvertRequest()
            //        {
            //            Id = id,
            //            FilePath = filePath,
            //            Status = AdvertStatus.Active
            //        };
            //        await _advertApiClient.ConfirmAsync(confirmModel).ConfigureAwait(false);
            //    }

            //    return RedirectToAction("Index", "Home");
            //}

            return View(model);
        }
    }
}