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
            if (JsonHandler.Import())
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
