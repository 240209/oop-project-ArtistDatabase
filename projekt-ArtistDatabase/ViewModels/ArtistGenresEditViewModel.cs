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
    public class ArtistGenresEditViewModel : ViewModelBase
    {
        private ObservableCollection<Genre> _genres;
        public ObservableCollection<Genre> Genres
        {
            get => _genres;
            set
            {
                _genres = value;
                OnPropertyChanged(nameof(Genres));
                OnPropertyChanged(nameof(hasGenres));
            }
        }
        private Genre _selectedGenre;
        public Genre SelectedGenre
        {
            get => _selectedGenre;
            set
            {
                _selectedGenre = value;
            }
        }
        public bool hasGenres => Genres.Count > 0;

        public ArtistGenresEditViewModel()
        {
            Genres = new ObservableCollection<Genre>();
        }

        public ICommand NewGenre { get; }
        public ICommand RemoveGenre { get; }
        public ICommand EditGenre { get; }
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

    }
}
