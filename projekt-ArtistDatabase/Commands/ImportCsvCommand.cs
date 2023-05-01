using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projekt_ArtistDatabase.Commands
{
    public class ImportCsvCommand : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object? parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv"; // file type filter
            string fileOutputPath = string.Empty;

            if (openFileDialog.ShowDialog() == true)
            {
                fileOutputPath = openFileDialog.FileName;
            }


            if (JsonHandler.Import(fileOutputPath))
            {
                MessageBox.Show("Database imported from ArtistDatabaseExport.csv successfuly.");
            }
            else
            {
                MessageBox.Show("Failed to import database.");
            }
        }

    }
}
