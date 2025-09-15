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

    public interface ICSVFile : IFormFile
    {
    }
    public interface ICSVReader
    {
        public string[]? ReadLine();
    }
    public interface IDisposable
    {
        public void Dispose();
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
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly string userId;
        public CSVHandler(ApplicationDbContext context, UserManager<User> userManager, string userID)
        {
            _context = context;
            _userManager = userManager;
            userId = userID;            
        }

        //Here I will prepare the string, File will be created in OnGetDownloadCsvAsync
        private string columnNames = "SessionId,SessionName,ExerciseId,ExerciseName,Weight,Repetitions,Sets,DateTime,Note";
        public string PrepareForDownload(List<Session> sessions)
        {
            string csvContent = "";
            var csvLines = new List<string>();
            csvLines.Add(columnNames);
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
        public string PrepareForUpload(ICSVFile cSVFile, out (int,int) succesRate)
        {
            // store succes values and missing values,
            // so I can write summary at the end of upload
            int succes = 0;
            int fail = 0;
            var csvReader = new CSVReader(new StreamReader(cSVFile.OpenReadStream()));
            // try to get keys and values
            // feasible columns are:
            // SessionName, ExerciseName, Weight, Repetitions, Sets, DateTime, Note
            // it will always create new session
            Dictionary<string,int> keyOrder = new Dictionary<string,int>();
            Dictionary<string,SessionRow> sessions = new Dictionary<string,SessionRow>();
            try
            {
                string[] keys = csvReader.getKeys()!;
                // add function that matches the names more
                for (int i = 0; i < keys.Length; i++)
                {
                    keyOrder[keys[i]] = i;
                }
                string[]? vals = csvReader.getKeys();
                while (vals is not null)
                {
                    bool validDate = DateTime.TryParse(vals[keyOrder["DateTime"]], out DateTime date);
                    bool validWeight = Double.TryParse(vals[keyOrder["Weight"]], out double weight);
                    bool validSets = int.TryParse(vals[keyOrder["Sets"]], out int sets);
                    bool validReps = int.TryParse(vals[keyOrder["Repetitions"]],out int reps);
                    string exercise = vals[keyOrder["ExerciseName"]];
                    string name = vals[keyOrder["SessionName"]];
                    string? note = null;
                    if (keyOrder.Keys.Contains("Note"))
                    {
                        note = vals[keyOrder["Note"]];
                    }
                    bool[] bools = { validDate, validWeight, validSets, validReps };
                    if (bools.Contains(false)) 
                    {
                        fail += 1;
                    }
                    else
                    {
                        sessions.Add(name,new SessionRow(exercise,weight,reps,sets,date,note));
                        succes += 1;
                    }
                }
                foreach (string sessionName in sessions.Keys)
                {
                    var exercise = _context.Exercises.FirstOrDefaultAsync(e => e.Name == sessions[sessionName].Exercise);
                    // if exercise name is not in the database,
                    // then the exercise ant it's session shouldn't be added
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
                        UserId = userId
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
            catch
            {

            }
            succesRate = (succes, fail);
            return " ";
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
            return null;
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
