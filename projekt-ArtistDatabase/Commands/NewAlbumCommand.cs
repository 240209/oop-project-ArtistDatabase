using Microsoft.EntityFrameworkCore;
using projekt_ArtistDatabase.EFCore;
using projekt_ArtistDatabase.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projekt_ArtistDatabase.Commands
{
    public class NewAlbumCommand : AsyncCommandBase
    {
        private readonly NavigationStore _navigationStore;
        public NewAlbumViewModel NewAlbumViewModel;
        private readonly Artist _artistToBeUpdated;

        private bool _dataValidated;
        public bool dataValidated
        {
            get => _dataValidated;
            set
            {
                _dataValidated = value;
                OnCanExecuteChanged();
            }
        }

        public NewAlbumCommand(Artist updatedArtist, NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _artistToBeUpdated = updatedArtist;
            dataValidated = false;
            OnCanExecuteChanged();
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Album newAlbum = new();
            newAlbum.Name = NewAlbumViewModel.Name;
            newAlbum.Year = NewAlbumViewModel.Year;
            newAlbum.Artist = _artistToBeUpdated;
            newAlbum.ArtistId = _artistToBeUpdated.Id;

            if (DatabaseHandler.InsertRecord(newAlbum))
            {
                MessageBox.Show("Album added succesfully.");
                App.context.SaveChanges();
                // notifying UI that the artist has to be refreshed
                App.context.Entry(_artistToBeUpdated).State = EntityState.Modified;
            }
            else
            {
                MessageBox.Show("Album addition failed.");
            }

            _navigationStore.Close();
        }
        public override bool CanExecute(object? parameter)
        {
            return dataValidated && base.CanExecute(parameter);
        }

        public void validateData(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(NewAlbumViewModel.Name) || e.PropertyName == nameof(NewAlbumViewModel.Year))
            {
                if(NewAlbumViewModel.Year > 0
                    && NewAlbumViewModel.Year < 2100
                    && NewAlbumViewModel.Name != string.Empty)
                {
                    dataValidated = true;
                }
                else
                {
                    dataValidated = false;
                }
            }
        }
    }
}
