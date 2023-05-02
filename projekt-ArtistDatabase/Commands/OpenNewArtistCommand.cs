using projekt_ArtistDatabase.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace projekt_ArtistDatabase.Commands
{
    public class OpenNewArtistCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        public OpenNewArtistCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }
        public override void Execute(object? parameter)
        {
            CloseModalCommand cancelCommand = new(_navigationStore);
            NewArtistCommand submitCommand = new(_navigationStore);

            NewArtistViewModel newArtistViewModel = new(cancelCommand, submitCommand);
            submitCommand.NewArtistViewModel = newArtistViewModel;
            submitCommand.NewArtistViewModel = newArtistViewModel;
            newArtistViewModel.PropertyChanged += submitCommand.validateData;

            _navigationStore.CurrentViewModel = newArtistViewModel;
        }
    }
}
