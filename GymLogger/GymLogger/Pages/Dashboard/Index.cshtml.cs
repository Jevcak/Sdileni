using GymLogger.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace GymLogger.Pages.Dashboard
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public string? NewExerciseName { get; set; }
        [BindProperty]
        public Session NewSession { get; set; } = new();

        [BindProperty]
        public ExerciseSession NewExerciseSession { get; set; } = new();
        [BindProperty]
        public int? SelectedSessionId { get; set; }

        public Session? LastSession { get; set; } = null;
        public SelectList Exercises { get; set; } = default!;
        public SelectList PredefinedSessionNames { get; set; } = default!;


        public async Task OnGetAsync()
        {
            Exercises = new SelectList(await _context.Exercises.ToListAsync(), "Id", "Name");
            PredefinedSessionNames = new SelectList(new List<string> { "Push Day", "Pull Day", "Leg Day" });
            var user = await _userManager.GetUserAsync(User);

            var now = DateTime.Now;
            LastSession = await _context.Sessions
                .Where(s => s.UserId == user.Id &&
                            s.Date >= now.AddHours(-3) &&
                            s.Date <= now.AddHours(3))
                .OrderByDescending(s => s.Date)
                .FirstOrDefaultAsync();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToPage("/Account/Login");

            // Pokud u�ivatel zadal vlastn� cvik, p�id�me ho do datab�ze
            if (!string.IsNullOrWhiteSpace(NewExerciseName))
            {
                var newExercise = new Exercise { Name = NewExerciseName };
                _context.Exercises.Add(newExercise);
                await _context.SaveChangesAsync();

                // p�i�ad�me nov� vytvo�en� cvik k ExerciseSession
                NewExerciseSession.ExerciseId = newExercise.Id;
            }

            // Pokud u�ivatel vybral p�ednastaven� n�zev tr�ninku, pou�ijeme ho
            if (!string.IsNullOrWhiteSpace(Request.Form["NewSession.Name"]))
            {
                NewSession.Name = Request.Form["NewSession.Name"].FirstOrDefault() ?? string.Empty;
            }

            // p�i�ad�me tr�nink k p�ihl�en�mu u�ivateli
            NewSession.UserId = user.Id;
            _context.Sessions.Add(NewSession);
            await _context.SaveChangesAsync();

            // p�i�ad�me prvn� cvik k tr�ninku
            NewExerciseSession.SessionId = NewSession.Id;
            _context.ExerciseSessions.Add(NewExerciseSession);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Sessions/Index");
        }
    }
}
