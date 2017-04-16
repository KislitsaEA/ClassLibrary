using System;

namespace DensityCalcClassLibrary
{
    public interface IFindPlotnost
    {
        double Find(double t, double davlenie);
    }

    public class FindPlotnostForAreometr : IFindPlotnost
    {
        public double _tIzm;//����������� ���������
        public IAreometr _areometr; //��� ���������
        public double _plotnostAreometr; //��������� ���������

        public FindPlotnostForAreometr(IAreometr areometr, double plotnostAreometr, double tIzm)
        {
            _tIzm = tIzm;
            _areometr = areometr;
            _plotnostAreometr = plotnostAreometr;
        }

        public double Find(double t, double davlenie)
        {
            double _B15;
            double plotnost = Raschot.PlotnostAreometrInNeft(_areometr, _plotnostAreometr, _tIzm);
            plotnost = Raschot.IteracionMetodForAreometr(out _B15, plotnost, _tIzm);
            double Y = Raschot.CalcY(plotnost, t);
            double resultPlotnost = Raschot.CalcPlotnost(plotnost, _B15, Y, t, davlenie);
            return Math.Round(resultPlotnost, 1);
        }
    }

    public class FindPlotnostForPlotnometr : IFindPlotnost
    {
        public double _tIzm;//����������� ���������
        public double _plotnostPlotnometr;//��������� �����������
        public double _davlenie;//�������� ���������


        public FindPlotnostForPlotnometr(double davlenie, double plotnostPlotnometr, double tIzm)
        {
            _tIzm = tIzm;
            _davlenie = davlenie;
            _plotnostPlotnometr = plotnostPlotnometr;
        }

        public double Find(double t, double davlenie)
        {
            double _B15;
            double plotnost = Raschot.IteracionMetodForPlotnometr(out _B15, _plotnostPlotnometr, _tIzm, _davlenie);
            double Y=Raschot.CalcY(plotnost, t);
            return Raschot.CalcPlotnost(plotnost, _B15, Y, t, davlenie);
        }
    }
}