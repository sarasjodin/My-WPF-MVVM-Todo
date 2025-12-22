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

        // Stores the name of the new activity entered by the user
        private string _newActivityTitle;
        public string NewActivityTitle
        { get; set; }

        // Command executed when the user clicks the Add button
        public ICommand AddCommand { get; }

        public AddTodoViewModel(MainViewModel mainVm)
        {
            _mainVm = mainVm;

            // Defines what happens when the Add button is clicked
            AddCommand = new RelayCommand(_ =>
            {
                if (string.IsNullOrWhiteSpace(NewActivityTitle))
                    return;

                _mainVm.Todos.Add(new ToDoItem
                {
                    Title = NewActivityTitle,
                    CreatedAt = DateTime.Now
                });

                NewActivityTitle = "";

                // Navigate back to activity list after add
                // _mainVm.CurrentView = _mainVm.TodoListVm;
            });
        }
    }
}