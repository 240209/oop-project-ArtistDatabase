using projekt_ArtistDatabase.EFCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace projekt_ArtistDatabase.ViewModels
{
    public class EditArtistViewModel : ViewModelBase
    {
        public Artist Artist { get; }
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="artist">Reference to the artist to be edited (Id not required)</param>
        /// <param name="cancelCommand"></param>
        /// <param name="submitCommand"></param>
        public EditArtistViewModel(Artist artist, ICommand cancelCommand, ICommand submitCommand)
        {
            CancelCommand = cancelCommand;
            SubmitCommand = submitCommand;
            Name = artist.Name;
            Artist = artist;
        }
    }
}
