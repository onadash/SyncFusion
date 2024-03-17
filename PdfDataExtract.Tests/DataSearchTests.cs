using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace PdfDataExtract.Tests
{
    [TestClass]
    public class DataSearchTests
    {
        [TestMethod]
        [DataRow("Some text", new string[] { "", "" }, false)]
        [DataRow(null, new string[] { "pattern1", "pattern2" }, false)]
        [DataRow("Some text", null, false)]
        [DataRow("Some text with pattern1 pattern3 and and pattern2", new string[] { "pattern1", "pattern2" }, false)]
        [DataRow("Some text without pattern1", new string[] { "pattern1", "pattern2" }, false)]
        [DataRow("Some text with pattern1 *-= pattern2", new string[] { "pattern1", "pattern2" }, true)]
        public void ContainsData_ReturnsExpectedResult(string pdfText, string[] searchData, bool expectedResult)
        {
            // Arrange
            var searchDataTuples = searchData!= null? searchData.Select(s => (s, "pattern2")):null;

            // Act
            var result = DataSearch.ContainsData(pdfText, searchDataTuples);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
