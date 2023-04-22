using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_ArtistDatabase.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public bool IsViewModelOpen => _navigationStore.IsOpen;
        public ArtistsViewModel ArtistsViewModel { get; }

        public MainViewModel(NavigationStore navigationStore, ArtistsViewModel artistsViewModel)
        {
            _navigationStore = navigationStore;
            ArtistsViewModel = artistsViewModel;

            _navigationStore.CurrentViewModelChanged += NavigationStore_CurrentViewModelChanged;
        }

        protected override void Dispose()
        {
            _navigationStore.CurrentViewModelChanged -= NavigationStore_CurrentViewModelChanged;

            base.Dispose();
        }

        private void NavigationStore_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
            OnPropertyChanged(nameof(IsViewModelOpen));
        }
    }
}
