using GymLogger.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GymLogger.Pages.Sessions
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        [BindProperty]
        public ExerciseSession NewExerciseSession { get; set; } = new();
        public SelectList Exercises { get; set; } = default!;
        [BindProperty]
        public Session NewSession { get; set; } = new();

        public IndexModel(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Session> Sessions { get; set; } = new List<Session>();

        public async Task OnGetAsync()
        {
            Exercises = new SelectList(await _context.Exercises.ToListAsync(), "Id", "Name");
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                RedirectToPage("/Account/Login");
                return;
            }

            Sessions = await _context.Sessions
                .Where(s => s.UserId == user.Id)
                .Include(s => s.ExerciseSessions)
                .OrderByDescending(s => s.Date)
                .ToListAsync();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToPage("/Account/Login");

            if (string.IsNullOrWhiteSpace(NewSession.Name))
            {
                ModelState.AddModelError("NewSession.Name", "You must enter a name for the new session.");
                return Page();
            }
            // kdyz existuje nedavna session tak pridavam cviky do ni
            var recentSession = await _context.Sessions
                .Where(s => s.UserId == user.Id && s.Date >= DateTime.Now.AddHours(-4))
                .OrderByDescending(s => s.Date)
                .FirstOrDefaultAsync();

            if (recentSession != null)
            {
                NewExerciseSession.SessionId = recentSession.Id;
            }
            else
            {
                NewSession.UserId = user.Id;
                _context.Sessions.Add(NewSession);
                await _context.SaveChangesAsync();

                NewExerciseSession.SessionId = NewSession.Id;
            }
            _context.ExerciseSessions.Add(NewExerciseSession);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Sessions/Index");
        }
    }
}
