using System.Windows.Input;
using WPF_MVVM_ToDo_Project.Commands;
using WPF_MVVM_ToDo_Project.Models;

namespace WPF_MVVM_ToDo_Project.ViewModels
{
    // ViewModel for editing an existing Todo item
    // Uses a temporary copy to allow canceling without modifying original data
    public class EditTodoViewModel : BaseViewModel
    {
        // Reference to MainViewModel
        private readonly MainViewModel _mainVm;

        // Original Todo item being edited, but not modified until saved
        private readonly ToDoItem _original;

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private bool _isCompleted;
        public bool IsCompleted
        {
            get => _isCompleted;
            set => SetProperty(ref _isCompleted, value);
        }

        // Snave and Cancel commands
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public EditTodoViewModel(MainViewModel mainVm, ToDoItem original)
        {
            _mainVm = mainVm;
            _original = original;

            // Create copies of the changed todo item for safe editing
            Title = original.Title;
            IsCompleted = original.IsCompleted;

            // Commands for user actions
            SaveCommand = new RelayCommand(_ => Save());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        private void Save()
        {
            // copy to original
            _original.Title = Title;
            _original.IsCompleted = IsCompleted;

            // Save immediately
            _mainVm.SaveTodos();

            // Back to list
            _mainVm.CurrentView = _mainVm.TodoListVm;
        }

        private void Cancel()
        {
            // Go back without changing original data
            _mainVm.CurrentView = _mainVm.TodoListVm;
        }
    }
}
