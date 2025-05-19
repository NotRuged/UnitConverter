using MVVM_Einheitenumrechner.Class;
using MVVM_Einheitenumrechner.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Xml.Linq;

namespace MVVM_Einheitenumrechner.ViewModel
{
    public class UnitViewModel : INotifyPropertyChanged
    {
        private string connectionString = @"Data Source=DESKTOP-OIR8S4A\SQLEXPRESS;Trusted_Connection=yes;Database=UnitCalculator;Connection Timeout=10;";

        public ObservableCollection<Category> Categories { get; set; } = new();
        public ObservableCollection<UnitDefinition> AvailableUnits { get; set; } = new();

        private Category _selectedCategory;
        private UnitDefinition _fromUnit;
        private UnitDefinition _toUnit;
        private string _inputValue;
        private string _result;

        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
                LoadUnits();
            }
        }

        public UnitDefinition FromUnit
        {
            get => _fromUnit;
            set
            {
                _fromUnit = value;
                OnPropertyChanged();
                ConvertUnits();
            }
        }

        public UnitDefinition ToUnit
        {
            get => _toUnit;
            set
            {
                _toUnit = value;
                OnPropertyChanged();
                ConvertUnits();
            }
        }

        public string InputValue
        {
            get => _inputValue;
            set
            {
                _inputValue = value;
                OnPropertyChanged();
                ConvertUnits();
            }
        }

        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged();
            }
        }

        public UnitViewModel()
        {
            LoadCategories();
        }

        public void TestConnection()
        {
            using var connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                System.Windows.MessageBox.Show("Verbindung erfolgreich!");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Verbindung fehlgeschlagen: " + ex.Message);
            }
        }

        private void LoadCategories()
        {
            TestConnection(); 
            Categories.Clear();

            using var connection = new SqlConnection(connectionString);
            string query = "SELECT CategoryID, CategoryName FROM Categories";

            try
            {
                connection.Open();
                var command = new SqlCommand(query, connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Categories.Add(new Category
                    {
                        CategoryID = (int)reader["CategoryID"],
                        CategoryName = reader["CategoryName"].ToString()
                    });
                }
            }
            catch
            {
                // Fehlerbehandlung z.B. Log, MessageBox ...
            }
        }

        private void LoadUnits()
        {
            AvailableUnits.Clear();

            if (SelectedCategory == null) return;

            using var connection = new SqlConnection(connectionString);
            string query = "SELECT Nr, CategoryID, Unit, Factor FROM UnitDefinitions WHERE CategoryID = @catId";

            try
            {
                connection.Open();
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@catId", SelectedCategory.CategoryID);

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AvailableUnits.Add(new UnitDefinition
                    {
                        Nr = (int)reader["Nr"],
                        CategoryID = (int)reader["CategoryID"],
                        Unit = reader["Unit"].ToString(),
                        Factor = (double)reader["Factor"]
                    });
                }

                FromUnit = AvailableUnits.FirstOrDefault();
                ToUnit = AvailableUnits.Skip(1).FirstOrDefault();
            }
            catch
            {
                // Fehlerbehandlung
            }
        }

        private void ConvertUnits()
        {
            if (FromUnit == null || ToUnit == null || string.IsNullOrWhiteSpace(InputValue))
            {
                Result = "";
                return;
            }

            if (!double.TryParse(InputValue.Replace(",", "."), System.Globalization.NumberStyles.Any,
                                 System.Globalization.CultureInfo.InvariantCulture, out double input))
            {
                Result = "Ungültige Eingabe";
                return;
            }

            // Temperatur-Umrechnung erweitern, da Fahrenheit dabei ist
            if (SelectedCategory?.CategoryName == "Temperatur")
            {
                double tempInCelsius = input;

                // Eingangswert in Celsius umrechnen
                switch (FromUnit.Unit)
                {
                    case "Celsius":
                        tempInCelsius = input;
                        break;
                    case "Kelvin":
                        tempInCelsius = input - 273.15;
                        break;
                    case "Fahrenheit":
                        tempInCelsius = (input - 32) * 5 / 9;
                        break;
                }

                double convertedTemp = tempInCelsius;

                // Celsius in Zielwert umrechnen
                switch (ToUnit.Unit)
                {
                    case "Celsius":
                        convertedTemp = tempInCelsius;
                        break;
                    case "Kelvin":
                        convertedTemp = tempInCelsius + 273.15;
                        break;
                    case "Fahrenheit":
                        convertedTemp = (tempInCelsius * 9 / 5) + 32;
                        break;
                }

                Result = $"{convertedTemp:F2} {ToUnit.Unit}";
            }
            else
            {
                double baseValue = input * FromUnit.Factor;
                double converted = baseValue / ToUnit.Factor;

                Result = $"{converted:F8} {ToUnit.Unit}";
            }

            // Ergebnis speichern
            SaveConversion(input, Result);
        }

        private void SaveConversion(double inputValue, string resultValue)
        {
            if (SelectedCategory == null || FromUnit == null || ToUnit == null)
                return;

            using var connection = new SqlConnection(connectionString);
            string insertQuery = @"
        INSERT INTO ConversionHistory 
            (CategoryID, FromUnit, ToUnit, InputValue, ResultValue) 
        VALUES 
            (@CategoryID, @FromUnit, @ToUnit, @InputValue, @ResultValue)";

            try
            {
                connection.Open();
                using var command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@CategoryID", SelectedCategory.CategoryID);
                command.Parameters.AddWithValue("@FromUnit", FromUnit.Unit);
                command.Parameters.AddWithValue("@ToUnit", ToUnit.Unit);
                command.Parameters.AddWithValue("@InputValue", inputValue);
                command.Parameters.AddWithValue("@ResultValue", resultValue);

                command.ExecuteNonQuery();
            }
            catch
            {
                // Fehlerbehandlung, z.B. Logging, kann ignoriert werden, falls DB mal nicht erreichbar
            }
        }

        private void ShowHistory_Click(object sender, RoutedEventArgs e)
        {
            var historyWindow = new HistoryView();
            historyWindow.Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
