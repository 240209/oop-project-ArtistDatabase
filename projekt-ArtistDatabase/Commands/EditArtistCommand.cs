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
    public class EditArtistCommand : AsyncCommandBase
    {
        private readonly NavigationStore _navigationStore;
        public EditArtistViewModel EditArtistViewModel;
        public Artist oldArtist;

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

        public EditArtistCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            dataValidated = true;
            OnCanExecuteChanged();
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Artist newArtist = new Artist();
            newArtist.Name = EditArtistViewModel.Name;

            _navigationStore.Close();

            if (DatabaseHandler.UpdateRecord(oldArtist, newArtist))
            {
                MessageBox.Show("Artist updated succesfully.");
                App.context.SaveChanges();
            }
            else
            {
                MessageBox.Show("Artist update failed.");
            }
        }
        public override bool CanExecute(object? parameter)
        {
            return dataValidated && base.CanExecute(parameter);
        }

        public void validateData(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(EditArtistViewModel.Name))
            {
                dataValidated = EditArtistViewModel.Name != string.Empty;
            }
        }
    }
}
