using projekt_ArtistDatabase.EFCore;
using projekt_ArtistDatabase.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace projekt_ArtistDatabase.Commands
{
    public class OpenEditArtistCommand : CommandBase
    {
        private readonly ArtistsViewModel _artistsViewModel;
        private readonly NavigationStore _navigationStore;
        public OpenEditArtistCommand(ArtistsViewModel artistsViewModel, NavigationStore navigationStore)
        {
            _artistsViewModel = artistsViewModel;
            _navigationStore = navigationStore;
        }
        public override void Execute(object? parameter)
        {
            CloseModalCommand cancelCommand = new(_navigationStore);
            EditArtistCommand submitCommand = new(_navigationStore);

            EditArtistViewModel editArtistViewModel = new(_artistsViewModel.SelectedArtist, cancelCommand, submitCommand);

            submitCommand.EditArtistViewModel = editArtistViewModel;
            submitCommand.oldArtist = _artistsViewModel.SelectedArtist;

            _navigationStore.CurrentViewModel = editArtistViewModel;
        }
    }
}
