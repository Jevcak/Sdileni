using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ScottPlot;

namespace GymLogger.Pages
{
    public class IndexModel : PageModel
    {
        private readonly SignInManager<GymLogger.User> _signInManager;

        public IndexModel(SignInManager<GymLogger.User> signInManager)
        {
            _signInManager = signInManager;
        }
        public IActionResult OnGet()
        {
            // if user is signed in, don't show this site
            if (_signInManager.IsSignedIn(User))
            {
                GraphPlotter plotter = new GraphPlotter();
                Plot plot = plotter.PreparePlot0();
                plot.SavePng("wwwroot/chart.png", 800, 400);
                return RedirectToPage("/Sessions/Index");
            }

            return Page();
        }
    }
}
