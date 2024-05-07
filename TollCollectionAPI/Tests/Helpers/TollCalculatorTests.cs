using Business.Helpers;
using Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Tests.Helpers
{
    [TestClass]
    public class TollCalculatorTests
    {

        [TestMethod]
        public void CalculateTaxPerDay_WithSinglePassage_ShouldReturnCorrectTax()
        {
            var tollPassages = new List<TollPassage>
            {
                new TollPassage { RegistrationNumber = "ABC123", Timestamp = new DateTime(2024, 5, 7, 7, 15, 0) }
            };

            var result = TollCalculator.CalculateTaxPerDay(tollPassages);

            Assert.AreEqual(22, result.TotalTaxAmount);
        }

        [TestMethod]
        public void CalculateTaxPerDay_WithMultiplePassagesInSameHour_ShouldReturnCorrectTax()
        {
            var tollPassages = new List<TollPassage>
            {
                new TollPassage { RegistrationNumber = "ABC123", Timestamp = new DateTime(2024, 5, 7, 7, 15, 0) },
                new TollPassage { RegistrationNumber = "ABC123", Timestamp = new DateTime(2024, 5, 7, 7, 45, 0) }
            };

            var result = TollCalculator.CalculateTaxPerDay(tollPassages);

            Assert.AreEqual(22, result.TotalTaxAmount);
        }

        [TestMethod]
        public void CalculateTaxPerDay_WithPassagesSpanningMultipleHours_ShouldReturnCorrectTax()
        {
            var tollPassages = new List<TollPassage>
            {
                new TollPassage { RegistrationNumber = "ABC123", Timestamp = new DateTime(2024, 5, 7, 6, 15, 0) },
                new TollPassage { RegistrationNumber = "ABC123", Timestamp = new DateTime(2024, 5, 7, 7, 14, 0) }, 
                new TollPassage { RegistrationNumber = "ABC123", Timestamp = new DateTime(2024, 5, 7, 8, 28, 0) } 
            };

            var result = TollCalculator.CalculateTaxPerDay(tollPassages);

            Assert.AreEqual(38, result.TotalTaxAmount);
        }

        [TestMethod]
        public void CalculateTaxPerDay_OnSwedishRedDay_ShouldReturnZeroTax()
        {
            var tollPassages = new List<TollPassage>
            {
                new TollPassage { RegistrationNumber = "ABC123", Timestamp = new DateTime(2024, 5, 9, 6, 15, 0) } // Kristihimmelfärd (Swedish red day)
            };

            var result = TollCalculator.CalculateTaxPerDay(tollPassages);

            Assert.AreEqual(0, result.TotalTaxAmount);
        }

        [TestMethod]
        public void CalculateTaxPerDay_OnSunday_ShouldReturnZeroTax()
        {
            var tollPassages = new List<TollPassage>
            {
                new TollPassage { RegistrationNumber = "ABC123", Timestamp = new DateTime(2024, 5, 12, 6, 15, 0) } // Sunday
            };

            var result = TollCalculator.CalculateTaxPerDay(tollPassages);

            Assert.AreEqual(0, result.TotalTaxAmount);
        }

        [TestMethod]
        public void CalculateTaxPerDay_InJuly_ShouldReturnZeroTax()
        {
            var tollPassages = new List<TollPassage>
            {
                new TollPassage { RegistrationNumber = "ABC123", Timestamp = new DateTime(2024, 7, 7, 6, 15, 0) } // July
            };

            var result = TollCalculator.CalculateTaxPerDay(tollPassages);
            Assert.AreEqual(0, result.TotalTaxAmount);
        }

        [TestMethod]
        public void CalculateTaxPerDay_MaxTaxPerDayIs60_ShouldReturn60()
        {
            var tollPassages = new List<TollPassage>
            {
                new TollPassage { RegistrationNumber = "ABC123", Timestamp = new DateTime(2024, 6, 7, 6, 15, 0) },
                new TollPassage { RegistrationNumber = "ABC123", Timestamp = new DateTime(2024, 6, 7, 7, 15, 0) },
                new TollPassage { RegistrationNumber = "ABC123", Timestamp = new DateTime(2024, 6, 7, 8, 15, 0) },
                new TollPassage { RegistrationNumber = "ABC123", Timestamp = new DateTime(2024, 6, 7, 9, 15, 0) },
                new TollPassage { RegistrationNumber = "ABC123", Timestamp = new DateTime(2024, 6, 7, 10, 15, 0) },
                new TollPassage { RegistrationNumber = "ABC123", Timestamp = new DateTime(2024, 6, 7, 12, 15, 0) },
                new TollPassage { RegistrationNumber = "ABC123", Timestamp = new DateTime(2024, 6, 7, 15, 15, 0) },
                new TollPassage { RegistrationNumber = "ABC123", Timestamp = new DateTime(2024, 6, 7, 16, 16, 0) }
            };

            var result = TollCalculator.CalculateTaxPerDay(tollPassages);
            Assert.AreEqual(60, result.TotalTaxAmount);
        }
    }
}
