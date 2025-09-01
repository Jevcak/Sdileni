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

        public Session Session { get; set; } = null!;
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
                .ThenInclude(e => e.ExerciseMuscles)
                .ThenInclude(em => em.Muscle)
                .Where(es => es.SessionId == id)
                .ToListAsync();
            

            return Page();
        }
    }
}
