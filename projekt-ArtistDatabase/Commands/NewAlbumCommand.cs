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
    public class NewAlbumCommand : AsyncCommandBase
    {
        private readonly NavigationStore _navigationStore;
        public NewAlbumViewModel NewAlbumViewModel;
        private readonly Artist _artistToBeUpdated;

        public NewAlbumCommand(Artist updatedArtist, NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _artistToBeUpdated = updatedArtist;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Album newAlbum = new();
            newAlbum.Name = NewAlbumViewModel.Name;
            newAlbum.Year = NewAlbumViewModel.Year;
            newAlbum.Artist = _artistToBeUpdated;
            newAlbum.ArtistId = _artistToBeUpdated.Id;

            if (DatabaseHandler.InsertRecord(newAlbum))
            {
                MessageBox.Show("Album added succesfully.");
                App.context.SaveChanges();
                App.context.Entry(_artistToBeUpdated).State = EntityState.Modified;
            }
            else
            {
                MessageBox.Show("Album addition failed.");
            }

            _navigationStore.Close();
        }
    }
}
