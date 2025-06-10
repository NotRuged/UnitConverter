using MVVM_Einheitenumrechner.Class;
using MVVM_Einheitenumrechner.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows;
using System.Xml.Linq;

namespace MVVM_Einheitenumrechner.ViewModel
{
    public class UnitViewModel : INotifyPropertyChanged
    {
        private string connectionString = @"Data Source=DESKTOP-L6EO2E6\MSSQLSERVER01;Trusted_Connection=yes;Database=UnitConverter;Connection Timeout=10;";

        public ObservableCollection<Category> Categories { get; set; } = new();
        public ObservableCollection<UnitDefinition> AvailableUnits { get; set; } = new();

        private Category _selectedCategory;
        private UnitDefinition _fromUnit;
        private UnitDefinition _toUnit;
        private string _inputValue;
        private string _result;

        // Timer für verzögertes Speichern
        private System.Timers.Timer _saveTimer;

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

                // Timer zurücksetzen, da neue Eingabe
                ResetSaveTimer();
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
            // Timer initialisieren: 5 Sekunden (5000 ms)
            _saveTimer = new System.Timers.Timer(2500);
            _saveTimer.AutoReset = false; // nur einmal auslösen
            _saveTimer.Elapsed += SaveTimer_Elapsed;

            LoadCategories();
        }

        private void ResetSaveTimer()
        {
            _saveTimer.Stop();
            _saveTimer.Start();
        }

        private void SaveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Timer läuft im Hintergrundthread - Wechsel in UI-Thread nötig
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (double.TryParse(InputValue?.Replace(",", "."), System.Globalization.NumberStyles.Any,
                                    System.Globalization.CultureInfo.InvariantCulture, out double input))
                {
                    SaveConversion(input, Result);
                }
            });
        }

        public void TestConnection()
        {
            using var connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                //MessageBox.Show("Verbindung erfolgreich!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verbindung fehlgeschlagen: " + ex.Message);
            }
        }

        private void LoadCategories()
        {
            try
            {
                TestConnection();
                Categories.Clear();

                using var connection = new SqlConnection(connectionString);
                string query = "SELECT CategoryID, CategoryName FROM Categories";

                connection.Open();
                using var command = new SqlCommand(query, connection);
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Categories.Add(new Category
                    {
                        CategoryID = reader["CategoryID"] as int? ?? 0,
                        CategoryName = reader["CategoryName"]?.ToString() ?? string.Empty
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Laden der Kategorien: " + ex.Message);
            }
        }


        private void LoadUnits()
        {
            try
            {
                AvailableUnits.Clear();

                if (SelectedCategory == null) return;

                using var connection = new SqlConnection(connectionString);
                string query = "SELECT Nr, CategoryID, Unit, Factor FROM UnitDefinitions WHERE CategoryID = @catId";

                connection.Open();
                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@catId", SelectedCategory.CategoryID);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AvailableUnits.Add(new UnitDefinition
                    {
                        Nr = reader["Nr"] as int? ?? 0,
                        CategoryID = reader["CategoryID"] as int? ?? 0,
                        Unit = reader["Unit"]?.ToString() ?? string.Empty,
                        Factor = reader["Factor"] as double? ?? 1.0
                    });
                }

                FromUnit = AvailableUnits.FirstOrDefault();
                ToUnit = AvailableUnits.Skip(1).FirstOrDefault();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Laden der Einheiten: " + ex.Message);
            }
        }


        private void ConvertUnits()
        {
            try
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

                // Schutz gegen Division durch Null
                if (ToUnit.Factor == 0)
                {
                    Result = "Fehler: Faktor der Ziel-Einheit ist 0";
                    return;
                }

                double baseValue = input * FromUnit.Factor;
                double converted = baseValue / ToUnit.Factor;

                Result = $"{converted:F2} {ToUnit.Unit}";
            }
            catch (Exception ex)
            {
                Result = "Fehler bei der Umrechnung";
                MessageBox.Show("Fehler bei der Umrechnung: " + ex.Message);
            }
        }


        private void SaveConversion(double inputValue, string resultValue)
        {
            if (SelectedCategory == null || FromUnit == null || ToUnit == null)
                return;

            try
            {
                using var connection = new SqlConnection(connectionString);
                string insertQuery = @"INSERT INTO ConversionHistory 
                               (CategoryID, FromUnit, ToUnit, InputValue, ResultValue) 
                               VALUES (@CategoryID, @FromUnit, @ToUnit, @InputValue, @ResultValue)";

                connection.Open();
                using var command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@CategoryID", SelectedCategory.CategoryID);
                command.Parameters.AddWithValue("@FromUnit", FromUnit.Unit);
                command.Parameters.AddWithValue("@ToUnit", ToUnit.Unit);
                command.Parameters.AddWithValue("@InputValue", inputValue);
                command.Parameters.AddWithValue("@ResultValue", resultValue ?? "");

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Speichern der Umrechnung: " + ex.Message);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}