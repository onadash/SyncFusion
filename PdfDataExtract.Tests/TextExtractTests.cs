using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf;


namespace PdfDataExtract.Tests
{
    [TestClass]
    public class TextExtractTests
    {

        private const string CsvFilePath = "test.csv";

        [ClassInitialize]
        public static void ClassInitialize()
        {
           
            // Create a sample CSV file for testing
            using (StreamWriter writer = new(CsvFilePath))
            {
                writer.WriteLine("Key1;Key2");
                writer.WriteLine("Value1;Value2");
            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            // Clean up test files
            File.Delete(CsvFilePath);
        }

        [DataTestMethod]
        [DataRow("test.csv", 2)]
        public void CsvExtract_ReturnsExpectedNumberOfValues(string filePath, int expectedCount)
        {
            // Act
            var extractedValues = TextExtract.CsvExtract(filePath).ToList();

            // Assert
            Assert.AreEqual(expectedCount, extractedValues.Count);
        }

        [DataTestMethod]
        [DataRow("test.csv", new[] { "Key1", "Key2" }, new[] { "Value1", "Value2" })]
        public void CsvExtract_ReturnsExpectedValues(string filePath, string[] expectedKeys, string[] expectedValues)
        {
            // Act
            var extractedValues = TextExtract.CsvExtract(filePath).ToList();

            // Assert
            Assert.AreEqual(expectedKeys.Length, extractedValues.Count);
            for (int i = 0; i < expectedKeys.Length; i++)
            {
                Assert.IsTrue(extractedValues.Any(v => v.Item1 == expectedKeys[i] && v.Item2 == expectedValues[i]));
            }
        }

        [TestMethod]
        public void CsvExtract_ReturnsEmptyCollectionWhenFileIsEmpty()
        {
            // Arrange
            string emptyCsvFilePath = "empty.csv";
            File.Create(emptyCsvFilePath).Dispose();

            // Act
            var extractedValues = TextExtract.CsvExtract(emptyCsvFilePath);

            // Assert
            Assert.IsFalse(extractedValues.Any());
        }
    }
}
