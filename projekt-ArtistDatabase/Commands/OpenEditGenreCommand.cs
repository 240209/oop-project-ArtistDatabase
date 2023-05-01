using projekt_ArtistDatabase.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace projekt_ArtistDatabase.Commands
{
    public class OpenEditGenreCommand : CommandBase
    {
        private readonly ArtistsViewModel _artistsViewModel;
        private readonly NavigationStore _navigationStore;
        public OpenEditGenreCommand(ArtistsViewModel artistsViewModel, NavigationStore navigationStore)
        {
            _artistsViewModel = artistsViewModel;
            _navigationStore = navigationStore;

            // subscribing to the artistsviewmodel property change to enable the EditGenre command
            _artistsViewModel.PropertyChanged += ArtistViewModel_PropertyChanged;
        }

        private void ArtistViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            // check if the changed property is IsArtistGenreSelected
            if (e.PropertyName == nameof(_artistsViewModel.IsArtistGenreSelected))
            {
                // then change the CanExecute to true
                OnCanExecuteChanged();
            }
        }
        public override void Execute(object? parameter)
        {
            CloseModalCommand cancelCommand = new(_navigationStore);
            EditGenreCommand submitCommand = new(_artistsViewModel.SelectedArtist, _artistsViewModel.SelectedArtistGenre, _navigationStore);

            EditGenreViewModel editGenreViewModel = new(_artistsViewModel.SelectedArtistGenre, cancelCommand, submitCommand);

            submitCommand.EditGenreViewModel = editGenreViewModel;

            _navigationStore.CurrentViewModel = editGenreViewModel;
        }
        public override bool CanExecute(object? parameter)
        {
            return _artistsViewModel.IsArtistGenreSelected && base.CanExecute(parameter);
        }
    }
}
