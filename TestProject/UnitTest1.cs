using ConsoleApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAdition()
        {
            Calculatrice calculatrice = new();
            Utile utile = new();

            List<string> list = utile.SeparateNumbersSigns("1 + -1");
            double result = calculatrice.Calculate(list);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestSubtraction()
        {
            Calculatrice calculatrice = new();
            Utile utile = new();

            List<string> list = utile.SeparateNumbersSigns("-1 - -1");
            double result = calculatrice.Calculate(list);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestMultiplication()
        {
            Calculatrice calculatrice = new();
            Utile utile = new();

            List<string> list = utile.SeparateNumbersSigns("(2+5)*3");
            double result = calculatrice.Calculate(list);

            Assert.AreEqual(21, result);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void TestDivision()
        {
            Calculatrice calculatrice = new();
            Utile utile = new();

            List<string> list = utile.SeparateNumbersSigns("1/0");
            double result = calculatrice.Calculate(list);

            Assert­.ThrowsException<DivideByZeroException>(() => calculatrice.Calculate(list), "Erreur - Division par zéro indéfini");
        }

        [TestMethod]
        public void TestExponential()
        {
            Calculatrice calculatrice = new();
            Utile utile = new();

            List<string> list = utile.SeparateNumbersSigns("2^8*5-1");
            double result = calculatrice.Calculate(list);

            Assert.AreEqual(1279, result);
        }

        [TestMethod]
        public void TestSquareRoot()
        {
            Calculatrice calculatrice = new();
            Utile utile = new();

            List<string> list = utile.SeparateNumbersSigns("sqrt(4)");
            double result = calculatrice.Calculate(list);

            Assert.AreEqual(2, result);
        }
    }
}