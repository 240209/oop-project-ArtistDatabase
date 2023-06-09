﻿using projekt_ArtistDatabase.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace projekt_ArtistDatabase.Commands
{
    public class OpenEditAlbumCommand : CommandBase
    {
        private readonly ArtistsViewModel _artistsViewModel;
        private readonly NavigationStore _navigationStore;
        public OpenEditAlbumCommand(ArtistsViewModel artistsViewModel, NavigationStore navigationStore)
        {
            _artistsViewModel = artistsViewModel;
            _navigationStore = navigationStore;

            // subscribing to the artistsviewmodel property change to enable the EditAlbum command
            _artistsViewModel.PropertyChanged += ArtistViewModel_PropertyChanged;
        }

        private void ArtistViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            // check if the changed property is IsArtistAlbumSelected
            if (e.PropertyName == nameof(_artistsViewModel.IsArtistAlbumSelected))
            {
                // then change the CanExecute to true
                OnCanExecuteChanged();
            }
        }
        public override void Execute(object? parameter)
        {
            CloseModalCommand cancelCommand = new(_navigationStore);
            EditAlbumCommand submitCommand = new(_artistsViewModel.SelectedArtist, _artistsViewModel.SelectedArtistAlbum, _navigationStore);

            EditAlbumViewModel editAlbumViewModel = new(_artistsViewModel.SelectedArtistAlbum, cancelCommand, submitCommand);

            submitCommand.EditAlbumViewModel = editAlbumViewModel;

            editAlbumViewModel.PropertyChanged += submitCommand.validateData;

            _navigationStore.CurrentViewModel = editAlbumViewModel;
        }
        public override bool CanExecute(object? parameter)
        {
            return _artistsViewModel.IsArtistAlbumSelected && base.CanExecute(parameter);
        }
    }
}
