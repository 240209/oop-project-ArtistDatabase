using projekt_ArtistDatabase.EFCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace projekt_ArtistDatabase.ViewModels
{
    public class ArtistAlbumsEditViewModel : ViewModelBase
    {
        private ObservableCollection<Album> _albums;
        public ObservableCollection<Album> Albums
        {
            get => _albums;
            set
            {
                _albums = value;
                OnPropertyChanged(nameof(Albums));
            }
        }
        public bool hasAlbums => Albums.Count > 0;

        public ArtistAlbumsEditViewModel()
        {
            Albums = new ObservableCollection<Album>();
        }

        public ICommand CancelCommand { get; set; }
        public ICommand SubmitCommand { get; set; }
        public ICommand NewAlbum { get; set; }
        public ICommand RemoveAlbum { get; set; }
        public ICommand EditAlbum { get; set; }
    }
}
