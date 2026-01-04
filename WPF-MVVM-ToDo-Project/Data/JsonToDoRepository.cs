using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using WPF_MVVM_ToDo_Project.Models;

namespace WPF_MVVM_ToDo_Project.Data
{  /// <summary>
   /// Handles loading / saving of activities as JSON-file in the user's AppData folder
   /// </summary>
    public class JsonToDoRepository : IToDoRepository
    {
        private readonly string _filePath; // Path to JSON-filen

        public JsonToDoRepository()
        {
            // Documentations of links checked to understand mpre about %LOCALAPPDATA%
            // https://learn.microsoft.com/en-us/windows/apps/design/app-settings/store-and-retrieve-app-data
            // https://learn.microsoft.com/en-us/dotnet/api/system.environment.specialfolder?view=net-10.0
            // https://stackoverflow.com/questions/4128759/environment-variable-for-appdata-local-access-downloaded-custom-assemblies
            // https://www.freecodecamp.org/news/appdata-where-to-find-the-appdata-folder-in-windows-10/
            // https://learn.microsoft.com/en-us/answers/questions/3235887/what-is-localappdataprograms
            // https://gist.github.com/DamianSuess/c143ed869e02e002d252056656aeb9bf
            // https://stackoverflow.com/questions/9709269/difference-between-specialfolder-localapplicationdata-and-specialfolder-appli
            // %LOCALAPPDATA%\WPF_MVVM_ToDo_Project\
            var folderPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "WPF_MVVM_ToDo_Project");

            Directory.CreateDirectory(folderPath);

            _filePath = Path.Combine(folderPath, "todos.json");
        }

        /// <summary>
        /// Loads activities from user's AppData folder
        /// </summary>
        // Sida 518 i studieboken
        // https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/overview
        // https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/deserialization
        // https://stackoverflow.com/questions/31774795/deserialize-json-from-file-in-c-sharp
        public ObservableCollection<ToDoItem> Load()
        {
            if (!File.Exists(_filePath))
                return new ObservableCollection<ToDoItem>(); // File doesn't exist? Return new empty ObservablaCollection<ToDoItem>.

            try
            {
                var json = File.ReadAllText(_filePath); // Read whole file content as string
                return JsonSerializer.Deserialize<ObservableCollection<ToDoItem>>(json) ?? new ObservableCollection<ToDoItem>();
                // Tries to deserialize JSON to ObservablaCollection<ToDoItem>.
                // If loading fails return empty list
            }
            catch
            {
                return new ObservableCollection<ToDoItem>(); // Problem with file? Returns empty collection to make the app start.
            }
        }

        /// <summary>
        /// Saves all activities to user's AppData folder
        /// Overwrites existing data.
        /// </summary>
        public void Save(ObservableCollection<ToDoItem> todos)
        {
            // Sida 519 i studieboken
            // https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/overview
            // https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/how-to
            File.WriteAllText(_filePath, // Write to _filePath - complete file or new
            JsonSerializer.Serialize(
                todos, // Serialize whole list "as is"
                new JsonSerializerOptions { WriteIndented = true })); // Results in better readability
        }
    }
}