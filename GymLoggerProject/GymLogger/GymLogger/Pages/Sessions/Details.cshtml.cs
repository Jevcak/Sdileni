using GymLogger.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GymLogger.Pages.Sessions
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Session? Session { get; set; } = null;
        [BindProperty]
        public List<ExerciseSession> ExerciseSessions { get; set; } = new();


        public async Task<IActionResult> OnGetAsync(int id)
        {
            Session = await _context.Sessions
                .FirstOrDefaultAsync(s => s.Id == id);

            if (Session == null)
            {
                return NotFound();
            }

            ExerciseSessions = await _context.ExerciseSessions
                .Include(es => es.Exercise)
                .ThenInclude(e => e!.ExerciseMuscles)!
                .ThenInclude(em => em.Muscle)
                .Where(es => es.SessionId == id)
                .ToListAsync();
            

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var exerciseSessionsFromDb = await _context.ExerciseSessions
                .Where(es => es.SessionId == id)
                .ToListAsync();

            // Pøiøaï hodnoty z POSTu
            foreach (var esDb in exerciseSessionsFromDb)
            {
                var posted = ExerciseSessions.FirstOrDefault(p => p.Id == esDb.Id);
                if (posted != null)
                {
                    esDb.Weight = posted.Weight;
                    esDb.NofRepetitions = posted.NofRepetitions;
                    esDb.NofSets = posted.NofSets;
                    esDb.Note = posted.Note;
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToPage(new { id });
        }
        public async Task<IActionResult> OnPostDeleteSessionAsync(int id)
        {
            var session = await _context.Sessions
                .Include(s => s.ExerciseSessions)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (session == null) return NotFound();

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();

            // delete session and return to dashboard

            return RedirectToPage("/Sessions/Index");
        }
    }
}
