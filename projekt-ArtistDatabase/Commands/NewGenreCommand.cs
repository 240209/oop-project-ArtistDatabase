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
    public class NewGenreCommand : AsyncCommandBase
    {
        private readonly NavigationStore _navigationStore;
        public NewGenreViewModel NewGenreViewModel;
        private readonly Artist _artistToBeUpdated;

        public NewGenreCommand(Artist updatedArtist, NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _artistToBeUpdated = updatedArtist;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Genre genre = new();
            genre.Name = NewGenreViewModel.Name;


            if (!DatabaseHandler.Contains(genre) && DatabaseHandler.InsertRecord(genre))
            {
                MessageBox.Show("Genre added succesfully.");
                _artistToBeUpdated.Genres.Add(genre);
                App.context.SaveChanges();
                App.context.Entry(_artistToBeUpdated).State = EntityState.Modified;
            }
            else if (DatabaseHandler.Contains(genre))
            {
                _artistToBeUpdated.Genres.Add(App.context.Genres.Where(i => i.Name == genre.Name).First());
                App.context.SaveChanges();
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
    }
}
