using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using WPF_MVVM_ToDo_Project.Commands;
using WPF_MVVM_ToDo_Project.Data;
using WPF_MVVM_ToDo_Project.Models;

namespace WPF_MVVM_ToDo_Project.ViewModels
{
    /// <summary>
    /// Main ViewModel that acts as the application shell.
    /// Handles navigation and shared application data.
    /// </summary>

    public class MainViewModel : BaseViewModel
    {

        // Collection that stores all Todo-activities
        public ObservableCollection<ToDoItem> Todos { get; }

        // Readonly to use a single repository instance
        // Implementation of Interface repo "contract"
        private readonly IToDoRepository _repository = new JsonToDoRepository();

        // Holds the currently active ViewModel which determines the actual view to display
        public BaseViewModel CurrentView
        { get; set; }

        // Commands used to switch between views
        public ICommand ShowTodoListCommand { get; }
        public ICommand ShowAddTodoCommand { get; }

        // Reused ViewModel instances for navigation
        public TodoListViewModel TodoListVm { get; }
        public AddTodoViewModel AddTodoVm { get; }

        public MainViewModel()
        {
            // Implementation of Load saved activities on startup
            Todos = _repository.Load() ?? new ObservableCollection<ToDoItem>();

            // autosave när listan ändras (Add/Remove/Clear)
            Todos.CollectionChanged += Todos_CollectionChanged;

            // Create ViewModels for each page
            TodoListVm = new TodoListViewModel(Todos);
            AddTodoVm = new AddTodoViewModel(this);

            // Navigation commands
            ShowTodoListCommand = new RelayCommand(_ => CurrentView = TodoListVm);
            ShowAddTodoCommand = new RelayCommand(_ => CurrentView = AddTodoVm);

            // Set start page
            CurrentView = TodoListVm;
        }

        // Implementation of Save when ObservableCollection changes
        private void Todos_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            _repository.Save(Todos);
        }
    }
}