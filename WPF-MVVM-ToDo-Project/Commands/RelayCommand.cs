using System.Windows.Input;

namespace WPF_MVVM_ToDo_Project.Commands
{

    // RelayCommand implementation provided as part of the WPF/MVVM course
    //"Informatik med inriktning systemutveckling" Östersund
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // Called by WPF to determine whether the command can execute
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        // Called when the command is executed (e.g. on button click)
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        // WPF listens to this event to update the enabled/disabled state of controls
        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
