using projekt_ArtistDatabase.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace projekt_ArtistDatabase.Commands
{
    public class OpenEditAlbumCommand : CommandBase
    {
        private readonly ArtistsViewModel _artistsViewModel;
        private readonly NavigationStore _navigationStore;
        public OpenEditAlbumCommand(ArtistsViewModel artistsViewModel, NavigationStore navigationStore)
        {
            _artistsViewModel = artistsViewModel;
            _navigationStore = navigationStore;
        }
        public override void Execute(object? parameter)
        {
            CloseModalCommand cancelCommand = new(_navigationStore);
            EditAlbumCommand submitCommand = new(_artistsViewModel.SelectedArtist, _artistsViewModel.SelectedArtistAlbum, _navigationStore);

            EditAlbumViewModel editAlbumViewModel = new(_artistsViewModel.SelectedArtistAlbum, cancelCommand, submitCommand);

            submitCommand.EditAlbumViewModel = editAlbumViewModel;

            _navigationStore.CurrentViewModel = editAlbumViewModel;
        }
    }
}
