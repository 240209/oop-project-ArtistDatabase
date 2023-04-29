using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projekt_ArtistDatabase.EFCore;
using projekt_ArtistDatabase.ViewModels;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using projekt_ArtistDatabase;
using Microsoft.EntityFrameworkCore.Metadata;
using projekt_ArtistDatabase.Commands;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace projekt_ArtistDatabase.ViewModels
{
    public class ArtistsViewModel : ViewModelBase
    {
        private ObservableCollection<Artist> _artistsOutput;
        public ObservableCollection<Artist> ArtistsOutput
        {
            get => _artistsOutput;
            set
            {
                _artistsOutput = value;
                OnPropertyChanged(nameof(ArtistsOutput));
            }
        }

        private string _searchField;
        public string SearchField
        {
            get => _searchField;
            set
            {
                _searchField = value;
                if(_searchField != string.Empty)
                {
                    ArtistsOutput = new ObservableCollection<Artist>(App.context.Artists.Where(artist => artist.Name.Contains(_searchField)).OrderBy(artist => artist.Name));
                }
                else
                {
                    ArtistsOutput = new ObservableCollection<Artist>(App.context.Artists.OrderBy(artist => artist.Name));
                }
                OnPropertyChanged(nameof(ArtistsOutput));
                OnPropertyChanged(nameof(SearchField));
            }
        }

        #region Selected Artist Handling
        public bool IsArtistSelected { get; set; }
        public bool IsArtistGenreSelected { get; set; }
        public bool IsArtistAlbumSelected { get; set; }
        public bool hasSelectedArtistAlbums { get; set; }
        public bool hasSelectedArtistGenres { get; set; }
        public string SelectedArtistName { get; set; }
        public ObservableCollection<Album> SelectedArtistAlbums { get; set; }
        public ObservableCollection<Genre> SelectedArtistGenres { get; set; }

        private Artist _selectedArtist;
        public Artist SelectedArtist
        {
            get => _selectedArtist;
            set
            {
                _selectedArtist = value;

                if (_selectedArtist != null)
                {
                    if (_selectedArtist.Albums.Any())
                    {
                        SelectedArtistAlbums = new ObservableCollection<Album>(_selectedArtist.Albums.OrderBy(album => album.Name).OrderBy(album => album.Year));
                        hasSelectedArtistAlbums = true;
                    }
                    else
                    {
                        SelectedArtistAlbums = new ObservableCollection<Album>();
                        hasSelectedArtistAlbums = false;
                    }


                    if (_selectedArtist.Genres.Any())
                    {
                        SelectedArtistGenres = new ObservableCollection<Genre>(_selectedArtist.Genres.OrderBy(genre => genre.Name));
                        hasSelectedArtistGenres = true;
                    }
                    else
                    {
                        SelectedArtistGenres = new ObservableCollection<Genre>();
                        hasSelectedArtistGenres = false;
                    }

                    SelectedArtistName = _selectedArtist.Name;
                    IsArtistSelected = true;
                }
                else
                {
                    IsArtistSelected = false;
                }

                OnPropertyChanged(nameof(IsArtistSelected));
                OnPropertyChanged(nameof(SelectedArtist));
                OnPropertyChanged(nameof(SelectedArtistAlbums));
                OnPropertyChanged(nameof(SelectedArtistGenres));
                OnPropertyChanged(nameof(SelectedArtistName));
                OnPropertyChanged(nameof(hasSelectedArtistAlbums));
                OnPropertyChanged(nameof(hasSelectedArtistGenres));
            }
        }

        private Album _selectedArtistAlbum { get; set; }
        public Album SelectedArtistAlbum
        {
            get => _selectedArtistAlbum;
            set
            {
                _selectedArtistAlbum = value;

                IsArtistAlbumSelected = value != null;

                OnPropertyChanged(nameof(SelectedArtistAlbum));
                OnPropertyChanged(nameof(IsArtistAlbumSelected));
            }
        }
        private Genre _selectedArtistGenre { get; set; }
        public Genre SelectedArtistGenre
        {
            get => _selectedArtistGenre;
            set
            {
                _selectedArtistGenre = value;

                IsArtistGenreSelected = value != null;

                OnPropertyChanged(nameof(SelectedArtistGenre));
                OnPropertyChanged(nameof(IsArtistGenreSelected));
            }
        }
        #endregion

        /// <summary>
        /// database context variable initialised in constructor and disposed in destructor
        /// </summary>
        private ArtistContext _context;

        public ArtistsViewModel(ArtistContext context, NavigationStore navigationStore)
        {
            _context = context;

            ArtistsOutput = new ObservableCollection<Artist>(_context.Artists.OrderBy(x => x.Name).ToList());

            IsArtistSelected = false;

            NewArtistCommand = new OpenNewArtistCommand(navigationStore);
            NewArtistAlbumCommand = new OpenNewAlbumCommand(this, navigationStore);
            NewArtistGenreCommand = new OpenNewGenreCommand(this, navigationStore);

            EditArtistCommand = new OpenEditArtistCommand(this, navigationStore);
            EditArtistAlbumCommand = new OpenEditAlbumCommand(this, navigationStore);
            EditArtistGenreCommand = new OpenEditGenreCommand(this, navigationStore);

            RemoveArtistCommand = new RemoveArtistCommand(this);
            RemoveArtistAlbumCommand = new RemoveAlbumCommand(this);
            RemoveArtistGenreCommand = new RemoveGenreCommand(this);

            ExportCsvCommand = new ExportCsvCommand();
            ImportCsvCommand = new ImportCsvCommand();

            ArtistsOutput.CollectionChanged += RefreshData;
        }

        public void RefreshData(object sender, EventArgs args)
        {
            ArtistsOutput = new ObservableCollection<Artist>(_context.Artists.OrderBy(x => x.Name).ToList());
            ArtistsOutput.CollectionChanged += RefreshData;
        }

        #region ICommands
        public ICommand SearchArtistsCommand { get; }
        public ICommand NewArtistCommand { get; }
        public ICommand EditArtistCommand { get; }
        public ICommand RemoveArtistCommand { get; }
        public ICommand NewArtistGenreCommand { get; }
        public ICommand EditArtistGenreCommand { get; }
        public ICommand RemoveArtistGenreCommand { get; }
        public ICommand NewArtistAlbumCommand { get; }
        public ICommand EditArtistAlbumCommand { get; }
        public ICommand RemoveArtistAlbumCommand { get; }
        public ICommand ImportCsvCommand { get; }
        public ICommand ExportCsvCommand { get; }
        #endregion
    }
}
