using System.Collections.ObjectModel;
using WPF_MVVM_ToDo_Project.Models;

namespace WPF_MVVM_ToDo_Project.Data
{
    /*
     * This is an interface contract that defines needed methods and parameters
     * to allow for easy switch from the current JSON-based solution
     */
    public interface IToDoRepository
    {
        // Contract says to load all saved ToDoItems and return them as one complete ObservableCollection
        ObservableCollection<ToDoItem> Load();

        // Contract says to save the current list of ToDoItems and replace any previously saved data
        void Save(ObservableCollection<ToDoItem> todos);
    }
}
