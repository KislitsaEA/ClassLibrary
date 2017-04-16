using System;

namespace DensityCalcClassLibrary
{
    public interface IAreometr
    {
        double Calc(double plotnostAreometr, double t);
    }

    public class Areometr15 : IAreometr //Ареометр градуированный при 15 градусах (7)
    {
        public double Calc(double plotnostAreometr, double t)
        {
            double K = 0;
            K = (1 - (0.000023 * (t - 15)) - 0.00000002 * Math.Pow(t - 15, 2));
            return plotnostAreometr * Math.Round(K, 4);
        }
    }

    public class Areometr20 : IAreometr //Ареометр градуированный при 20 градусах (8)
    {
        public double Calc(double plotnostAreometr, double t)
        {
            double K = 0;
            K = (1 - (0.000025 * (t - 20)));
            return plotnostAreometr * Math.Round(K, 4);
        }
    }
}