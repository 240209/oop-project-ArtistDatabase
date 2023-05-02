using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
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
    public class NewArtistCommand : AsyncCommandBase
    {
        private readonly NavigationStore _navigationStore;
        public NewArtistViewModel NewArtistViewModel;

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

        public NewArtistCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            dataValidated = false;
            OnCanExecuteChanged();
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Artist artist = new Artist();
            artist.Name = NewArtistViewModel.Name;

            if (DatabaseHandler.InsertRecord(artist))
            {
                MessageBox.Show("Artist added succesfully.");
                App.context.SaveChanges();
            }
            else
            {
                MessageBox.Show("Artist addition failed.");
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
            if (e.PropertyName == nameof(NewArtistViewModel.Name))
            {
                dataValidated = NewArtistViewModel.Name != string.Empty;
            }
        }
    }
}
