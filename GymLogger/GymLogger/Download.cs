using GymLogger.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Globalization;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GymLogger
{

    public interface ICSVReader
    {
        public string[]? ReadLine();
    }
    public class DateTimeException : Exception
    {
        public DateTimeException()
        {
        }
    }
    public struct SessionRow
    {
        public SessionRow(string exercise, double weight, int reps, int sets, DateTime date, string? note)
        {
            Exercise = exercise;
            Weight = weight;
            Repetitions = reps;
            Sets = sets;
            Date = date;
            Note = note;            
        }
        public string Exercise { get; set; }
        public double Weight { get; set; }
        public int Repetitions { get; set; }
        public int Sets { get; set; }
        public DateTime Date { get; set; }
        public string? Note { get; set; }
    }
    public class CSVHandler
    {
        private readonly ApplicationDbContext? _context;
        private readonly UserManager<User>? _userManager;
        private readonly string? userId;
        public Dictionary<string, SessionRow> sessions = new Dictionary<string, SessionRow>();
        public CSVHandler() { }
        public CSVHandler(ApplicationDbContext context, UserManager<User> userManager, string userID)
        {
            _context = context;
            _userManager = userManager;
            userId = userID;            
        }

        private string[] columnNames = 
            ["SessionId","SessionName",
            "ExerciseId","ExerciseName",
            "Weight","Repetitions","Sets",
            "DateTime","Note"];
        //Here I will prepare the string, File will be created in OnGetDownloadCsvAsync
        public string PrepareForDownload(List<Session> sessions)
        {
            string csvContent = "";
            var csvLines = new List<string>();
            csvLines.Add(string.Join(',', columnNames));
            foreach (var session in sessions)
            {
                foreach (var es in session.ExerciseSessions!)
                {
                    csvLines.Add($"{es.SessionId},{es.Session!.Name},{es.ExerciseId},{es.Exercise?.Name},{es.Weight},{es.NofRepetitions},{es.NofSets},{es.DateTime},{es.Note}");
                }
            }
            csvContent += string.Join(Environment.NewLine, csvLines);
            return csvContent;
        }
        // PrepareForUpload is different, it would be better
        // if it uploaded straight into the database
        public string HandleUpload(IFormFile file, bool test = false)
        {
            var csvReader = new CSVReader(new StreamReader(file.OpenReadStream()));
            string message = PrepareForUpload(csvReader, out int success, out int fail);
            if (!test)
            {
                // make it's own function so I can properly test
                // csv reading and overall function
                Upload(ref fail);
            }
            return message;
        }
        public string PrepareForUpload(CSVReader csvReader, out int success, out int fail)
        {
            // store succes values and missing values,
            // so I can write summary at the end of upload
            success = 0;
            fail = 0;
            // try to get keys and values
            // feasible columns are:
            // SessionName, ExerciseName, Weight, Repetitions, Sets, DateTime, Note
            // it will always create new session
            Dictionary<string,int> keyOrder = new Dictionary<string,int>();
            try
            {
                string[] keys = csvReader.getKeys()!;
                // add function that matches the names more
                for (int i = 0; i < keys.Length; i++)
                {
                    string mappedName = MapName(keys[i]);
                    keyOrder[mappedName] = i;
                }
                string[]? vals = csvReader.ReadLine();
                while (vals is not null)
                {
                    bool validDate = DateTime.TryParse(vals[keyOrder[columnNames[7]]], out DateTime date);
                    bool validWeight = double.TryParse(vals[keyOrder[columnNames[4]]], out double weight);
                    bool validSets = int.TryParse(vals[keyOrder[columnNames[6]]], out int sets);
                    bool validReps = int.TryParse(vals[keyOrder[columnNames[5]]], out int reps);
                    string exercise = vals[keyOrder[columnNames[3]]];
                    string name = vals[keyOrder[columnNames[1]]];
                    string? note = null;
                    // Note is not required
                    if (keyOrder.Keys.Contains(columnNames[8]))
                    {
                        note = vals[keyOrder[columnNames[8]]];
                    }
                    bool[] bools = { validDate, validWeight, validSets, validReps };
                    if (bools.Contains(false))
                    {
                        fail += 1;
                    }
                    else
                    {
                        if (sessions.ContainsKey(name))
                        {
                            sessions[name] = new SessionRow(exercise, weight, reps, sets, date, note);
                        }
                        else
                        {
                            sessions.Add(name, new SessionRow(exercise, weight, reps, sets, date, note));
                        }
                        success += 1;
                    }
                    vals = csvReader.ReadLine();
                }
            }
            catch (Exception ex)
            {
                return $"Upload failed: {ex.Message}";
            }
            csvReader.Dispose();
            return string.Format("Upload successful with success rate {0}/{1}", success, fail);

        }
        public string MapName(string name)
        {
            // should also check if the column is already
            // in Dict sessions,so storing 2 values
            // in one row doesn't happen
            string actual;
            if (name.ToLower().Contains("exercise"))
            {
                // columnNames[3] is "ExerciseName"
                actual = columnNames[3];
            }
            else if (name.ToLower().Contains("weight"))
            {
                // columnNames[4] is "Weight"
                actual = columnNames[4];
            }
            else if (name.ToLower().Contains("rep"))
            {
                // columnNames[5] is "Repetitions"
                actual = columnNames[5];
            }
            else if (name.ToLower().Contains("set"))
            {
                // columnNames[6] is "Sets"
                actual = columnNames[6];
            }
            else if (name.ToLower().Contains("date") || name.ToLower().Contains("time"))
            {
                // columnNames[7] is "DateTime"
                actual = columnNames[7];
            }
            else if (name.ToLower().Contains("note"))
            {
                // columnNames[8] is "Note"
                actual = columnNames[8];
            }
            // should be last because exercise column could have name in it
            else if (name.ToLower().Contains("name"))
            {
                // columnNames[1] is "SessionName"
                actual = columnNames[1];
            }
            else
            {
                actual = "invalidColumnName";
            }
            if (sessions.ContainsKey(actual)) 
            {
                actual = "alreadyMappedColumn";
            }
            return actual;
        }
        public void Upload(ref int fail)
        {
            foreach (string sessionName in sessions.Keys)
            {
                var exercise = _context!.Exercises.FirstOrDefaultAsync(e => e.Name == sessions[sessionName].Exercise);
                // if exercise name is not in the database,
                // then the exercise and it's session shouldn't be added
                // we try to keep the exercise table clean
                if (exercise == null)
                {
                    fail += 1;
                    continue;
                }
                // always create new session from csv,
                // don't try to match it with existing one
                Session session = new Session
                {
                    Name = sessionName,
                    Date = sessions[sessionName].Date,
                    UserId = userId!
                };
                _context.Sessions.Add(session);
                _context.SaveChangesAsync();

                var exerciseSession = new ExerciseSession
                {
                    SessionId = session.Id,
                    ExerciseId = exercise.Id,
                    Weight = sessions[sessionName].Weight,
                    NofRepetitions = sessions[sessionName].Repetitions,
                    NofSets = sessions[sessionName].Sets,
                    Note = sessions[sessionName].Note,
                };

                _context.ExerciseSessions.Add(exerciseSession);
                _context.SaveChangesAsync();
            }
        }
    }
    public class CSVReader : ICSVReader, IDisposable
    {
        public TextReader _reader;
        public CSVReader(TextReader reader) 
        {
            _reader = reader;
        }
        public string[]? getKeys()
        {
            return ReadLine();
        }
        public string[]? ReadLine()
        {
            // returns line in array
            var sb = new StringBuilder();
            List<string> line = new List<string>();
            int ch;
            while ((ch = _reader.Read()) != -1)
            {
                if (ch == '\n')
                {
                    break;
                }
                else if (ch != '\r')
                {
                    if (ch == ',')
                    {
                        line.Add(sb.ToString());
                        sb.Clear();
                    }
                    else
                    {
                        sb.Append((char)ch);
                    }
                }
            }
            line.Add(sb.ToString());
            return line.ToArray();
        }
        public void Dispose() 
        {
            _reader.Dispose();
        }
        
    }
}
