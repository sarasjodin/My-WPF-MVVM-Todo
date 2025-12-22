using System.Collections.ObjectModel;
using System.Windows.Input;
using WPF_MVVM_ToDo_Project.Commands;
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
        public ObservableCollection<ToDoItem> Todos { get; } = new();

        // Holds the currently active ViewModel
        private BaseViewModel _currentView;
        public BaseViewModel CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        // Commands used to switch between views
        public ICommand ShowTodoListCommand { get; }
        public ICommand ShowAddTodoCommand { get; }

        // Reused ViewModel instances for navigation
        public TodoListViewModel TodoListVm { get; }
        public AddTodoViewModel AddTodoVm { get; }

        public MainViewModel()
        {
            // Create ViewModels for each page
            TodoListVm = new TodoListViewModel();
            AddTodoVm = new AddTodoViewModel(this);

            // Navigation commands
            ShowTodoListCommand = new RelayCommand(_ => CurrentView = TodoListVm);
            ShowAddTodoCommand = new RelayCommand(_ => CurrentView = AddTodoVm);

            // Set start page
            CurrentView = TodoListVm;

            // TODO: Load saved activities on startup
            // TODO: Save when ObservableCollection changes
        }
    }
}