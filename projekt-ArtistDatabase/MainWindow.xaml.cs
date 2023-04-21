using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using projekt_ArtistDatabase.EFCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

// export a import do a z CSV souborů

namespace projekt_ArtistDatabase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// database context variable initialised in constructor and disposed in destructor
        /// </summary>
        private ArtistContext _context;
        
        public MainWindow()
        {
            InitializeComponent();

            // Connection to the database
            _context = new();
            _context.Database.Migrate();

            // Sample data fill if database empty
            if(!_context.Artists.Any())
            {
                DatabaseHandler.InsertSampleData(_context);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        // TO DELETE
        private void SearchArtists(string inputText)
        {
            //artistFeed.Clear();
            //artistFeed.AddRange(_context.Artists.Where(x => x.Name.Contains(inputText)));
        }

        /// <summary>
        /// Destructor to close database context
        /// </summary>
        ~MainWindow()
        {
            _context.Dispose();
        }
    }
}
