using MVVM_Einheitenumrechner.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MVVM_Einheitenumrechner.Views
{
    /// <summary>
    /// Interaktionslogik für SettingView.xaml
    /// </summary>
    public partial class SettingView : Window
    {
        public SettingView()
        {
            InitializeComponent();
            this.DataContext = new SettingViewModel(); // Hier ViewModel zuweisen

        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            TestView testView = new TestView();
            testView.Show();
            this.Close();
        }
    }
}
