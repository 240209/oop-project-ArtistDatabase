using projekt_ArtistDatabase.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace projekt_ArtistDatabase.Commands
{
    public class RemoveGenreCommand : AsyncCommandBase
    {
        private readonly ArtistsViewModel _artistsViewModel;

        public RemoveGenreCommand(ArtistsViewModel artistsViewModel)
        {
            _artistsViewModel = artistsViewModel;

            // subscribing to the artistsviewmodel property change to enable the RemoveAlbum command
            _artistsViewModel.PropertyChanged += ArtistViewModel_PropertyChanged;
        }

        private void ArtistViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            // check if the changed property is IsArtistAlbumSelected
            if (e.PropertyName == nameof(_artistsViewModel.IsArtistGenreSelected))
            {
                // then change the CanExecute to true
                OnCanExecuteChanged();
            }
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            var selectedGenre = _artistsViewModel.SelectedArtistGenre;
            if (DatabaseHandler.DeleteRecord(selectedGenre) != null)
            {
                MessageBox.Show("Genre deleted successfuly.");
                App.context.SaveChanges();
                // Remove removed album from UI
                _artistsViewModel.SelectedArtistGenres.Remove(selectedGenre);
                _artistsViewModel.updateAlbumsGenresBooleans();
            }
            else
            {
                MessageBox.Show("Genre cannot be deleted.");
            }
        }
        public override bool CanExecute(object? parameter)
        {
            return _artistsViewModel.IsArtistGenreSelected && base.CanExecute(parameter);
        }
    }
}
