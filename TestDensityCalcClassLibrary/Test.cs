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
            double Y = Raschot.CalcY(836.15,27.3);
            double result = 0.0008148;
            Assert.That(Y,Is.EqualTo(result));
        }

    }
}
