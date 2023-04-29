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
    public class EditArtistCommand : AsyncCommandBase
    {
        private readonly NavigationStore _navigationStore;
        public EditArtistViewModel EditArtistViewModel;
        public Artist oldArtist;

        public EditArtistCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Artist newArtist = new Artist();
            newArtist.Name = EditArtistViewModel.Name;

            _navigationStore.Close();

            if (DatabaseHandler.UpdateRecord(oldArtist, newArtist))
            {
                MessageBox.Show("Artist updated succesfully.");
                try
                {
                    App.context.SaveChanges();
                }
                catch   (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Artist update failed.");
            }
        }
    }
}
