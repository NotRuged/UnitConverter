using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MVVM_Einheitenumrechner.ViewModel
{
    public class SettingViewModel : INotifyPropertyChanged
    {
        private readonly string connectionString = @"Data Source=DESKTOP-L6EO2E6\MSSQLSERVER01;Trusted_Connection=yes;Database=UnitConverter;Connection Timeout=10;";

        private string _categoryName;
        public string CategoryName
        {
            get => _categoryName;
            set
            {
                _categoryName = value;
                OnPropertyChanged(nameof(CategoryName));
            }
        }

        private string _unit;
        public string Unit
        {
            get => _unit;
            set
            {
                _unit = value;
                OnPropertyChanged(nameof(Unit));
            }
        }

        private decimal _factor;
        public decimal Factor
        {
            get => _factor;
            set
            {
                _factor = value;
                OnPropertyChanged(nameof(Factor));
            }
        }

        private string _messageText;
        public string MessageText
        {
            get => _messageText;
            set
            {
                _messageText = value;
                OnPropertyChanged(nameof(MessageText));
            }
        }

        private Brush _messageColor;
        public Brush MessageColor
        {
            get => _messageColor;
            set
            {
                _messageColor = value;
                OnPropertyChanged(nameof(MessageColor));
            }
        }



        public ICommand SaveCommand { get; }

        public SettingViewModel()
        {
            SaveCommand = new RelayCommand(Save);
        }

        [Obsolete]
        private void Save(object parameter)
        {
            if (string.IsNullOrWhiteSpace(CategoryName) || string.IsNullOrWhiteSpace(Unit) || Factor == 0)
            {
                MessageText = "Bitte alle Felder korrekt ausfüllen.";
                MessageColor = Brushes.Red;
                return;
            }

            try
            {
                int categoryId;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Kategorie holen oder einfügen
                    string categoryQuery = "SELECT CategoryID FROM Categories WHERE CategoryName = @name";
                    using (SqlCommand cmd = new SqlCommand(categoryQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", CategoryName);
                        var result = cmd.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int id))
                        {
                            categoryId = id;
                        }
                        else
                        {
                            string insertCategory = "INSERT INTO Categories (CategoryName) VALUES (@name); SELECT CAST(SCOPE_IDENTITY() AS int);";
                            using (SqlCommand insertCmd = new SqlCommand(insertCategory, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@name", CategoryName);
                                var insertedId = insertCmd.ExecuteScalar();
                                if (insertedId == null || !int.TryParse(insertedId.ToString(), out categoryId))
                                {
                                    MessageText = "Fehler beim Einfügen der Kategorie.";
                                    MessageColor = Brushes.Red;
                                    return;
                                }
                            }
                        }
                    }

                    // Einheit einfügen
                    string insertUnit = "INSERT INTO UnitDefinitions (CategoryID, Unit, Factor) VALUES (@catId, @unit, @factor)";
                    using (SqlCommand cmd = new SqlCommand(insertUnit, conn))
                    {
                        cmd.Parameters.AddWithValue("@catId", categoryId);
                        cmd.Parameters.AddWithValue("@unit", Unit);
                        cmd.Parameters.AddWithValue("@factor", Factor);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageText = "Einheit erfolgreich gespeichert!";
                MessageColor = Brushes.Green;

                // Felder zurücksetzen
                CategoryName = string.Empty;
                Unit = string.Empty;
                Factor = 0;
            }
            catch (Exception ex)
            {
                MessageText = $"Fehler beim Speichern: {ex.Message}";
                MessageColor = Brushes.Red;
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;

        public RelayCommand(Action<object> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _execute(parameter);

        public event EventHandler CanExecuteChanged { add { } remove { } }
    }
}