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
    public class NewGenreCommand : AsyncCommandBase
    {
        private readonly NavigationStore _navigationStore;
        public NewGenreViewModel NewGenreViewModel;
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

        public NewGenreCommand(Artist updatedArtist, NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _artistToBeUpdated = updatedArtist;
            dataValidated = false;
            OnCanExecuteChanged();
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Genre genre = new();
            genre.Name = NewGenreViewModel.Name;


            if (!DatabaseHandler.Contains(genre) && DatabaseHandler.InsertRecord(genre))
            {
                // if genre doesn't yet exist
                MessageBox.Show("Genre added succesfully.");
                _artistToBeUpdated.Genres.Add(genre);
                App.context.SaveChanges();
                // notifying UI that the artist has to be refreshed
                App.context.Entry(_artistToBeUpdated).State = EntityState.Modified;
            }
            else if (DatabaseHandler.Contains(genre))
            {
                // if genre exists already, it links the existing genre instead of creating the same one
                _artistToBeUpdated.Genres.Add(App.context.Genres.Where(i => i.Name == genre.Name).First());
                App.context.SaveChanges();
                // notifying UI that the artist has to be refreshed
                App.context.Entry(_artistToBeUpdated).State = EntityState.Modified;
                MessageBox.Show("New genre linked to existing genre with the same name.");
            }
            else
            {
                MessageBox.Show("Genre addition failed.");
                return;
            }


            _navigationStore.Close();
        }
        public override bool CanExecute(object? parameter)
        {
            return dataValidated && base.CanExecute(parameter);
        }

        public void validateData(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(NewGenreViewModel.Name))
            {
                dataValidated = NewGenreViewModel.Name != string.Empty;
            }
        }
    }
}
