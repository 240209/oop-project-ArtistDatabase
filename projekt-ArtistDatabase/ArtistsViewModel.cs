using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using projekt_ArtistDatabase;
using System.Threading.Tasks;
using projekt_ArtistDatabase.EFCore;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace projekt_ArtistDatabase
{
    public class ArtistsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Artist> _artistsOutput;
        public ObservableCollection<Artist> ArtistsOutput
        {
            get { return _artistsOutput; }
            set
            {
                _artistsOutput = value;
                OnPropertyChanged(nameof(ArtistsOutput));
            }
        }


        #region Selected Artist Handling
        public bool IsArtistSelected { get; set; }
        public bool hasSelectedArtistAlbums { get; set; }
        public string SelectedArtistName { get; set; }
        public string SelectedArtistGenres { get; set; }
        public ObservableCollection<Album> SelectedArtistAlbums { get; set; }

        private Artist _selectedArtist;
        public Artist SelectedArtist
        {
            get
            {
                return _selectedArtist;
            }
            set
            {
                _selectedArtist = value;

                if(_selectedArtist != null)
                {
                    if (_selectedArtist.Albums.Any())
                    {
                        SelectedArtistAlbums = new ObservableCollection<Album>(_selectedArtist.Albums);
                        hasSelectedArtistAlbums = true;
                    }
                    else
                    {
                        SelectedArtistAlbums = new ObservableCollection<Album>();
                        hasSelectedArtistAlbums = false;
                    }


                    if (_selectedArtist.Genres.Any())
                    {
                        StringBuilder artistGenres = new();
                        foreach (var genre in _selectedArtist.Genres)
                        {
                            artistGenres.Append(genre.Name);
                            artistGenres.Append(", ");
                        }
                        artistGenres.Remove(artistGenres.Length - 2, 2);
                        SelectedArtistGenres = artistGenres.ToString();
                    }
                    else
                    {
                        SelectedArtistGenres = "Add the first genre!";
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
            }
        } 
        #endregion



        /// <summary>
        /// database context variable initialised in constructor and disposed in destructor
        /// </summary>
        private ArtistContext _context;

        public ArtistsViewModel(ArtistContext context)
        {
            //_selectedArtist = null;
            _context = context;
            ArtistsOutput = new ObservableCollection<Artist>(_context.Artists.ToList());
            IsArtistSelected = false;
        }

        #region PropertyChanged Handling
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion

        #region ICommands
        public ICommand SearchArtists { get; }
        public ICommand AddArtist { get; }
        public ICommand DeleteArtist { get; }
        public ICommand EditArtistName { get; }
        public ICommand EditArtistGenres { get; }
        public ICommand EditArtistAlbums { get; }
        public ICommand ImportCsv { get; }
        public ICommand ExportCsv { get; } 
        #endregion
    }
}
