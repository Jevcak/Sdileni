using GymLogger.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace GymLogger.Pages.User
{
    public class DetailsModel : PageModel
    {
        private readonly UserManager<GymLogger.User> _userManager;
        private readonly ApplicationDbContext _context;

        public DetailsModel(UserManager<GymLogger.User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        [BindProperty]
        public InputModel Input { get; set; } = new();
        public class InputModel
        {
            public string? UserName { get; set; }
            [EmailAddress]
            public string? Email { get; set; }

            public int? Height { get; set; }
            public int? Weight { get; set; }

            [DataType(DataType.Date)]
            public DateOnly? BirthDate { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            Input = new InputModel
            {
                UserName = user.FullName,
                Email = user.Email,
                Height = user.Height,
                Weight = user.Weight,
                BirthDate = user.BirthDate
            };

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            user.FullName = Input.UserName!;
            user.Height = Input.Height;
            user.Weight = Input.Weight;
            user.BirthDate = Input.BirthDate;

            await _userManager.UpdateAsync(user);
            return RedirectToPage();
        }
    }
}
