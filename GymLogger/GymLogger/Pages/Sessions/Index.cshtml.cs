using GymLogger.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace GymLogger.Pages.Sessions
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<GymLogger.User> _userManager;
        [BindProperty]
        public ExerciseSession NewExerciseSession { get; set; } = new();
        public SelectList Exercises { get; set; } = default!;
        [BindProperty]
        public Session NewSession { get; set; } = new();

        public IndexModel(ApplicationDbContext context, UserManager<GymLogger.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Session> Sessions { get; set; } = new List<Session>();
        public Session? LastSession { get; set; }


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

            LastSession = await _context.Sessions
                .Where(s => s.UserId == user.Id)
                .Include(s => s.ExerciseSessions)!
                    .ThenInclude(es => es.Exercise)
                .OrderByDescending(s => s.Date)
                .FirstOrDefaultAsync();


        }
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToPage("/Account/Login");

            // kdyz existuje nedavna session tak pridavam cviky do ni
            var recentSession = await _context.Sessions
                .Where(s => s.UserId == user.Id && s.Date >= DateTime.Now.AddHours(-4))
                .OrderByDescending(s => s.Date)
                .FirstOrDefaultAsync();

            if (recentSession != null)
            {
                NewExerciseSession.SessionId = recentSession.Id;
            }
            else if (string.IsNullOrWhiteSpace(NewSession.Name))
            {
                ModelState.AddModelError("NewSession.Name", "You must enter a name for the new session.");
                return Page();
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
        public async Task<IActionResult>? OnGetDownloadCsvAsync()
        {
            var userId = _userManager.GetUserId(User);
            var handler = new CSVHandler();
            var sessions = await _context.Sessions
                .Where(s => s.UserId == userId)
                .Include(s => s.ExerciseSessions)!
                    .ThenInclude(es => es.Exercise)
                .ToListAsync();
            if (sessions == null)
                return NotFound();
            string csvContent = handler.PrepareForDownload(sessions);
            var bytes = System.Text.Encoding.UTF8.GetBytes(csvContent);
            return File(bytes, "text/csv", $"sessions_{DateTime.Now.ToShortDateString()}.csv");
        }
        [BindProperty]
        public IFormFile CsvFile { get; set; } = default!;
        public async Task<IActionResult> OnPostUploadCsvAsync(IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length == 0)
            {
                TempData["UploadError"] = "Please upload a CSV file.";
                return RedirectToPage();
            }

            using (var reader = new StreamReader(csvFile.OpenReadStream()))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    var values = line.Split(',');

                    // Pøíklad: oèekáváš CSV ve formátu: SessionName, Date, ExerciseName, Weight, Reps, Sets
                    var sessionName = values[0];
                    var date = DateTime.ParseExact(values[1], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    var exerciseName = values[2];
                    var weight = double.Parse(values[3]);
                    var reps = int.Parse(values[4]);
                    var sets = int.Parse(values[5]);

                    // Najdi nebo vytvoø session
                    var session = await _context.Sessions
                        .FirstOrDefaultAsync(s => s.Name == sessionName);

                    if (session == null)
                    {
                        session = new Session
                        {
                            Name = sessionName,
                            Date = date,
                            UserId = "TODO: add current user id"
                        };
                        _context.Sessions.Add(session);
                        await _context.SaveChangesAsync();
                    }

                    // Najdi exercise
                    var exercise = await _context.Exercises.FirstOrDefaultAsync(e => e.Name == exerciseName);
                    if (exercise == null)
                    {
                        exercise = new Exercise { Name = exerciseName };
                        _context.Exercises.Add(exercise);
                        await _context.SaveChangesAsync();
                    }

                    // Pøidej exercise session
                    var exerciseSession = new ExerciseSession
                    {
                        SessionId = session.Id,
                        ExerciseId = exercise.Id,
                        Weight = weight,
                        NofRepetitions = reps,
                        NofSets = sets
                    };

                    _context.ExerciseSessions.Add(exerciseSession);
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
