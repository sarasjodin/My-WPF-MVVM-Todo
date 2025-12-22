using System.Windows;
using WPF_MVVM_ToDo_Project.ViewModels;

namespace WPF_MVVM_ToDo_Project
{
    /// <summary>
    /// Main application window
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Set the MainViewModel as DataContext for data binding
            DataContext = new MainViewModel();
        }
    }
}