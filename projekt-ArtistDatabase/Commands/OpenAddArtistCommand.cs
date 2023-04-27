using projekt_ArtistDatabase.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace projekt_ArtistDatabase.Commands
{
    public class OpenAddArtistCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        public OpenAddArtistCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

        }
        public override void Execute(object? parameter)
        {
            ICommand cancelCommand = new CloseModalCommand(_navigationStore);
            NewArtistViewModel newArtistViewModel = new(cancelCommand, null);
            _navigationStore.CurrentViewModel = newArtistViewModel;
        }
    }
}
