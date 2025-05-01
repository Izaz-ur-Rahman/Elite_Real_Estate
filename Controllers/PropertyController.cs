using EliteRealEstate.Services;
using Microsoft.AspNetCore.Mvc;

namespace EliteRealEstate.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IApiService _apiService;
        public PropertyController(IApiService apiService)
        {
             _apiService = apiService;
        }

        public async Task<IActionResult> PropertyDetails(int id)
        {
            var property = await _apiService.GetPropertyById(id);
            return View(property);
        }



    }
}
