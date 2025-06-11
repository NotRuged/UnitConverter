using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Einheitenumrechner.Class
{
    /**
     * \brief Repräsentiert eine Kategorie von Einheiten.
     * 
     * Diese Klasse definiert eine Einheiten-Kategorie, z. B. „Länge“, „Gewicht“ oder „Temperatur“.
     * Sie enthält den Kategorienamen sowie die zugehörigen Umrechnungseinheiten und die Historie der Benutzereingaben.
     */
    public class Category
    {
        /**
         * \brief Primärschlüssel der Kategorie.
         * 
         * Eindeutige ID zur Identifikation dieser Kategorie in der Datenbank.
         */
        [Key]
        public int CategoryID { get; set; }

        /**
         * \brief Name der Kategorie.
         * 
         * Beschreibt, zu welchem Einheitenbereich diese Kategorie gehört (z. B. „Länge“).
         */
        public string CategoryName { get; set; }

        /**
         * \brief Historische Einträge für diese Kategorie.
         * 
         * Eine Sammlung von Benutzereingaben oder -aktionen, die zu dieser Kategorie gehören.
         */
        public ICollection<HistoryEntry> HistoryEntries { get; set; }

        /**
         * \brief Einheiten dieser Kategorie.
         * 
         * Enthält alle definierten Umrechnungseinheiten, die dieser Kategorie zugeordnet sind.
         */
        public ICollection<UnitDefinition> UnitDefinitions { get; set; }
    }
}
