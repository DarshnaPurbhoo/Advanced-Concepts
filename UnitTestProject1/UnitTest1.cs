using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            double Algebra = 81;
            double Calculus = 79;
            double Programming = 83;
            double Databases = 89;
            double Expected = 83;

            //Act
            double Actual = WindowsFormsApp_Practises.Form1.CalculateAverage(Algebra, Calculus, Programming, Databases);

            //Assert
            Assert.AreEqual(Expected, Actual);

        }
    }
}
