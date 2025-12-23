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
        // Makes list visible on "list view"
        public ObservableCollection<ToDoItem> Todos { get; }

        // Add list logic to utilize select of a todo item (if any)
        public ToDoItem? SelectedTodo { get; set; }

        // Add command to delete a todo item
        public ICommand DeleteCommand { get; }

        public TodoListViewModel(ObservableCollection<ToDoItem> todos)
        {
            Todos = todos;

            DeleteCommand = new RelayCommand(
                execute: _ => DeleteSelected(),
                canExecute: _ => SelectedTodo != null
            );
        }

        private void DeleteSelected()
        {
            if (SelectedTodo == null) return;

            Todos.Remove(SelectedTodo);   // trigger CollectionChanged from MainViewModel
            SelectedTodo = null;          // clear selection afterwards
        }

        // TODO: Add list logic: filtering
    }
}