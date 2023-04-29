using projekt_ArtistDatabase.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace projekt_ArtistDatabase.Commands
{
    public class OpenNewGenreCommand : CommandBase
    {
        private readonly ArtistsViewModel _artistsViewModel;
        private readonly NavigationStore _navigationStore;
        public OpenNewGenreCommand(ArtistsViewModel artistsViewModel, NavigationStore navigationStore)
        {
            _artistsViewModel = artistsViewModel;
            _navigationStore = navigationStore;
        }
        public override void Execute(object? parameter)
        {
            CloseModalCommand cancelCommand = new(_navigationStore);
            NewGenreCommand submitCommand = new(_artistsViewModel.SelectedArtist, _navigationStore);
            NewGenreViewModel newGenreViewModel = new(cancelCommand, submitCommand);

            submitCommand.NewGenreViewModel = newGenreViewModel;

            _navigationStore.CurrentViewModel = newGenreViewModel;
        }
    }
}
