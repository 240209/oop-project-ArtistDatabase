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
        /// <summary>
        /// Constructor for Main - includes other views (ArtistsView especially)
        /// </summary>
        /// <param name="navigationStore">stores modal views</param>
        /// <param name="artistsViewModel">needed for commands to have access to selected artist to be edited etc.</param>
        public MainViewModel(NavigationStore navigationStore, ArtistsViewModel artistsViewModel)
        {
            _navigationStore = navigationStore;
            ArtistsViewModel = artistsViewModel;

            // when modal opened/closed - call propertyChanged methods
            _navigationStore.CurrentViewModelChanged += NavigationStore_CurrentViewModelChanged;
        }

        protected override void Dispose()
        {
            // unsubscribe on closing the viewmodel
            _navigationStore.CurrentViewModelChanged -= NavigationStore_CurrentViewModelChanged;

            base.Dispose();
        }
        /// <summary>
        /// showing / closing Modal View
        /// </summary>
        private void NavigationStore_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
            OnPropertyChanged(nameof(IsViewModelOpen));
        }
    }
}
