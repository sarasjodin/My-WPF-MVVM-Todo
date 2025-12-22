using System.Collections.ObjectModel;
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

        public TodoListViewModel(ObservableCollection<ToDoItem> todos)
        {
            Todos = todos;
        }

        // TODO: Add list logic: selection
        // TODO: Add list logic: filtering
        // TODO: Add list logic: commands
    }
}