using System.Collections.ObjectModel;
using System.Windows.Input;
using WPF_MVVM_ToDo_Project.Commands;
using WPF_MVVM_ToDo_Project.Models;

namespace WPF_MVVM_ToDo_Project.ViewModels
{

    /// <summary>
    /// ViewModel for the activity list view.
    /// </summary>

    public class TodoListViewModel : BaseViewModel
    {
        // Reference to MainViewModel
        private readonly MainViewModel _mainVm;

        // Command to edit a todo item
        private readonly RelayCommand _editCommand; // Private "real" command that contains the logic
        public ICommand EditCommand => _editCommand; // Public "interface" command

        // Command to delete a todo item
        private readonly RelayCommand _deleteCommand; // Private "real" command that contains the logic
        public ICommand DeleteCommand => _deleteCommand; // Public "interface" command

        // Makes list visible on "list view"
        public ObservableCollection<ToDoItem> Todos { get; }

        // Currently selected todo item (if an item is selected)
        private ToDoItem? _selectedTodo;
        public ToDoItem? SelectedTodo
        {
            get => _selectedTodo;
            set
            {
                if (_selectedTodo != value)
                {
                    _selectedTodo = value;
                    OnPropertyChanged();

                    _editCommand.RaiseCanExecuteChanged();
                    _deleteCommand.RaiseCanExecuteChanged();
                }
            }
        }

        // Constructor for full functionality (edit and delete)
        public TodoListViewModel(MainViewModel mainVm, ObservableCollection<ToDoItem> todos)
        {
            _mainVm = mainVm;
            Todos = todos;

            // Navigate to edit view for the selected Todo item
            _editCommand = new RelayCommand(
                _ => _mainVm.StartEditSelectedTodo(SelectedTodo!),
                _ => SelectedTodo != null
            );

            // Delete the selected Todo item
            _deleteCommand = new RelayCommand(
               _ => DeleteSelected(),
               _ => SelectedTodo != null
           );
        }

        // Unselect todo item when returning to ToDoList
        public void ClearSelection() => SelectedTodo = null;

        // Delete selected todo item
        private void DeleteSelected()
        {
            if (SelectedTodo == null) return;

            Todos.Remove(SelectedTodo);   // trigger CollectionChanged from MainViewModel
            SelectedTodo = null;          // Clear selection afterwards
        }
    }
}