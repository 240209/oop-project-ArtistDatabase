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
    public class EditAlbumCommand : AsyncCommandBase
    {
        private readonly NavigationStore _navigationStore;
        public EditAlbumViewModel EditAlbumViewModel;
        private readonly Artist _artistToBeUpdated;
        private readonly Album _albumToBeUpdated;

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

        public EditAlbumCommand(Artist updatedArtist, Album updatedAlbum, NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _artistToBeUpdated = updatedArtist;
            _albumToBeUpdated = updatedAlbum;
            dataValidated = true;
            OnCanExecuteChanged();
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Album newAlbum = new();
            newAlbum.Name = EditAlbumViewModel.Name;
            newAlbum.Year = EditAlbumViewModel.Year;

            if (DatabaseHandler.UpdateRecord(_albumToBeUpdated, newAlbum))
            {
                MessageBox.Show("Album edited succesfully.");
                App.context.SaveChanges();
                // notifying UI that the artist has to be refreshed
                App.context.Entry(_artistToBeUpdated).State = EntityState.Modified;
            }
            else
            {
                MessageBox.Show("Album edit failed.");
            }

            _navigationStore.Close();
        }
        public override bool CanExecute(object? parameter)
        {
            return dataValidated && base.CanExecute(parameter);
        }

        public void validateData(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(EditAlbumViewModel.Name) || e.PropertyName == nameof(EditAlbumViewModel.Year))
            {
                if (EditAlbumViewModel.Year > 0
                    && EditAlbumViewModel.Year < 2100
                    && EditAlbumViewModel.Name != string.Empty)
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
