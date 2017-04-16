namespace DensityCalcClassLibrary
{
    public enum TypeGroup //Тип группы определения коэффицентов К
    {
        Neft, NefteProdukt, Maslo
    }

    public class Koef //расчет коэффицентов К0,К1,К2
    {
        public double K0 = 0;
        public double K1 = 0;
        public double K2 = 0;

        public Koef(TypeGroup typeGroup, double plotnost)
        {
            Calc(typeGroup, plotnost);
        }

        private void Calc(TypeGroup typeGroup, double plotnost)
        {
            if (typeGroup == TypeGroup.Neft && plotnost >= 611.2 && plotnost < 1163.8)
            {
                K0 = 613.9723;
                K1 = 0;
                K2 = 0;
            }
            else if (typeGroup == TypeGroup.NefteProdukt)
            {
                if (plotnost >= 611.2 && plotnost < 770.9)
                {
                    K0 = 346.4228;
                    K1 = 0.43884;
                    K2 = 0;
                }
                else if (plotnost >= 770.9 && plotnost < 788)
                {
                    K0 = 2690.7440;
                    K1 = 0;
                    K2 = -0.0033762;
                }
                else if (plotnost >= 788 && plotnost < 838.7)
                {
                    K0 = 594.5418;
                    K1 = 0;
                    K2 = 0;
                }
                else if (plotnost >= 838.7 && plotnost < 1163.9)
                {
                    K0 = 186.9696;
                    K1 = 0.4862;
                    K2 = 0;
                }
            }
            else if (typeGroup == TypeGroup.Maslo && plotnost >= 838.7 && plotnost < 1163.9)
            {
                K0 = 186.9696;
                K1 = 0.4862;
                K2 = 0;
            }
        }
    }
}