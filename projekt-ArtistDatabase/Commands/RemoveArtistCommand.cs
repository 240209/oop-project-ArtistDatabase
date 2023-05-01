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
    public class RemoveArtistCommand : AsyncCommandBase
    {
        private readonly ArtistsViewModel _artistsViewModel;

        public RemoveArtistCommand(ArtistsViewModel artistsViewModel)
        {
            _artistsViewModel = artistsViewModel;

            _artistsViewModel.PropertyChanged += ArtistViewModel_PropertyChanged;

        }

        private void ArtistViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_artistsViewModel.IsArtistSelected))
            {
                OnCanExecuteChanged();
            }
        }
        public override async Task ExecuteAsync(object? parameter)
        {
            var selectedArtist = _artistsViewModel.SelectedArtist;
            if (DatabaseHandler.DeleteRecord(selectedArtist) != null)
            {
                MessageBox.Show("Artist deleted successfuly.");
                App.context.SaveChanges();
                _artistsViewModel.ArtistsOutput.Remove(selectedArtist);
            }
            else
            {
                MessageBox.Show("Artist cannot be deleted.");
            }
        }
        public override bool CanExecute(object? parameter)
        {
            return _artistsViewModel.IsArtistSelected && base.CanExecute(parameter);
        }
    }
}
