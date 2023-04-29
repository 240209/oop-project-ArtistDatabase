using projekt_ArtistDatabase.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace projekt_ArtistDatabase.Commands
{
    public class OpenNewAlbumCommand : CommandBase
    {
        private readonly ArtistsViewModel _artistsViewModel;

        private readonly NavigationStore _navigationStore;
        public OpenNewAlbumCommand(ArtistsViewModel artistsViewModel, NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _artistsViewModel = artistsViewModel;
        }
        public override void Execute(object? parameter)
        {
            CloseModalCommand cancelCommand = new(_navigationStore);
            NewAlbumCommand submitCommand = new(_artistsViewModel.SelectedArtist, _navigationStore);
            NewAlbumViewModel newAlbumViewModel = new(cancelCommand, submitCommand);

            submitCommand.NewAlbumViewModel = newAlbumViewModel;

            _navigationStore.CurrentViewModel = newAlbumViewModel;
        }
    }
}
