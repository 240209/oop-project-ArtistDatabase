using projekt_ArtistDatabase.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projekt_ArtistDatabase.Commands
{
    public class RemoveGenreCommand : AsyncCommandBase
    {
        private readonly ArtistsViewModel _artistViewModel;

        public RemoveGenreCommand(ArtistsViewModel artistsViewModel)
        {
            _artistViewModel = artistsViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            var selectedGenre = _artistViewModel.SelectedArtistGenre;
            if (DatabaseHandler.DeleteRecord(selectedGenre) != null)
            {
                App.context.SaveChanges();
                _artistViewModel.SelectedArtistGenres.Remove(selectedGenre);
                MessageBox.Show("Genre deleted successfuly.");
            }
            else
            {
                MessageBox.Show("Genre cannot be deleted.");
            }
        }
    }
}
