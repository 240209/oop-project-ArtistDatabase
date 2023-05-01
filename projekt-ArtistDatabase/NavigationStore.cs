using projekt_ArtistDatabase.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_ArtistDatabase
{
    /// <summary>
    /// Handles operation with modals - displaying, closing, tracking.
    /// </summary>
    public class NavigationStore
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                CurrentViewModelChanged?.Invoke();

            }
        }

        public bool IsOpen => CurrentViewModel != null;

        public event Action CurrentViewModelChanged;
        /// <summary>
        /// Closes current modal
        /// </summary>
        public void Close()
        {
            CurrentViewModel = null;
        }
    }
}
