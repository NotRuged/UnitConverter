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
    /// Interaktionslogik für TestView.xaml
    /// </summary>
    public partial class TestView : Window
    {
        public TestView()
        {
            InitializeComponent();
        }

        private void ShowHistory_Click(object sender, RoutedEventArgs e)
        {
            var historyWindow = new HistoryView();
            historyWindow.Show();
            this.Close();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingView();
            settingsWindow.Show();
            this.Close();
        }

    }
}
