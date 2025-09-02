using GymLogger.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Text;

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
    public class CSVHandler
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public CSVHandler(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
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
        public string PrepareForUpload(ICSVFile cSVFile)
        {
            var csvReader = new CSVReader(new StreamReader(cSVFile.OpenReadStream()));
            try
            {

            }
            catch
            {

            }
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
        public string[]? ReadLine()
        {
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
