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
    public class EditAlbumViewModel : ViewModelBase
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
        private int _year;

        public int Year
        {
            get => _year;
            set
            {
                _year = value;
                OnPropertyChanged(nameof(Year));
            }
        }

        public ICommand CancelCommand { get; }

        public ICommand SubmitCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="album">Reference to the album to be edited (Id not required)</param>
        /// <param name="cancelCommand"></param>
        /// <param name="submitCommand"></param>
        public EditAlbumViewModel(Album album, ICommand cancelCommand, ICommand submitCommand)
        {
            CancelCommand = cancelCommand;
            SubmitCommand = submitCommand;
            Name = album.Name;
            Year = album.Year;
        }
    }
}
