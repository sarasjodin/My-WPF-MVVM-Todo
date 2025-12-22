namespace WPF_MVVM_ToDo_Project.Models
{
    /// <summary>
    /// Represents a single activity item in the application.
    /// Contains only data, no UI or logic.
    /// </summary>

    public class ToDoItem
    {
        // The Activity Title
        public string Title { get; set; }

        // If activity is completed
        public bool IsCompleted { get; set; }

        // Date and time when activity was created
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
