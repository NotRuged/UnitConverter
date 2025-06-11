using MVVM_Einheitenumrechner.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace MVVM_Einheitenumrechner.Views
{
    /// <summary>
    /// Interaction logic for HistoryView.xaml
    /// </summary>
    public partial class HistoryView : UserControl
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="HistoryView"/>-Klasse.
        /// Setzt den DataContext auf das <see cref="HistoryViewModel"/>.
        /// </summary>
        public HistoryView()
        {
            InitializeComponent();
            DataContext = new HistoryViewModel();
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.SetSliderVisibility(false);
        }

        /// <summary>
        /// Event-Handler für den Klick auf den Zurück-Button.
        /// Wechselt die Ansicht zurück zur UnitView im Hauptfenster.
        /// </summary>
        /// <param name="sender">Der Button, der das Event ausgelöst hat.</param>
        /// <param name="e">Event-Argumente.</param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.ShowUnitView();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
