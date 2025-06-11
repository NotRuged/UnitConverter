using MVVM_Einheitenumrechner.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace MVVM_Einheitenumrechner.Views
{
    /// <summary>
    /// Interaktionslogik für SettingView.xaml
    /// </summary>
    public partial class SettingView : UserControl
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="SettingView"/>-Klasse.
        /// Setzt den DataContext auf das zugehörige ViewModel <see cref="SettingViewModel"/>.
        /// </summary>
        public SettingView()
        {
            InitializeComponent();
            this.DataContext = new SettingViewModel();
        }

        /// <summary>
        /// Event-Handler für den Klick auf den Zurück-Button.
        /// Navigiert zurück zur UnitView im Hauptfenster.
        /// </summary>
        /// <param name="sender">Der Button, der das Event ausgelöst hat.</param>
        /// <param name="e">Event-Argumente.</param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.ShowUnitView();
        }
    }
}
