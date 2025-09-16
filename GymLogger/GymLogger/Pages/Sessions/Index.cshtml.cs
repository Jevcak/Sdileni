using GymLogger.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using ScottPlot;


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
        public Session? LastSession { get; set; }


        public async Task OnGetAsync()
        {
            // hodilo by se to napsat do samostatny knihovny
            Plot myPlot = new();

            double[] dataX = { 1, 2, 3, 4, 5 };
            double[] dataY = { 1, 4, 9, 16, 25 };

            myPlot.Add.Scatter(dataX, dataY);

            myPlot.SavePng("wwwroot/chart.png", 400, 300);


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
            var handler = new CSVHandler(_context,_userManager,userId!);
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
        public async Task<IActionResult> OnPostUploadCsvAsync(IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length == 0)
            {
                TempData["UploadError"] = "Please upload a CSV file.";
                return RedirectToPage();
            }
            var userId = _userManager.GetUserId(User);
            var csvHandler = new CSVHandler(_context,_userManager, userId!);
            string message = csvHandler.HandleUpload(csvFile);
            TempData["UploadError"] = message;
            //await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
