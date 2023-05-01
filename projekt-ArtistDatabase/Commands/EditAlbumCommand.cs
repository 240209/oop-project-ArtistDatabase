using Microsoft.EntityFrameworkCore;
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
    public class EditAlbumCommand : AsyncCommandBase
    {
        private readonly NavigationStore _navigationStore;
        public EditAlbumViewModel EditAlbumViewModel;
        private readonly Artist _artistToBeUpdated;
        private readonly Album _albumToBeUpdated;

        public EditAlbumCommand(Artist updatedArtist, Album updatedAlbum, NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _artistToBeUpdated = updatedArtist;
            _albumToBeUpdated = updatedAlbum;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Album newAlbum = new();
            newAlbum.Name = EditAlbumViewModel.Name;
            newAlbum.Year = EditAlbumViewModel.Year;

            if (DatabaseHandler.UpdateRecord(_albumToBeUpdated, newAlbum))
            {
                MessageBox.Show("Album edited succesfully.");
                App.context.SaveChanges();
                App.context.Entry(_artistToBeUpdated).State = EntityState.Modified;
            }
            else
            {
                MessageBox.Show("Album edit failed.");
            }

            _navigationStore.Close();
        }
    }
}
