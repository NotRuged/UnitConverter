using MVVM_Einheitenumrechner.Class;
using MVVM_Einheitenumrechner.Data;
using MVVM_Einheitenumrechner.NewFolder;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MVVM_Einheitenumrechner.ViewModel
{
    /**
     * \brief ViewModel zur Verwaltung und Darstellung der Umrechnungshistorie.
     * 
     * Diese Klasse stellt eine observable Liste von \c HistoryEntry -Objekten bereit und lädt Daten
     * entweder aus der Datenbank oder aus einer JSON-Datei – abhängig vom gewählten Modus (Slider).
     */
    public class HistoryViewModel : INotifyPropertyChanged
    {
        /**
         * \brief Repository für den JSON-Datenzugriff.
         * 
         * Dient dem Laden der Historie aus einer lokalen JSON-Datei.
         */
        private readonly FileJSONRepository _history = new FileJSONRepository("History");

        /**
         * \brief Liste der geladenen Historieneinträge.
         * 
         * Wird zur Datenbindung im View verwendet.
         */
        public ObservableCollection<HistoryEntry> HistoryEntries { get; set; } = new();

        /**
         * \brief Konstruktor, prüft den Slider-Modus und lädt entsprechende Daten.
         */
        public HistoryViewModel()
        {
            CheckSlider();
        }

        /**
         * \brief Entscheidet anhand des Sliderwerts, ob die Datenbank oder die JSON-Datei verwendet wird.
         */
        private void CheckSlider()
        {
            int slidevalue = MainWindow.CheckSlideMode;

            if (slidevalue == 0)
            {
                LoadHistory();
            }
            else
            {
                LoadHistoryJSon();
            }
        }

        /**
         * \brief Lädt die Umrechnungshistorie aus der Datenbank.
         */
        public void LoadHistory()
        {
            HistoryEntries.Clear();

            using var context = new UnitCalculatorContext();

            var history = context.HistoryEntries
                .OrderByDescending(ch => ch.Timestamp)
                .Select(ch => new HistoryEntry
                {
                    ConversionID = ch.ConversionID,
                    Timestamp = ch.Timestamp,
                    CategoryID = ch.CategoryID,
                    CategoryName = ch.CategoryName,
                    FromUnit = ch.FromUnit,
                    ToUnit = ch.ToUnit,
                    InputValue = ch.InputValue,
                    ResultValue = ch.ResultValue
                }).ToList();

            foreach (var item in history)
            {
                HistoryEntries.Add(item);
            }
        }

        /**
         * \brief Lädt die Umrechnungshistorie aus der JSON-Datei.
         * 
         * Zeigt eine Fehlermeldung an, falls das Laden fehlschlägt.
         */
        public void LoadHistoryJSon()
        {
            HistoryEntries.Clear();

            try
            {
                var historyList = _history.Load<HistoryEntry>();
                foreach (var entry in historyList)
                {
                    HistoryEntries.Add(entry);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Laden der History aus JSON:\n{ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /**
         * \brief Event zur Benachrichtigung bei Property-Änderungen (für Datenbindung).
         */
        public event PropertyChangedEventHandler PropertyChanged;

        /**
         * \brief Löst das PropertyChanged-Event aus.
         * 
         * \param name Der Name der geänderten Eigenschaft.
         */
        protected virtual void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
