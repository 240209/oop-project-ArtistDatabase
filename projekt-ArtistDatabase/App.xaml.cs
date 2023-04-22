using Microsoft.EntityFrameworkCore;
using projekt_ArtistDatabase.EFCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace projekt_ArtistDatabase
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// database context variable initialised in OnStartup and disposed in destructor
        /// </summary>
        private ArtistContext _context;
        protected override void OnStartup(StartupEventArgs e)
        {
            // Connection to the database
            _context = new();
            _context.Database.Migrate();

            // Sample data fill if database empty
            if (!_context.Artists.Any())
            {
                DatabaseHandler.InsertSampleData(_context);
            }

            // Show MainWindow, passing DataContext
            MainWindow = new MainWindow()
            {
                DataContext = new ArtistsViewModel(_context)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }

        /// <summary>
        /// Destructor to close database context
        /// </summary>
        ~App()
        {
            _context.Dispose();
        }
    }
}
