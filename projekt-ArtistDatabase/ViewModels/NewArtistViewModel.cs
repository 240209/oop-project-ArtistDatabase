using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace projekt_ArtistDatabase.ViewModels
{
    class NewArtistViewModel : ViewModelBase
    {
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public ICommand CancelCommand { get; }

        public ICommand SubmitCommand { get; }
        public NewArtistViewModel(ICommand cancelCommand, ICommand submitCommand)
        {
            CancelCommand = cancelCommand;
            SubmitCommand = submitCommand;
        }
    }
}
