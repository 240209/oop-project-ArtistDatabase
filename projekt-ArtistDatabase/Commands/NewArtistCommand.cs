using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using projekt_ArtistDatabase.EFCore;
using projekt_ArtistDatabase.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projekt_ArtistDatabase.Commands
{
    public class NewArtistCommand : AsyncCommandBase
    {
        private readonly NavigationStore _navigationStore;
        public NewArtistViewModel NewArtistViewModel;

        public NewArtistCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Artist artist = new Artist();
            artist.Name = NewArtistViewModel.Name;

            if (DatabaseHandler.InsertRecord(artist))
            {
                MessageBox.Show("Artist added succesfully.");
                App.context.SaveChanges();
                App.context.Entry(artist).State = EntityState.Added;
            }
            else
            {
                MessageBox.Show("Artist addition failed.");
                return;
            }


            _navigationStore.Close();
        }
    }
}
