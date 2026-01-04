using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
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
        public ObservableCollection<ToDoItem> Todos { get; } = new();

        // Readonly to use a single repository instance
        // Implementation of Interface repo "contract"
        private readonly IToDoRepository _repository;

        // Holds the currently active ViewModel which determines the actual view to display
        // To avoid nullable warning. Should never be null
        private BaseViewModel _currentView = null!;
        public BaseViewModel CurrentView
        {
            get => _currentView;
            set
            {
                if (SetProperty(ref _currentView, value))
                    OnCurrentViewChanged();
            }
        }

        // Commands used to switch between views
        public ICommand ShowTodoListCommand { get; }
        public ICommand ShowAddTodoCommand { get; }

        // Reused ViewModel instances for navigation
        public TodoListViewModel TodoListVm { get; }
        public AddTodoViewModel AddTodoVm { get; }
        // Created if editing a Todo item
        public EditTodoViewModel? EditTodoVm { get; private set; }


        public MainViewModel(IToDoRepository repository)
        {
            _repository = repository;

            // Load saved todos when the application starts
            // Clear and add the existing list instead of creating a new one
            var loaded = _repository.Load();

            Todos.Clear();
            foreach (var t in loaded)
                Todos.Add(t);


            // Autosaves when IsCompleted changes through checkbox in list view
            foreach (var todo in Todos)
                todo.PropertyChanged += Todo_PropertyChanged;

            // autosave when list changes (Add/Remove)
            Todos.CollectionChanged += Todos_CollectionChanged;

            // Create ViewModels for each page (once, are being reused)
            TodoListVm = new TodoListViewModel(this, Todos);
            AddTodoVm = new AddTodoViewModel(this);

            // Navigation commands
            ShowTodoListCommand = new RelayCommand(_ =>
            {
                // Clear selected item when returning to list
                TodoListVm.ClearSelection();
                CurrentView = TodoListVm;
            });

            ShowAddTodoCommand = new RelayCommand(_ => CurrentView = AddTodoVm);

            // Set start view (below navigation) thorugh property
            CurrentView = TodoListVm;
        }

        // Implementation of Save when ObservableCollection changes
        private void Todos_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            // New items (e.NewItems), autosave on IsCompleted for newly added todos
            if (e.NewItems != null)
            {
                foreach (ToDoItem item in e.NewItems)
                    // autosaves for new activities
                    item.PropertyChanged += Todo_PropertyChanged;
            }

            // Removed items (e.OldItems)
            if (e.OldItems != null)
            {
                foreach (ToDoItem item in e.OldItems)
                    // removes listeners on old items when activity is deleted
                    item.PropertyChanged -= Todo_PropertyChanged;
            }

            // Save on Add/Delete
            _repository.Save(Todos);
        }

        private void Todo_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            // e.PropertyName = which property on the TodoItem was changed
            // Autosave only for checkbox toggling
            if (e.PropertyName == nameof(ToDoItem.IsCompleted))
                _repository.Save(Todos);
        }

        // Navigates to edit view for a certain selected todo item
        // Not in constructor = new VM each time
        public void StartEditSelectedTodo(ToDoItem item)
        {
            EditTodoVm = new EditTodoViewModel(this, item);
            CurrentView = EditTodoVm;
        }

        // Saving Todos
        public void SaveTodos()
        {
            _repository.Save(Todos);
        }

        // Clear any previous selection when navigating back to the list view 
        private void OnCurrentViewChanged()
        {
            if (CurrentView == TodoListVm)
            {
                TodoListVm.ClearSelection();
            }
        }
    }
}