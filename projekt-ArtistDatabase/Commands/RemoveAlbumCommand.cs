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
    public class RemoveAlbumCommand : AsyncCommandBase
    {
        private readonly ArtistsViewModel _artistViewModel;

        public RemoveAlbumCommand(ArtistsViewModel artistsViewModel)
        {
            _artistViewModel = artistsViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            var selectedAlbum = _artistViewModel.SelectedArtistAlbum;
            if (DatabaseHandler.DeleteRecord(selectedAlbum) != null)
            {
                App.context.SaveChanges();
                _artistViewModel.SelectedArtistAlbums.Remove(selectedAlbum);
                MessageBox.Show("Album deleted successfuly.");
            }
            else
            {
                MessageBox.Show("Album cannot be deleted.");
            }
        }
    }
}
