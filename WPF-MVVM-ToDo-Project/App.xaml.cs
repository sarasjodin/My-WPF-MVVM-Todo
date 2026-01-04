using System.Windows;
using WPF_MVVM_ToDo_Project.Data;
using WPF_MVVM_ToDo_Project.ViewModels;

namespace WPF_MVVM_ToDo_Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IToDoRepository repository = new JsonToDoRepository();
            var vm = new MainViewModel(repository); // MainVM saves _repository and loads Todos

            var window = new MainWindow
            {
                DataContext = vm
            };
            MainWindow = window;
            window.Show();
        }
    }
}
