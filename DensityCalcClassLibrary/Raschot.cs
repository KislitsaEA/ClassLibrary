using System;

namespace DensityCalcClassLibrary
{
    public static class Raschot
    {
        public static double CalcPlotnost(double plotnost15, double B15, double Y, double t, double P) //расчет плотности (1)
        {
            double plotnost = (plotnost15 * Math.Exp((0 - B15) * (t - 15) * (1 + (0.8 * B15 * (t - 15))))) / (1 - (Y * P));
            return Math.Round(plotnost, 2);
        }

        public static double CalcPlotnostForIterAreometr(double plotnost, double B15, double tIzm) //расчет плотности P15 для итерационного метода для ареометра (1)
        {
            double plotnost15 = plotnost / Math.Exp((0 - B15) * (tIzm - 15) * (1 + (0.8 * B15 * (tIzm - 15))));
            return Math.Round(plotnost15, 2);
        }

        public static double CalcPlotnostForIterPlotnometr(double plotnost, double B15, double Y, double tIzm, double P) //расчет плотности P15 для итерационного метода для плотнометра  (1)
        {
            double plotnost15 = (plotnost*(1-(Y*P)))/ Math.Exp((0 - B15) * (tIzm - 15) * (1 + (0.8 * B15 * (tIzm - 15))));
            return Math.Round(plotnost15, 2);
        }

        public static double CalcKoefB15(double plotnost) // расчет коэффицента B15 (2)
        {
            Koef koef = new Koef(TypeGroup.Neft, plotnost);
            return Math.Round((koef.K0 + koef.K1 * plotnost) / Math.Pow(plotnost, 2) + koef.K2, 7);
        }

        public static double PlotnostAreometrInNeft(IAreometr areometr, double plotnost, double tIzm) //пересчет показаний ареометра в плотность нефти (6)
        {
            return Math.Round(areometr.Calc(plotnost, tIzm), 1);
        }

        public static double IteracionMetodForAreometr(out double B15, double firstPlotnost, double tIzm) //итерационный метод нахождения плотности для ареометра
        {
            B15 = CalcKoefB15(firstPlotnost);
            double endPlotnost = CalcPlotnostForIterAreometr(firstPlotnost, B15, tIzm);
            double currentPlotnost = 0;

            while (Math.Abs(endPlotnost - currentPlotnost) > 0.01)
            {
                currentPlotnost = endPlotnost;
                B15 = CalcKoefB15(currentPlotnost);
                endPlotnost = CalcPlotnostForIterAreometr(firstPlotnost, B15, tIzm);
            }
            return Math.Round(endPlotnost, 1);
        }

        public static double IteracionMetodForPlotnometr(out double B15, double firstPlotnost, double tIzm, double davlenie) //итерационный метод нахождения плотности для плотнометра
        {

            /*1 итерация*/
            B15=Raschot.CalcKoefB15(firstPlotnost);
            double Y = Raschot.CalcY(firstPlotnost, tIzm);
            double endPlotnost = Raschot.CalcPlotnostForIterPlotnometr(firstPlotnost, B15, Y, tIzm, davlenie);
            double currentPlotnost = 0;

            while (Math.Abs(endPlotnost - currentPlotnost) > 0.01)//разность между итерациями
            {
                currentPlotnost = endPlotnost;
                B15 = Raschot.CalcKoefB15(currentPlotnost);
                Y = Raschot.CalcY(currentPlotnost, tIzm);
                endPlotnost = Raschot.CalcPlotnostForIterPlotnometr(firstPlotnost, B15, Y, tIzm, davlenie);
            }
            return Math.Round(endPlotnost, 1);
        }

        public static double CalcY(double plotnost, double t)//расчет коэффицента сжимаемости (3)
        {
            double Y = Math.Pow(10, -3) *
                       Math.Exp(-1.62080 +
                                0.00021592 * t +
                                (0.87096 * Math.Pow(10, 6) / Math.Pow(plotnost, 2) +
                                 (4.2092 * t * Math.Pow(10, 3) / Math.Pow(plotnost, 2))));
            return Math.Round(Y, 7);
        }

    }
}