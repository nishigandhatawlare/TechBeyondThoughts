using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechBeyondThoughts.Web.Models;
using TechBeyondThoughts.Web.Service;

namespace TechBeyondThoughts.Web.Controllers
{
    public class TechController : Controller
    {
        private readonly ITechService _techService;

        public TechController(ITechService techService)
        {
            _techService = techService;
        }
       
        public async Task<IActionResult> TechIndex(int? page)
        {
            int pageSize = 3; // Set the desired page size

            ResponceDto? response = await _techService.GetAllTechAsync();

            if (response != null && response.IsSuccess)
            {
                var techList = JsonConvert.DeserializeObject<List<TechDataDto>>(Convert.ToString(response.Result));

                var paginatedList = PaginatedList<TechDataDto>.Create(techList, page ?? 1, pageSize);

                return View(paginatedList);
            }
            else
            {
                TempData["error"] = response?.Message;
                return View();
            }
        }
        /************************************/
        public async Task<IActionResult> techDetails(int id)
        {
            TechDataDto? model = new();
            ResponceDto? responce = await _techService.GetTechByIdAsync(id);

            if (responce != null && responce.IsSuccess)
            {
                model = JsonConvert.DeserializeObject<TechDataDto>(Convert.ToString(responce.Result));
            }
            else
            {
                TempData["error"] = responce?.Message;
            }
            return View(model);
        }
        public IActionResult Search(string Keyword)
        {
            List<TechDataDto> models = null;

            ResponceDto responce = _techService.SearchTechByNameAsync(Keyword).Result;

            if (responce != null && responce.IsSuccess)
            {
                models = JsonConvert.DeserializeObject<List<TechDataDto>>(Convert.ToString(responce.Result));
            }
            else
            {
                TempData["error"] = responce?.Message;
            }

            return View(models);
        }





        public async Task<IActionResult> CreateTech(TechDataDto model)
        {
            if (ModelState.IsValid) {

                ResponceDto? responce = await _techService.CreateTechAsync(model);

                if (responce != null && responce.IsSuccess)
                {
                    TempData["success"] = "Technology Created Successfully!";
					return RedirectToAction(nameof(TechIndex));
                }
                else
                {
                    TempData["error"] = responce?.Message;
                }
            }
            return View(model);
        }
       
        [HttpGet]
        public async Task<IActionResult> DeleteTech(int techId)
        {
            ResponceDto? responce = await _techService.GetTechByIdAsync(techId);

            if (responce != null && responce.IsSuccess)
            {
                TechDataDto? model = JsonConvert.DeserializeObject<TechDataDto>(Convert.ToString(responce.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = responce?.Message;
            }
            return NotFound();
        }
        [HttpPost, ActionName("DeleteTech")]
        public async Task<IActionResult> DeleteConfirmed(int techId)
        {
            try
            {
                ResponceDto? responce = await _techService.DeleteTechAsync(techId);

                if (responce != null && responce.IsSuccess)
                {
                    // Redirect to TechIndex upon successful deletion
                    return RedirectToAction(nameof(TechIndex));
                }
                else
                {
                    TempData["error"] = responce?.Message;
                }

                // If the deletion was not successful, redirect to a suitable action
                return RedirectToAction(nameof(TechIndex));
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes

                ModelState.AddModelError(string.Empty, "An error occurred while deleting the technology. Please try again.");
                // Redirect to a suitable action
                return RedirectToAction(nameof(TechIndex));
            }
        }



    }
}
