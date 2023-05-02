using Microsoft.EntityFrameworkCore;
using projekt_ArtistDatabase.EFCore;
using projekt_ArtistDatabase.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projekt_ArtistDatabase.Commands
{
    public class RemoveAlbumCommand : AsyncCommandBase
    {
        private readonly ArtistsViewModel _artistsViewModel;

        public RemoveAlbumCommand(ArtistsViewModel artistsViewModel)
        {
            _artistsViewModel = artistsViewModel;

            // subscribing to the artistsviewmodel property change to enable the RemoveAlbum command
            _artistsViewModel.PropertyChanged += ArtistViewModel_PropertyChanged;
        }
        private void ArtistViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            // check if the changed property is IsArtistAlbumSelected
            if (e.PropertyName == nameof(_artistsViewModel.IsArtistAlbumSelected))
            {
                // then change the CanExecute to true
                OnCanExecuteChanged();
            }
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            var selectedAlbum = _artistsViewModel.SelectedArtistAlbum;
            if (DatabaseHandler.DeleteRecord(selectedAlbum) != null)
            {
                MessageBox.Show("Album deleted successfuly.");
                App.context.SaveChanges();
                // Remove removed album from UI
                _artistsViewModel.SelectedArtistAlbums.Remove(selectedAlbum);
                _artistsViewModel.updateAlbumsGenresBooleans();
            }
            else
            {
                MessageBox.Show("Album cannot be deleted.");
            }
        }
        public override bool CanExecute(object? parameter)
        {
            return _artistsViewModel.IsArtistAlbumSelected && base.CanExecute(parameter);
        }
    }
}
