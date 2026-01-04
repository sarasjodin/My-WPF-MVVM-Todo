using System.Windows.Input;
using WPF_MVVM_ToDo_Project.Commands;
using WPF_MVVM_ToDo_Project.Models;

namespace WPF_MVVM_ToDo_Project.ViewModels
{
    /// <summary>
    /// ViewModel for the Add activity view.
    /// Handles user input and add action logic.
    /// </summary>

    public class AddTodoViewModel : BaseViewModel
    {
        // Reference to the main ViewModel to access shared data
        private readonly MainViewModel _mainVm;

        // Name of the new activity entered by the user
        private string _newActivityTitle = "";
        public string NewActivityTitle
        {
            get => _newActivityTitle;
            set => SetProperty(ref _newActivityTitle, value);
        }

        // Info message shown to the user
        private string _statusMessage = "";
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        // Command executed when the user clicks the Add button
        public ICommand AddCommand { get; }

        public AddTodoViewModel(MainViewModel mainVm)
        {
            _mainVm = mainVm;

            // Defines what happens when the Add button is clicked
            AddCommand = new RelayCommand(async _ =>
            {
                if (string.IsNullOrWhiteSpace(NewActivityTitle))
                    return;

                // Each new activity combined with its creation date
                _mainVm.Todos.Add(new ToDoItem
                {
                    Title = NewActivityTitle,
                    CreatedAt = DateTime.Now
                });

                // Show info message
                StatusMessage = $"\"{NewActivityTitle}\" added to the list.";

                // Remove text after activity has been added
                NewActivityTitle = "";

                // Remove StatusMessage after 2 seconds
                // Async delay
                await Task.Delay(2000);
                StatusMessage = "";
            });
        }
    }
}