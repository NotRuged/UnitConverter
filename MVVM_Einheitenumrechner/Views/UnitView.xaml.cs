using System.Windows;
using System.Windows.Controls;

namespace MVVM_Einheitenumrechner.Views
{
    /// <summary>
    /// Interaktionslogik für TestView.xaml
    /// </summary>
    public partial class TestView : UserControl
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="TestView"/>-Klasse.
        /// </summary>
        public TestView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event-Handler für den Klick auf den Button "ShowHistory".
        /// Navigiert zur HistoryView im Hauptfenster.
        /// </summary>
        /// <param name="sender">Der Button, der das Event ausgelöst hat.</param>
        /// <param name="e">Event-Argumente.</param>
        private void ShowHistory_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.ShowHistoryView();
        }

        /// <summary>
        /// Event-Handler für den Klick auf den "Edit" Button.
        /// Navigiert zur SettingsView im Hauptfenster.
        /// </summary>
        /// <param name="sender">Der Button, der das Event ausgelöst hat.</param>
        /// <param name="e">Event-Argumente.</param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.ShowSettingsView();
        }
    }
}
