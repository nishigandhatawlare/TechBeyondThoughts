using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
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
        public async Task<IActionResult> TechIndex()
        {
            List<TechDataDto>? list = new();
            ResponceDto? responce = await _techService.GetAllTechAsync();

            if (responce != null && responce.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<TechDataDto>>(Convert.ToString(responce.Result));

            }
            else {
                TempData["error"] = responce?.Message;
            }
            return View(list);
        }

       /* public async Task<IActionResult> CreateTech()
        {
            
            return View();
        }*/

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
        [HttpPost]
        public async Task<IActionResult> DeleteTech(TechDataDto techDataDto)
        {
            ResponceDto? responce = await _techService.DeleteTechAsync(techDataDto.Id);
            if (responce != null && responce.IsSuccess)
            {
                return RedirectToAction(nameof(TechIndex));
            }
            else
            {
                TempData["error"] = responce?.Message;
            }
            return View(techDataDto);
        }
    }
}
