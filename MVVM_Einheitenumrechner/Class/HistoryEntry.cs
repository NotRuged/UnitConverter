using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Einheitenumrechner.Class
{
    /**
     * \brief Repräsentiert einen Eintrag in der Umrechnungshistorie.
     * 
     * Diese Klasse speichert Informationen über eine durchgeführte Umrechnung, 
     * einschließlich Eingabewert, Ergebnis, verwendete Einheiten und Zeitstempel.
     */
    public class HistoryEntry
    {
        /**
         * \brief Eindeutige ID der Umrechnung.
         * 
         * Dient als Primärschlüssel zur Identifikation des Historieneintrags.
         */
        [Key]
        public int ConversionID { get; set; }

        /**
         * \brief Zeitpunkt der Umrechnung.
         * 
         * Gibt an, wann die Umrechnung durchgeführt wurde. Standardmäßig aktuelles Datum.
         */
        public DateTime Timestamp { get; set; } = DateTime.Now;

        /**
         * \brief Fremdschlüssel zur zugehörigen Kategorie.
         * 
         * Verweist auf die Kategorie, zu der diese Umrechnung gehört.
         */
        public int CategoryID { get; set; }

        /**
         * \brief Navigation zur Kategorie.
         * 
         * Objektverknüpfung zur zugehörigen Kategorieinstanz.
         */
        public Category CategoryName { get; set; }

        /**
         * \brief Ursprüngliche Einheit.
         * 
         * Die Einheit, aus der umgerechnet wurde.
         */
        public string FromUnit { get; set; }

        /**
         * \brief Ziel-Einheit.
         * 
         * Die Einheit, in die umgerechnet wurde.
         */
        public string ToUnit { get; set; }

        /**
         * \brief Eingabewert der Umrechnung.
         * 
         * Wert, der in der ursprünglichen Einheit angegeben wurde.
         */
        public double InputValue { get; set; }

        /**
         * \brief Ergebnis der Umrechnung.
         * 
         * Der berechnete Wert in der Ziel-Einheit.
         */
        public string ResultValue { get; set; }
    }
}
