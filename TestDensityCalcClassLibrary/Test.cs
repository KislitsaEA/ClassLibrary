using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using DensityCalcClassLibrary;

namespace TestDensityCalcClassLibrary
{
    [TestFixture]
    public class ResultTest
    {
        [Test]
        public void CalcY_Result()//проверка функции CalcY
        {
            double Y = Raschot.CalcY(836.15, 27.3);
            double result = 0.0008148;
            Assert.That(Y, Is.EqualTo(result));
        }

    }

    [TestFixture]
    public class DencityTest
    {
        [TestCase(0, 45, 744.0)]
        [TestCase(0, 46, 743.1)]
        [TestCase(0, 47, 742.2)]
        [TestCase(0, 58, 732.2)]
        [TestCase(0, 59, 731.3)]
        [TestCase(0, 60, 730.4)]
        [TestCase(0, 67, 724.0)]
        [TestCase(0, 68, 723.1)]
        [TestCase(0, 73, 718.5)]
        [TestCase(0, 74, 717.6)]
        [TestCase(0, 79, 713.0)]
        public void TestDensityCalcClassLibrary(double p, double T, double dens)
        {
            var test = new DensityCalc(p, dens, T,0.5,TypeGroup.Neft);
            var dens15 = test.FindPlotnost(15, 0);

            var testtP = new DensityCalc(0, dens15, 15,0.5,TypeGroup.Neft);
            var denstP = test.FindPlotnost(T, p);

            Console.WriteLine("{0}\t\t{1}",dens,denstP);

            Assert.That(dens, Is.EqualTo(denstP).Within(0.01));
        }
    }
}
