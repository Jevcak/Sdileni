
using GymLogger;

namespace GymLoggerTest
{
    public class GraphPlotterTest
    {
        [Fact]
        public void CleanseArrayTestEmpty()
        {
            //Arrange
            GraphPlotter plotter = new GraphPlotter();
            //Act
            plotter.CleanseArray(Array.Empty<double>(), Array.Empty<double>(), out double[] xset, out double[] yset);
            //Assert
            Assert.Empty(xset);
            Assert.Empty(yset);
        }
        [Fact]
        public void CleanseArrayTestLength()
        {
            //Arrange
            GraphPlotter plotter = new GraphPlotter();
            double[] xs = { 12, 12,34, 34,56.42,56.42 };
            double[] ys = { 0,0,0,0,0,0};
            int expected = 3;
            //Act
            int actual = plotter.CleanseArray(xs, ys, out double[] xset, out double[] yset);
            //Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void CleanseArrayTestArray()
        {
            //Arrange
            GraphPlotter plotter = new GraphPlotter();
            double[] xs = { 12, 12, 34, 34, 42, 56.42, 56.42, 57};
            double[] ys = { 10, 20, 53, 25, 42, 56, 65, 75 };
            double[] expectedx = { 12, 34, 42, 56.42, 57 };
            double[] expectedy = { 30, 78, 42, 121, 75 };
            //Act
            int _ = plotter.CleanseArray(xs, ys, out double[] xset, out double[] yset);
            //Assert
            for (int i = 0; i < expectedx.Length; i++)
            {
                Assert.Equal(expectedx[i], xset[i]);
                Assert.Equal(expectedy[i], yset[i]);
            }
        }
    }
}
