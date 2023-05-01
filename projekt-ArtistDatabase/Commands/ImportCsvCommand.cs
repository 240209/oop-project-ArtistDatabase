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
            // getting the input file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv";
            string fileOutputPath = string.Empty;

            if (openFileDialog.ShowDialog() == true)
            {
                fileOutputPath = openFileDialog.FileName;
            }

            // if cancelled
            if (fileOutputPath == string.Empty)
            {
                MessageBox.Show("Import canceled.");
                return;
            }

            if (JsonHandler.Import(fileOutputPath))
            {
                MessageBox.Show("Database imported from .csv file successfuly.");
            }
            else
            {
                MessageBox.Show("Failed to import database.");
            }
        }

    }
}
