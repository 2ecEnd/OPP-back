using Microsoft.AspNetCore.Mvc;
using OPP_back.Services;

namespace OPP_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AuthService authService;

        public AuthController(AuthService authService)
        {
            this.authService = authService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
