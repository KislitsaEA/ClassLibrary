using System;

namespace DensityCalcClassLibrary
{
    public interface IFindPlotnost
    {
        double Find(double t, double davlenie);
    }

    public class FindPlotnostForAreometr : IFindPlotnost
    {
        private double _tIzm;//температура измерения
        private IAreometr _areometr; //тип ареометра
        private double _plotnostAreometr; //плотность ареометра
        private TypeGroup _typeGroup; // тип нефтепродукта

        public FindPlotnostForAreometr(IAreometr areometr, double plotnostAreometr, double tIzm, TypeGroup typeGroup)
        {
            _tIzm = tIzm;
            _areometr = areometr;
            _plotnostAreometr = plotnostAreometr;
            _typeGroup = typeGroup;
        }

        public double Find(double t, double davlenie)
        {
            double _B15;
            double plotnost = Raschot.PlotnostAreometrInNeft(_areometr, _plotnostAreometr, _tIzm);
            plotnost = Raschot.IteracionMetodForAreometr(out _B15, plotnost, _tIzm, _typeGroup);
            double Y = Raschot.CalcY(plotnost, t);
            double resultPlotnost = Raschot.CalcPlotnost(plotnost, _B15, Y, t, davlenie);
            return Math.Round(resultPlotnost, 1);
        }
    }

    public class FindPlotnostForPlotnometr : IFindPlotnost
    {
        private double _tIzm;//температура измерения
        private double _plotnostPlotnometr;//плотность плотнометра
        private double _davlenie;//давление измерения
        private double _pogreshnost;// погрешность плотнометра
        private TypeGroup _typeGroup;// тип нефтепродукта


        public FindPlotnostForPlotnometr(double davlenie, double plotnostPlotnometr, double tIzm, double pogreshnost, TypeGroup typeGroup)
        {
            _tIzm = tIzm;
            _davlenie = davlenie;
            _plotnostPlotnometr = plotnostPlotnometr;
            _pogreshnost = pogreshnost;
            _typeGroup = typeGroup;
        }

        public double Find(double t, double davlenie)
        {
            double _B15;
            double plotnost = Raschot.IteracionMetodForPlotnometr(out _B15, _plotnostPlotnometr, _tIzm, _davlenie, _typeGroup);
            double Y=Raschot.CalcY(plotnost, t);
            double resultPlotnost=Raschot.CalcPlotnost(plotnost, _B15, Y, t, davlenie);
            
            if (_pogreshnost<0.5) return Math.Round(resultPlotnost, 2);
            return Math.Round(resultPlotnost, 1);

        }
    }
}