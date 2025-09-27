using GymLogger.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScottPlot;
using ScottPlot.Colormaps;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


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

        public List<Session> Sessions { get; set; } = new List<Session>();
        public List<ExerciseWithDaysViewModel> ExercisesWithDays { get; set; } = new List<ExerciseWithDaysViewModel>();
        public List<PersonalRecordsViewModel> PersonalRecords { get; set; } = new List<PersonalRecordsViewModel>();
        public Session? LastSession { get; set; }

        public class PersonalRecordsViewModel
        {
            public string ExerciseName { get; set; } = "";
            public double RecordWeight { get; set; }
            public int RecordReps { get; set; }
        }

        public class ExerciseWithDaysViewModel
        {
            public string ExerciseName { get; set; } = "";
            public int DaysSince { get; set; }
        }

        public class MuscleGroupDaysViewModel
        {
            public string MuscleGroup { get; set; } = "";
            public DateTime SessionDate { get; set; }
            public int TotalReps { get; set; }
        }
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
                .Include(s => s.ExerciseSessions)!
                .ThenInclude(es => es.Exercise)
                .OrderByDescending(s => s.Date)
                .ToListAsync();

            LastSession = Sessions.FirstOrDefault();

            if (LastSession?.ExerciseSessions != null)
            {
                foreach (var es in LastSession.ExerciseSessions)
                {
                    var lastDate = Sessions
                        .Where(s => s.Date < LastSession.Date)
                        .SelectMany(s => s.ExerciseSessions!)
                        .Where(e => e.ExerciseId == es.ExerciseId)
                        .OrderByDescending(e => e.Session!.Date)
                        .Select(e => e.Session!.Date)
                        .FirstOrDefault();

                    int daysSince = lastDate != default
                        ? (LastSession.Date - lastDate).Days
                        : -1;

                    ExercisesWithDays.Add(new ExerciseWithDaysViewModel
                    {
                        ExerciseName = es.Exercise?.Name ?? "",
                        DaysSince = daysSince
                    });
                }
                PersonalRecords = Sessions
                .SelectMany(s => s.ExerciseSessions ?? new List<ExerciseSession>())
                .Where(es => es.Exercise != null)
                .GroupBy(es => es.Exercise!.Name)
                .Select(g => new PersonalRecordsViewModel
                {
                    ExerciseName = g.Key,
                    RecordWeight = g.Max(es => es.Weight),
                    RecordReps = g.OrderByDescending(es => es.Weight).First().NofRepetitions
                })
                .ToList();
            }

            Exercises = new SelectList(await _context.Exercises.ToListAsync(), "Id", "Name");

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
        public async Task<IActionResult> OnGetPrimaryAsync()
        {
            var userId = _userManager.GetUserId(User);
            var sessions = await _context.Sessions
                .Where(s => s.UserId == userId)
                .Include(s => s.ExerciseSessions)!
                    .ThenInclude(es => es.Exercise)
                .ToListAsync();

            GraphPlotter plotter = new GraphPlotter();
            var plot = plotter.PreparePlot1(sessions);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "chart.png");
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            plot.SavePng("wwwroot/chart.png", 800, 400);

            return RedirectToPage();
        }
        public async Task<IActionResult> OnGetSecondaryAsync()
        {
            var userId = _userManager.GetUserId(User);
            var sessions = await _context.Sessions
                .Where(s => s.UserId == userId)
                .Include(s => s.ExerciseSessions)!
                    .ThenInclude(es => es.Exercise)
                .ToListAsync();

            GraphPlotter plotter = new GraphPlotter();
            var plot = plotter.PreparePlot2(sessions);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "chart.png");
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            plot.SavePng("wwwroot/chart.png", 800, 400);

            return RedirectToPage();
        }
        public async Task<IActionResult> OnGetTerciaryAsync()
        {
            var userId = _userManager.GetUserId(User);
            var sessions = await _context.Sessions
                .Where(s => s.UserId == userId)
                .Include(s => s.ExerciseSessions)!
                    .ThenInclude(es => es.Exercise)
                .ToListAsync();

            GraphPlotter plotter = new GraphPlotter();
            var plot = plotter.PreparePlot3(sessions);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "chart.png");
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            plot.SavePng("wwwroot/chart.png", 800, 400);

            return RedirectToPage();
        }
        public async Task<IActionResult> OnGetQuartalAsync()
        {
            var muscleGroups = new Dictionary<string, string>
            {
                { "Hamstrings", "Legs" },
                { "Quadriceps", "Legs" },
                { "Glutes", "Legs" },
                { "Calves", "Legs" },
                { "Chest", "Push" },
                { "Back", "Pull" },
                { "Shoulders", "Push" },
                { "Biceps", "Pull" },
                { "Triceps", "Push" },
                { "Forearms", "Pull" },
                { "Core", "Legs" },
                { "Trapezius", "Pull" },
                { "Adductors", "Legs" }
            };
            var userId = _userManager.GetUserId(User);
            var sessions = await _context.Sessions
                .Where(s => s.UserId == userId)
                .Include(s => s.ExerciseSessions)!
                    .ThenInclude(es => es.Exercise)
                .ToListAsync();
            var joinedTable = (
                from s in _context.Sessions
                join es in _context.ExerciseSessions on s.Id equals es.SessionId
                join e in _context.Exercises on es.ExerciseId equals e.Id
                join em in _context.ExerciseMuscles on es.ExerciseId equals em.ExerciseId
                join m in _context.Muscles on em.MuscleId equals m.Id
                where m.Importance > 4
                select new
                {
                    SessionName = s.Name,
                    SessionId = s.Id,
                    ExerciseName = e.Name,
                    SessionDate = s.Date,
                    MuscleName = m.Name,
                    MuscleImportance = m.Importance,
                    OverallReps = es.NofRepetitions * es.NofSets
                }
            )
            .AsEnumerable() // needs to be here, because using
                            // the dictionary causes an error
            .Select(x => new
            {
                x.SessionName,
                x.SessionId,
                x.ExerciseName,
                x.SessionDate,
                x.MuscleName,
                MuscleGroup = muscleGroups[x.MuscleName],
                x.MuscleImportance,
                x.OverallReps
            });
            var grouped = joinedTable
                .GroupBy(x => new { x.SessionId, x.MuscleGroup })
                .Select(g => new MuscleGroupDaysViewModel
                {
                    MuscleGroup = g.Key.MuscleGroup,
                    SessionDate = g.First().SessionDate,
                    TotalReps = g.Sum(x => x.OverallReps)
                })
                .OrderBy(r => r.MuscleGroup)
                .ThenBy(r => r.SessionDate);
            var groupedByMuscleGroup = grouped
                .GroupBy(g => g.MuscleGroup)
                .ToDictionary(
                    g => g.Key,
                    g => g.ToList()
                );
            GraphPlotter plotter = new GraphPlotter();
            var plot = plotter.PreparePlot4(groupedByMuscleGroup);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "chart.png");
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            plot.SavePng("wwwroot/chart.png", 800, 400);

            return RedirectToPage();
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
            var csvReader = new CSVReader(new StreamReader(csvFile.OpenReadStream()));
            string message = csvHandler.PrepareForUpload(csvReader, out int success, out int fail);
            //string message = csvHandler.HandleUpload(csvFile);
            foreach (string sessionName in csvHandler.sessions.Keys)
            {
                // always create new session from csv,
                // don't try to match it with existing one
                Session session = new Session
                {
                    Name = sessionName,
                    Date = csvHandler.sessions[sessionName][0].Date,
                    UserId = userId!
                };
                _context!.Sessions.Add(session);
                await _context.SaveChangesAsync();
                foreach (SessionRow row in csvHandler.sessions[sessionName])
                {
                    var exercise = await _context!.Exercises.FirstOrDefaultAsync(e => e.Name == row.Exercise);
                    // if exercise name is not in the database,
                    // then the exercise and it's session shouldn't be added
                    // we try to keep the exercise table clean
                    if (exercise == null)
                    {
                        fail += 1;
                        //throw new Exception("exercise was not found");
                        continue;
                    }

                    var exerciseSession = new ExerciseSession
                    {
                        SessionId = session.Id,
                        ExerciseId = exercise.Id,
                        Weight = row.Weight,
                        NofRepetitions = row.Repetitions,
                        NofSets = row.Sets,
                        Note = row.Note,
                    };

                    _context.ExerciseSessions.Add(exerciseSession);
                    await _context.SaveChangesAsync();
                }
            }
            if (success == 0)
            {
                TempData["UploadError"] = "Upload failed";
            }
            else
            {
                TempData["UploadSuccess"] = string.Format("Upload successful with success rate {0}/{1}", success, fail);
            }
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
