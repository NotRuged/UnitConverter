using MVVM_Einheitenumrechner.Views;
using System.Windows;

namespace MVVM_Einheitenumrechner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Initial View setzen, falls gewünscht
            
        }

        public static int CheckSlideMode = 0;

        private void OpenUnitView_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new TestView();
        }

        public void ShowHistoryView()
        {
            MainContent.Content = new HistoryView();
        }

        public void ShowSettingsView()
        {
            MainContent.Content = new SettingView();
        }

        public void ShowUnitView()
        {
            MainContent.Content = new TestView();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Wert in int umwandeln
            int value = (int)e.NewValue;
            // CheckSlideMode setzen
            CheckSlideMode = value;
            // Optional: weitere Logik, wenn sich der Modus ändert
        }

        public void SetSliderVisibility(bool visible)
        {
            this.DataSlider.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
        }


        public int GetSelectedRepositoryMode()
        {
            return (int)DataSlider.Value;
        }
    }
}
