using System.Text.RegularExpressions;

namespace PdfDataExtract
{
    public class DataSearch
    {

        public static bool ContainsData(string pdfText, IEnumerable<(string, string)> searchData)
        {

            if (pdfText == null|| searchData == null) return false;
            return searchData.Any(dataItem => dataItem.Item1!= null && dataItem.Item2 != null&& Regex.IsMatch(pdfText, $"{dataItem.Item1}[^a-zA-Z0-9]*{dataItem.Item2}"));
        }
    }
}
