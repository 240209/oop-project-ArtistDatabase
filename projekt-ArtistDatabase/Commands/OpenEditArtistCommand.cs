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
        private readonly Artist _artist;
        private readonly NavigationStore _navigationStore;
        public OpenEditArtistCommand(Artist artist, NavigationStore navigationStore)
        {
            _artist = artist;
            _navigationStore = navigationStore;

        }
        public override void Execute(object? parameter)
        {
            ICommand cancelCommand = new CloseModalCommand(_navigationStore);
            EditArtistViewModel editArtistViewModel = new(_artist, cancelCommand, null);
            _navigationStore.CurrentViewModel = editArtistViewModel;
        }
    }
}
