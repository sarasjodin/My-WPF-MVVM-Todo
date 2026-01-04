using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_MVVM_ToDo_Project.ViewModels
{
    // BaseViewModel implementation provided as part of the WPF/MVVM course
    // "Informatik med inriktning systemutveckling" Östersund
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(field, value)) return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
