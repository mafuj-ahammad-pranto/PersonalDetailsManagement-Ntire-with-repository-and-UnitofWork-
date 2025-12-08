using BusinessLogicLayer.IService;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PersonalDetailsController : Controller
    {
        private readonly IPersolaDetails _personalDetailsService;
        public PersonalDetailsController(IPersolaDetails personalDetailsService)
        {
            _personalDetailsService = personalDetailsService;
        }
        public async Task<IActionResult> Index()
        {
            var personalDetails = await _personalDetailsService.GetAllPersonalDetailsAsync();
            return View(personalDetails);
        }


    }
}
