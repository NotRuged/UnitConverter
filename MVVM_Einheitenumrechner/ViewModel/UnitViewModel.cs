using MVVM_Einheitenumrechner.Class;
using MVVM_Einheitenumrechner.Data;
using MVVM_Einheitenumrechner.NewFolder;
using MVVM_Einheitenumrechner.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows;

namespace MVVM_Einheitenumrechner.ViewModel
{
    /**
     * \brief ViewModel zur Steuerung der Einheiten-Umrechnung und Verwaltung von Kategorien und Einheiten.
     * 
     * Lädt Kategorien und Einheiten, rechnet Werte um, speichert Umrechnungen in DB oder JSON.
     */
    public class UnitViewModel : INotifyPropertyChanged
    {
        /// \brief Liste aller Kategorien.
        public ObservableCollection<Category> Categories { get; set; } = new();

        /// \brief Liste der Einheiten der ausgewählten Kategorie.
        public ObservableCollection<UnitDefinition> AvailableUnits { get; set; } = new();

        private Category _selectedCategory;
        private UnitDefinition _fromUnit;
        private UnitDefinition _toUnit;
        private string _inputValue;
        private string _result;

        private List<HistoryEntry> _historyList = new();
        private readonly FileJSONRepository _jsonRepo = new("History");
        private readonly FileJSONRepository _history = new FileJSONRepository("History");


        private System.Timers.Timer _saveTimer;

        /**
         * \brief Die aktuell ausgewählte Kategorie.
         * 
         * Lädt Einheiten der Kategorie beim Setzen.
         */
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

        /// \brief Die Ausgangseinheit der Umrechnung.
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

        /// \brief Die Ziel-Einheit der Umrechnung.
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

        /// \brief Der Eingabewert für die Umrechnung als String.
        public string InputValue
        {
            get => _inputValue;
            set
            {
                _inputValue = value;
                OnPropertyChanged();
                ConvertUnits();
                ResetSaveTimer();
            }
        }

        /// \brief Das Ergebnis der Umrechnung als String.
        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged();
            }
        }

        /**
         * \brief Konstruktor initialisiert Timer und lädt Kategorien.
         */
        public UnitViewModel()
        {
            _saveTimer = new System.Timers.Timer(2500);
            _saveTimer.AutoReset = false; // nur einmal auslösen
            _saveTimer.Elapsed += SaveTimer_Elapsed;

            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.SetSliderVisibility(true);
            CheckSlider();
        }

        /**
         * \brief Startet oder resettet den Timer zum verzögerten Speichern.
         */
        private void ResetSaveTimer()
        {
            _saveTimer.Stop();
            _saveTimer.Start();
        }


        /**
         * \brief Eventhandler zum Speichern der Umrechnung nach Timerablauf.
         */
        private void SaveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (double.TryParse(InputValue?.Replace(",", "."), System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out double input))
                {
                    int slidevalue = MainWindow.CheckSlideMode;

                    if (slidevalue == 0)
                    {
                        SaveConversion(input, Result);
                    }
                    else
                    {
                        SaveConversionJson(input, Result);
                    }
                }
            });
        }

        /**
         * \brief Prüft den Modus (Datenbank oder JSON) und lädt Kategorien entsprechend.
         */
        private void CheckSlider()
        {
            int slidevalue = MainWindow.CheckSlideMode;
            LoadCategories();
        }

        /**
         * \brief Lädt alle Kategorien aus der Datenbank.
         */
        private void LoadCategories()
        {
            try
            {
                using var context = new UnitCalculatorContext();
                Categories.Clear();
                var cats = context.Categories.ToList();
                foreach (var cat in cats)
                    Categories.Add(cat);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Laden der Kategorien: " + ex.Message);
            }
        }

        /**
         * \brief Lädt die Einheiten der aktuell ausgewählten Kategorie.
         */
        private void LoadUnits()
        {
            try
            {
                AvailableUnits.Clear();
                if (SelectedCategory == null) return;

                using var context = new UnitCalculatorContext();
                var units = context.UnitDefinitions
                    .Where(u => u.CategoryID == SelectedCategory.CategoryID)
                    .ToList();
                foreach (var unit in units)
                    AvailableUnits.Add(unit);

                FromUnit = AvailableUnits.FirstOrDefault();
                ToUnit = AvailableUnits.Skip(1).FirstOrDefault();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Laden der Einheiten: " + ex.Message);
            }
        }

        /**
         * \brief Führt die Umrechnung durch und setzt das Ergebnis.
         */
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

                if (ToUnit.Factor == 0)
                {
                    Result = "Fehler: Faktor der Ziel-Einheit ist 0";
                    return;
                }

                decimal baseValue = (decimal)input * FromUnit.Factor;
                decimal converted = baseValue / ToUnit.Factor;
                Result = $"{converted:F2} {ToUnit.Unit}";
            }
            catch (Exception ex)
            {
                Result = "Fehler bei der Umrechnung";
                MessageBox.Show("Fehler bei der Umrechnung: " + ex.Message);
            }
        }

        /**
         * \brief Speichert die Umrechnung in der Datenbank.
         */
        private void SaveConversion(double inputValue, string resultValue)
        {
            if (SelectedCategory == null || FromUnit == null || ToUnit == null)
                return;

            try
            {
                using var context = new UnitCalculatorContext();
                var history = new HistoryEntry
                {
                    CategoryID = SelectedCategory.CategoryID,
                    FromUnit = FromUnit.Unit,
                    ToUnit = ToUnit.Unit,
                    InputValue = inputValue,
                    ResultValue = resultValue ?? ""
                };

                context.HistoryEntries.Add(history);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Speichern:{ex.Message}");
            }
        }

        /**
         * \brief Speichert die Umrechnung im JSON-Format.
         */
        private void SaveConversionJson(double inputValue, string resultValue)
        {
            try
            {
                var existingEntries = _history.Load<HistoryEntry>();
                var entry = new HistoryEntry
                {
                    CategoryID = SelectedCategory.CategoryID,
                    FromUnit = FromUnit.Unit,
                    ToUnit = ToUnit.Unit,
                    InputValue = inputValue,
                    ResultValue = resultValue ?? ""
                };

                _historyList.Add(entry);
                _jsonRepo.Save(_historyList);

                if (existingEntries != null)
                {
                    _historyList.AddRange(existingEntries);
                    _jsonRepo.Save(_historyList);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Speichern in JSON:\n{ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// \brief Event zum Benachrichtigen von Property-Änderungen.
        public event PropertyChangedEventHandler PropertyChanged;

        /// \brief Löst das PropertyChanged-Event aus.
        protected virtual void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
