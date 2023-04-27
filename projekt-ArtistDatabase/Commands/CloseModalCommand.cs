using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_ArtistDatabase.Commands
{
    public class CloseModalCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;

        public CloseModalCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object? parameter)
        {
            _navigationStore.Close();
        }
    }
}
