using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using ScottPlot;
using ScottPlot.TickGenerators;

namespace GymLogger
{
    public class GraphPlotter
    {
        // null graph to show before any graph is chosen
        public Plot PreparePlot0()
        {
            var plot = new Plot();
            var text = plot.Add.Text("Choose data to display", 25, 0.5);
            text.LabelFontSize = 26;
            // when bold it doesn't quite match the buttons
            //text.LabelBold = true;
            text.LabelFontColor = Colors.White;
            // blue color that should match the buttons
            text.LabelBackgroundColor = Color.FromHex("#0D6EFD");
            text.LabelBorderWidth = 3;
            text.LabelPadding = 10;
            text.LabelAlignment = Alignment.MiddleCenter;
            return plot;
        }
        // graph that shows overall weight lifted on sessions in time
        public Plot PreparePlot1(List<Session> sessions)
        {
            Plot plot = new Plot();

            var sorted = sessions
                .OrderBy(s => s.Date)
                .Select(s => (s.Date, s.TotalWeightLifted))
                .ToList();

            double[] xs = sorted.Select(v => v.Date.ToOADate()).ToArray(); 
            double[] ys = sorted.Select(v => v.TotalWeightLifted).ToArray();
            // xs is sorted, can use CleanseArray
            CleanseArray(xs,ys, out double[] xset, out double[] ylist);
            var sp1 = plot.Add.Scatter(xset, ylist);
            sp1.LegendText = "Total Weight Lifted";
            sp1.LineWidth = 3;
            sp1.MarkerSize = 10;
            sp1.Smooth = true;
            sp1.Color = Colors.Green;

            plot.Axes.DateTimeTicksBottom();
            plot.Legend.IsVisible = true;
            plot.Title("Progress of Total Weight Lifted");
            plot.YLabel("Total Volume (kg)");
            plot.XLabel("Date");
            plot.ShowLegend();

            return plot;
        }
        // graph to showcase three exercises over time
        // squat, bench press, deadlift
        public Plot PreparePlot2(List<Session> sessions)
        {
            Plot plot = new Plot();

            string[] targetExercises = { "Squat", "Bench Press", "Deadlift" };

            var exerciseData = targetExercises.ToDictionary(
                e => e,
                e => new List<(DateTime, double)>()
            );

            foreach (var session in sessions)
            {
                foreach (string exerciseName in targetExercises)
                {
                    double totalForExercise = session.ExerciseSessions?
                        .Where(es => es.Exercise != null && es.Exercise.Name == exerciseName)
                        .Sum(es => es.Weight * es.NofRepetitions * es.NofSets) ?? 0;

                    if (totalForExercise > 0)
                        exerciseData[exerciseName].Add((session.Date, totalForExercise));
                }
            }

            foreach (var kvp in exerciseData)
            {
                if (kvp.Value.Count == 0) continue;

                var sorted = kvp.Value.OrderBy(v => v.Item1).ToList();
                double[] xs = sorted.Select(v => v.Item1.ToOADate()).ToArray();
                double[] ys = sorted.Select(v => v.Item2).ToArray();
                // xs is sorted, can Cleanse with my function
                int _ = CleanseArray(xs, ys, out double[] xset, out double[] ylist);
                var sp1 = plot.Add.Scatter(xset, ylist);
                sp1.LegendText = kvp.Key;
                sp1.LineWidth = 3;
                sp1.MarkerSize = 10;
                sp1.Smooth = true;
            }

            plot.Axes.DateTimeTicksBottom();
            plot.Legend.IsVisible = true;
            plot.Title("Progress of Volume Lifted");
            plot.YLabel("Total Volume (kg)");
            plot.XLabel("Date");
            plot.ShowLegend();

            return plot;
        }

        // graph showing session count over time
        public Plot PreparePlot3(List<Session> sessions)
        {
            var myPlot = new Plot();

            var grouped = sessions
                .GroupBy(s => new { s.Date.Year, s.Date.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .ToList();

            if (grouped.Count == 0)
                return myPlot;

            for (int i = 0; i < grouped.Count; i++)
            {
                myPlot.Add.Bar(position: i + 1, value: grouped[i].Count);
            }

            Tick[] ticks = grouped
                .Select((g, i) => new Tick(i + 1, $"{g.Month:D2}.{g.Year}"))
                .ToArray();

            myPlot.Axes.Bottom.TickGenerator = new NumericManual(ticks);
            myPlot.Axes.Bottom.MajorTickStyle.Length = 0;

            myPlot.YLabel("Number of Sessions");
            myPlot.XLabel("Month");
            myPlot.Title("Sessions per Month");

            myPlot.HideGrid();
            myPlot.Axes.Margins(bottom: 0, top: 0.2);

            return myPlot;
        }
        public Plot PreparePlot4()
        {
            return new Plot();
        }
        public int CleanseArray(in double[] xs, in double[] ys, out double[] xset, out double[] yset)
        {
            if (xs.Length == 0)
            {
                xset = Array.Empty<double>();
                yset = Array.Empty<double>();
                return 0;
            }
            double temp = xs[0];
            int l = 1;
            // only for sorted xs
            for (int i = 1; i < xs.Length; i++) 
            {
                if(temp !=  xs[i])
                {
                    temp = xs[i];
                    l += 1;
                }
            }
            xset = new double[l];
            yset = new double[l];
            for (int i = 0; i < l; i++)
            {
                yset[i] = 0;
            }
            temp = xs[0];
            xset[0] = temp;
            int j = 0;
            for (int i = 0; i < xs.Length; i++)
            {
                if (temp != xs[i])
                {
                    temp = xs[i];
                    j += 1;
                    xset[j] = temp;
                }
                yset[j] += ys[i];
            }
            return l;
        }
    }
}
