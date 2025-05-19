using MVVM_Einheitenumrechner.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;


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
        }

        private void OpenUnitView_Click(object sender, RoutedEventArgs e)
        {
            // Erstelle eine Instanz von UnitView und öffne das Fenster
            TestView testView = new TestView();
            testView.Show();  // Fenster wird angezeigt
            this.Close(); 
            // Wenn du möchtest, dass das MainWindow sich schließt:
            // this.Close();
        }

    }
}