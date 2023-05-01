using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Xml;

namespace projekt_ArtistDatabase.Commands
{
    public class ExportCsvCommand : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object? parameter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv"; // Set file type filter
            saveFileDialog.FileName = "ArtistDatabaseExport.csv"; // Set default filename
            string fileOutputPath = string.Empty;

            if (saveFileDialog.ShowDialog() == true)
            {
                fileOutputPath = saveFileDialog.FileName;
            }

            if (JsonHandler.Export(fileOutputPath))
            {
                MessageBox.Show("Data exported into ArtistDatabaseExport.csv successfuly.");
            }
            else
            {
                MessageBox.Show("Failed to export database.");
            }
        }
    }
}
