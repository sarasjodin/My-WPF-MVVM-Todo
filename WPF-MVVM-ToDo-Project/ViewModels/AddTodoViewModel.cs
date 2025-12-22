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
        public string NewActivityTitle
        { get; set; }

        // Info message shown to the user
        public string StatusMessage
        { get; set; }

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

                _mainVm.Todos.Add(new ToDoItem
                {
                    Title = NewActivityTitle,
                    CreatedAt = DateTime.Now
                });

                // Show info message
                StatusMessage = "Activity added to the list";

                // Remove text after activity has been added
                NewActivityTitle = "";

                // Remove StatusMessage after 2 seconds
                await Task.Delay(2000);
                StatusMessage = "";
            });
        }
    }
}