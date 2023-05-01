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

            // subscribing to the artistsviewmodel property change to enable the RemoveArtist command
            _artistsViewModel.PropertyChanged += ArtistViewModel_PropertyChanged;

        }

        private void ArtistViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            // check if the changed property is IsArtistSelected
            if (e.PropertyName == nameof(_artistsViewModel.IsArtistSelected))
            {
                // then change the CanExecute to true
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
                // Remove removed artist from UI
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
