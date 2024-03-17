using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace PdfDataExtract
{
    public partial class MainWindow : Window
    {
        private readonly List<string> pdfFiles = [];
        private string? csvFilePath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UploadPdfFiles_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Multiselect = true,
                Filter = "PDF Files (*.pdf)|*.pdf"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                pdfFiles.AddRange(openFileDialog.FileNames);
                UpdatePdfFilesListBox();
            }
        }

        private void UpdatePdfFilesListBox()
        {
            PdfFilesListBox.Items.Clear();
            foreach (string file in pdfFiles)
            {
                PdfFilesListBox.Items.Add(Path.GetFileName(file));
            }
        }

        private void UploadCsvFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "CSV Files (*.csv)|*.csv"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                csvFilePath = openFileDialog.FileName;
                CsvFilePathTextBox.Text = csvFilePath;
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (pdfFiles.Count == 0 || string.IsNullOrEmpty(csvFilePath))
            {
                MessageBox.Show("Please upload PDF files and CSV file.");
                return;
            }

            var searchData= TextExtract.CsvExtract(csvFilePath);
            List<string> results = [];

            foreach (string pdfFile in pdfFiles)
            {
                string pdfText = TextExtract.PdfExtract(pdfFile);
                if (DataSearch.ContainsData(pdfText, searchData))
                {
                    results.Add(pdfFile);
                }
            }

            DisplayResults(results);
        }


        private static void DisplayResults(List<string> results)
        {
            if (results.Count == 0)
            {
                MessageBox.Show("No matching results found.");
            }
            else
            {
                MessageBox.Show("Matching results found in:\n" + string.Join("\n", results));
            }
        }
    }
}
