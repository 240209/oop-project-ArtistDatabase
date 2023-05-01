using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_ArtistDatabase.Commands
{
    /// <summary>
    /// Abstract logic for all the async commands running
    /// </summary>
    public abstract class AsyncCommandBase : CommandBase
    {
        private bool _isExecuting;
        /// <summary>
        /// Serves for disabling multiple Submits when the task is being solved
        /// </summary>
        public bool IsExecuting
        {
            get => _isExecuting;
            set
            {
                _isExecuting = value;
                OnCanExecuteChanged();
            }
        }
        // default CanExecute prop
        public override bool CanExecute(object? parameter)
        {
            return !IsExecuting && base.CanExecute(parameter);
        }
        // eating thrown exceptions so that the app doesn't crash - should be handled inside the async methods themself
        public override async void Execute(object? parameter)
        {
            IsExecuting = true;

            try
            {
                await ExecuteAsync(parameter);
            }

            catch (Exception) { }

            finally
            {
                IsExecuting = false;
            }
        }
        public abstract Task ExecuteAsync(object? parameter);
    }
}
