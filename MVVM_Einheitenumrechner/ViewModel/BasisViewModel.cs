using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Einheitenumrechner.ViewModel
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    namespace UnitConverter.ViewModels
    {
        public class BaseViewModel : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            // Diese Methode benachrichtigt das View, dass sich eine Property geändert hat
            protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            // Praktische SetProperty-Methode: prüft auf Änderung, setzt Wert, benachrichtigt
            protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
            {
                if (Equals(field, value))
                    return false;

                field = value;
                OnPropertyChanged(propertyName);
                return true;
            }
        }
    }

}
