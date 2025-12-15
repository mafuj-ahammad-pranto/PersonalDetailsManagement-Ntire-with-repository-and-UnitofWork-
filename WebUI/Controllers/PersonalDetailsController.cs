using BusinessLogicLayer.IService;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

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
        [HttpGet]
        public IActionResult Create()
        {
            var model = new PersonalDetailCreateVM();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonalDetailCreateVM model)
        {
            // Remove ModelState errors for foreign keys and navigation properties
            var keysToRemove = ModelState.Keys
                .Where(k => k.Contains("PersonalDetailId") || k.Contains("PersonalDetail"))
                .ToList();

            foreach (var key in keysToRemove)
            {
                ModelState.Remove(key);
            }

            // Validate that we have at least the required data
            if (model.PersonalDetail == null)
            {
                ModelState.AddModelError("", "Personal details are required");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Ensure lists are not null
            if (model.AcademicQualifications == null)
                model.AcademicQualifications = new List<AcademicQualification>();

            if (model.JobExperiences == null)
                model.JobExperiences = new List<JobExperience>();

            try
            {
                await _personalDetailsService.AddAllAsync(
                    model.PersonalDetail,
                    model.AcademicQualifications,
                    model.JobExperiences);

                TempData["Success"] = "Personal details created successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while saving: " + ex.Message);
                return View(model);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var personalDetail = await _personalDetailsService.GetByIdWithRelatedDataAsync(id);

            if (personalDetail == null)
            {
                return NotFound();
            }

            return View(personalDetail);
        }




    }
}
