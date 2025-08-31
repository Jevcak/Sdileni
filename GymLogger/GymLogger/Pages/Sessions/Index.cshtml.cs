using GymLogger.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GymLogger.Pages.Sessions
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Session> Sessions { get; set; } = new List<Session>();

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null) return;

            Sessions = await _context.Sessions
                .Where(s => s.UserId == user.Id)
                .OrderByDescending(s => s.Date)
                .ToListAsync();
        }
    }
}
