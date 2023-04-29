using Microsoft.EntityFrameworkCore;
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
            if (JsonHandler.Export())
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
