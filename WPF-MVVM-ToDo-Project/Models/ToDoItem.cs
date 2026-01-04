using System.ComponentModel;

namespace WPF_MVVM_ToDo_Project.Models
{
    /// <summary>
    /// Represents a single activity item in the application.
    /// Contains only data, no UI or logic.
    /// </summary>

    public class ToDoItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // The Activity Title
        // Only saved through Edit view "Save"
        public string Title { get; set; } = "";

        // If activity is completed
        private bool _isCompleted;
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                if (_isCompleted == value) return;
                _isCompleted = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCompleted)));
            }
        }

        // Date and time when activity was created
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
