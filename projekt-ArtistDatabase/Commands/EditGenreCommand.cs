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
    public class EditGenreCommand : AsyncCommandBase
    {
        private readonly NavigationStore _navigationStore;
        public EditGenreViewModel editGenreViewModel;
        private readonly Artist _artistToBeUpdated;
        private readonly Genre _genreToBeUpdated;

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

        public EditGenreCommand(Artist updatedArtist, Genre updatedGenre , NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _artistToBeUpdated = updatedArtist;
            _genreToBeUpdated = updatedGenre;
            dataValidated = true;
            OnCanExecuteChanged();
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Genre newGenre = new();
            newGenre.Name = editGenreViewModel.Name;

            _navigationStore.Close();

            if (DatabaseHandler.UpdateRecord(_genreToBeUpdated, newGenre))
            {
                MessageBox.Show("Genre edited succesfully.");
                App.context.SaveChanges();
                // notifying UI that the artist has to be refreshed
                App.context.Entry(_artistToBeUpdated).State = EntityState.Modified;
            }
            else
            {
                MessageBox.Show("Genre edit failed.");
            }
        }
        public override bool CanExecute(object? parameter)
        {
            return dataValidated && base.CanExecute(parameter);
        }

        public void validateData(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(editGenreViewModel.Name))
            {
                dataValidated = editGenreViewModel.Name != string.Empty;
            }
        }
    }
}
