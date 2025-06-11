using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Einheitenumrechner.Class
{
    /**
     * \brief Definiert eine Einheit innerhalb einer Kategorie.
     * 
     * Diese Klasse enthält Informationen über eine spezifische Einheit, 
     * ihren Umrechnungsfaktor sowie die zugehörige Kategorie.
     */
    public class UnitDefinition
    {
        /**
         * \brief Primärschlüssel der Einheit.
         * 
         * Eindeutige ID zur Identifikation der Einheit.
         */
        [Key]
        public int Nr { get; set; }

        /**
         * \brief Name oder Symbol der Einheit.
         * 
         * Bezeichnung wie z. B. „cm“, „kg“ oder „°C“.
         */
        public string Unit { get; set; }

        /**
         * \brief Umrechnungsfaktor zur Basiseinheit.
         * 
         * Gibt an, mit welchem Faktor diese Einheit zur Standard-Basiseinheit umgerechnet wird.
         */
        public decimal Factor { get; set; }

        /**
         * \brief Fremdschlüssel zur zugehörigen Kategorie.
         * 
         * Verknüpft diese Einheit mit einer bestimmten Kategorie (z. B. „Länge“).
         */
        public int CategoryID { get; set; }

        /**
         * \brief Navigation zur Kategorie.
         * 
         * Objektverknüpfung zur übergeordneten Kategorie, der diese Einheit angehört.
         */
        [ForeignKey("CategoryID")]
        public Category Category { get; set; }
    }
}
