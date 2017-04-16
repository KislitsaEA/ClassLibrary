namespace DensityCalcClassLibrary
{
    public interface IDensityCalc
    {
        double FindPlotnost(double t, double davlenie);
    }

    public class DensityCalc
    {
        private IFindPlotnost findPlotnost;

        public DensityCalc(IAreometr areometr, double plotnostAreometr, double tIzm)//для ареометра
        {
            findPlotnost = new FindPlotnostForAreometr(areometr, plotnostAreometr, tIzm); 
        }

        public DensityCalc(double davlenie, double plotnostPlotnometr, double tIzm)//для плотнометра 
        {
            findPlotnost = new FindPlotnostForPlotnometr(davlenie, plotnostPlotnometr, tIzm); 
        }

        public double FindPlotnost(double t,double davlenie)// определение плотности при темературе t и давлении P
        {
            return findPlotnost.Find(t,davlenie);
        }
    }




}
