using System;

namespace DensityCalcClassLibrary
{
    public interface IFindPlotnost
    {
        double Find(double t, double davlenie);
    }

    public class FindPlotnostForAreometr : IFindPlotnost
    {
        private double _tIzm;//����������� ���������
        private IAreometr _areometr; //��� ���������
        private double _plotnostAreometr; //��������� ���������
        private TypeGroup _typeGroup; // ��� �������������

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
        private double _tIzm;//����������� ���������
        private double _plotnostPlotnometr;//��������� �����������
        private double _davlenie;//�������� ���������
        private double _pogreshnost;// ����������� �����������
        private TypeGroup _typeGroup;// ��� �������������


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