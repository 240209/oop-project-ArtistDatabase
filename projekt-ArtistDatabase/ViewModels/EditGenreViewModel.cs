﻿using projekt_ArtistDatabase.EFCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace projekt_ArtistDatabase.ViewModels
{
    public class EditGenreViewModel : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public ICommand CancelCommand { get; }

        public ICommand SubmitCommand { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="genre">Reference to the genre to be edited (Id not required)</param>
        /// <param name="cancelCommand"></param>
        /// <param name="submitCommand"></param>
        public EditGenreViewModel(Genre genre, ICommand cancelCommand, ICommand submitCommand)
        {
            Name = genre.Name;
            CancelCommand = cancelCommand;
            SubmitCommand = submitCommand;
        }
    }
}
