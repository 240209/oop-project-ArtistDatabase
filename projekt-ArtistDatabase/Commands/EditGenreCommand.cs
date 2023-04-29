using Microsoft.EntityFrameworkCore;
using projekt_ArtistDatabase.EFCore;
using projekt_ArtistDatabase.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projekt_ArtistDatabase.Commands
{
    public class EditGenreCommand : AsyncCommandBase
    {
        private readonly NavigationStore _navigationStore;
        public EditGenreViewModel EditGenreViewModel;
        private readonly Artist _artistToBeUpdated;
        private readonly Genre _genreToBeUpdated;

        public EditGenreCommand(Artist updatedArtist, Genre updatedGenre , NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _artistToBeUpdated = updatedArtist;
            _genreToBeUpdated = updatedGenre;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Genre newGenre = new();
            newGenre.Name = EditGenreViewModel.Name;

            _navigationStore.Close();

            if (DatabaseHandler.UpdateRecord(_genreToBeUpdated, newGenre))
            {
                MessageBox.Show("Genre edited succesfully.");
                App.context.SaveChanges();
                App.context.Entry(_artistToBeUpdated).State = EntityState.Modified;
            }
            else
            {
                MessageBox.Show("Genre edit failed.");
            }
        }
    }
}
