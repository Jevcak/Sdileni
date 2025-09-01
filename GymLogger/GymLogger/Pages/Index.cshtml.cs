using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GymLogger.Pages
{
    public class IndexModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;

        public IndexModel(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }
        public IActionResult OnGet()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToPage("/Sessions/Index");
            }

            return Page();
        }
    }
}
