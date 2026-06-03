using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Authorize]  // Only logged-in users can access the chatbot
    public class LawChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
