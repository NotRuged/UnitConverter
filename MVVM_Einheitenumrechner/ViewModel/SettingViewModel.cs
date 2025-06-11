using MVVM_Einheitenumrechner.Class;
using MVVM_Einheitenumrechner.NewFolder;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MVVM_Einheitenumrechner.ViewModel
{
    /**
     * \brief ViewModel für die Einstellungen zum Hinzufügen von Kategorien und Einheiten.
     * 
     * Ermöglicht das Speichern neuer Einheiten mit zugehöriger Kategorie in die Datenbank.
     */
    public class SettingViewModel : INotifyPropertyChanged
    {
        /// \brief Der Name der Kategorie.
        public string CategoryName
        {
            get => _categoryName;
            set
            {
                _categoryName = value;
                OnPropertyChanged();
            }
        }
        private string _categoryName;

        /// \brief Die neue Einheit.
        public string Unit
        {
            get => _unit;
            set
            {
                _unit = value;
                OnPropertyChanged();
            }
        }
        private string _unit;

        /// \brief Der Umrechnungsfaktor für die Einheit.
        public decimal Factor
        {
            get => _factor;
            set
            {
                _factor = value;
                OnPropertyChanged();
            }
        }
        private decimal _factor;

        /// \brief Meldungstext zur Anzeige von Benutzerinformationen.
        public string MessageText
        {
            get => _messageText;
            set
            {
                _messageText = value;
                OnPropertyChanged();
            }
        }
        private string _messageText;

        /// \brief Farbe der Meldung (z. B. Grün für Erfolg, Rot für Fehler).
        public Brush MessageColor
        {
            get => _messageColor;
            set
            {
                _messageColor = value;
                OnPropertyChanged();
            }
        }
        private Brush _messageColor;

        /// \brief Kommando zum Speichern der Einheit.
        public ICommand SaveCommand { get; set; }

        /**
         * \brief Konstruktor, der den Modus prüft und die Befehle entsprechend initialisiert.
         */
        public SettingViewModel()
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.SetSliderVisibility(false);
            CheckSlider();
        }

        /**
         * \brief Prüft den Slider-Modus und setzt den Speicherbefehl.
         * 
         * Bei Slide-Wert 0 wird das Speichern über die Datenbank aktiviert.
         */
        public void CheckSlider()
        {
            int slidevalue = MainWindow.CheckSlideMode;

            if (slidevalue == 0)
            {
                SaveCommand = new RelayCommand(SaveSetting);
            }
            else
            {
                SaveCommand = new RelayCommand(SaveSetting);
            }
        }

        /**
         * \brief Speichert eine neue Einheit und ggf. eine neue Kategorie in die Datenbank.
         * 
         * \param parameter Optionales Übergabeobjekt (nicht verwendet).
         */
        private void SaveSetting(object parameter)
        {
            if (string.IsNullOrWhiteSpace(CategoryName) || string.IsNullOrWhiteSpace(Unit) || Factor == 0)
            {
                MessageText = "Bitte alle Felder korrekt ausfüllen.";
                MessageColor = Brushes.Red;
                return;
            }

            try
            {
                using var db = new UnitCalculatorContext();

                var category = db.Categories.FirstOrDefault(c => c.CategoryName == CategoryName);
                if (category == null)
                {
                    category = new Category { CategoryName = CategoryName };
                    db.Categories.Add(category);
                    db.SaveChanges();
                }

                var newUnit = new UnitDefinition
                {
                    CategoryID = category.CategoryID,
                    Unit = Unit,
                    Factor = Factor
                };

                db.UnitDefinitions.Add(newUnit);
                db.SaveChanges();

                MessageText = "Einheit erfolgreich gespeichert!";
                MessageColor = Brushes.Green;

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

        /**
         * \brief Event zur Benachrichtigung bei Property-Änderungen.
         */
        public event PropertyChangedEventHandler PropertyChanged;

        /**
         * \brief Löst das PropertyChanged-Event aus.
         * 
         * \param propertyName Der Name der geänderten Eigenschaft.
         */
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /**
         * \brief Implementierung eines ICommand zur Ausführung von Aktionen.
         */
        public class RelayCommand : ICommand
        {
            private readonly Action<object> _execute;
            private readonly Predicate<object> _canExecute;

            /**
             * \brief Konstruktor für den RelayCommand.
             * 
             * \param execute Die auszuführende Aktion.
             * \param canExecute Optionales Prädikat zur Steuerung der Ausführbarkeit.
             */
            public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                _canExecute = canExecute;
            }

            /// \brief Gibt an, ob das Kommando ausgeführt werden kann.
            public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

            /// \brief Führt das Kommando aus.
            public void Execute(object parameter) => _execute(parameter);

            /// \brief Event zur Änderung der Ausführbarkeit.
            public event EventHandler CanExecuteChanged
            {
                add => CommandManager.RequerySuggested += value;
                remove => CommandManager.RequerySuggested -= value;
            }
        }
    }
}
