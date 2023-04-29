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

            _artistsViewModel.PropertyChanged += ArtistViewModel_PropertyChanged;
        }

        private void ArtistViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_artistsViewModel.IsArtistGenreSelected))
            {
                OnCanExecuteChanged();
            }
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            var selectedGenre = _artistsViewModel.SelectedArtistGenre;
            if (DatabaseHandler.DeleteRecord(selectedGenre) != null)
            {
                App.context.SaveChanges();
                _artistsViewModel.SelectedArtistGenres.Remove(selectedGenre);
                MessageBox.Show("Genre deleted successfuly.");
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
