using Microsoft.EntityFrameworkCore;
using projekt_ArtistDatabase.EFCore;
using projekt_ArtistDatabase.ViewModels;
using projekt_ArtistDatabase;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;

namespace projekt_ArtistDatabase
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;
        public static ArtistsViewModel ArtistsViewModel;
        public App()
        {
            _navigationStore = new();
        }
        /// <summary>
        /// database context variable initialised in OnStartup and disposed in destructor
        /// </summary>
        public static ArtistContext context;
        protected override void OnStartup(StartupEventArgs e)
        {
            // Connection to the database
            context = new();
            context.Database.Migrate();

            // SUBSCRIPTION FOR DATABASE UPDATES
            context.ChangeTracker.StateChanged += (sender, args) =>
            {
                if (args.Entry.Entity is Artist changedArtist)
                {
                    if (args.Entry.State == EntityState.Added)
                    {
                        ArtistsViewModel.ArtistsOutput.Add(changedArtist);
                        ArtistsViewModel.ArtistsOutput = new ObservableCollection<Artist>(ArtistsViewModel.ArtistsOutput.OrderBy(x => x.Name));
                    }
                    else if (args.Entry.State == EntityState.Deleted)
                    {
                        ArtistsViewModel.ArtistsOutput.Remove(changedArtist);
                    }
                    else if (args.Entry.State == EntityState.Modified)
                    {
                        ArtistsViewModel.ArtistsOutput = new ObservableCollection<Artist>(ArtistsViewModel.ArtistsOutput.OrderBy(x => x.Name));
                        var index = ArtistsViewModel.ArtistsOutput.IndexOf(changedArtist);
                        if (index >= 0)
                        {
                            ArtistsViewModel.ArtistsOutput[index] = changedArtist;
                            ArtistsViewModel.SelectedArtist = changedArtist;
                        }
                    }
                }
                else if (args.Entry.Entity is Album album && args.Entry.State == EntityState.Deleted)
                {
                    ArtistsViewModel.ArtistsOutput = new ObservableCollection<Artist>(context.Artists.OrderBy(x => x.Name));
                }

            };

            // Sample data fill if database empty - WE HAVE .csv IMPORT/EXPORT
            //if (!context.Artists.Any() || !context.Albums.Any() || !context.Genres.Any())
            //{
            //    DatabaseHandler.InsertSampleData();
            //}

            // Show MainWindow, passing DataContext
            ArtistsViewModel = new ArtistsViewModel(context, _navigationStore);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore, ArtistsViewModel)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }

        /// <summary>
        /// Destructor to close database context
        /// </summary>
        ~App()
        {
            context.Dispose();
        }
    }
}
