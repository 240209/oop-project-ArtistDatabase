using projekt_ArtistDatabase.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projekt_ArtistDatabase.Commands
{
    public class RemoveArtistCommand : AsyncCommandBase
    {
        private readonly ArtistsViewModel _artistViewModel;

        public RemoveArtistCommand(ArtistsViewModel artistsViewModel)
        {
            _artistViewModel = artistsViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            var selectedArtist = _artistViewModel.SelectedArtist;
            if (DatabaseHandler.DeleteRecord(selectedArtist) != null)
            {
                App.context.SaveChanges();
                _artistViewModel.ArtistsOutput.Remove(selectedArtist);
                MessageBox.Show("Artist deleted successfuly.");
            }
            else
            {
                MessageBox.Show("Artist cannot be deleted.");
            }
        }
    }
}
