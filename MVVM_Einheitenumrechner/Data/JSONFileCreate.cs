using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MVVM_Einheitenumrechner.Data
{
    /**
     * \brief Generische JSON-Dateiverwaltung für Datenobjekte.
     * 
     * Diese Klasse übernimmt das Speichern und Laden von Datenlisten im JSON-Format aus einer Datei.
     * Der Pfad zur Datei wird je nach Typ dynamisch gesetzt (z. B. „History“, „UnitDefinition“ oder „Categories“).
     */
    public class FileJSONRepository
    {
        /**
         * \brief Pfad zur JSON-Datei.
         * 
         * Abhängig vom Typ („History“, „UnitDefinition“, „Categories“) wird ein unterschiedlicher Dateipfad verwendet.
         */
        private readonly string _filePath;

        /**
         * \brief Konstruktor zum Setzen des Dateipfads basierend auf dem Typ.
         * 
         * \param type Der Datentyp, der gespeichert oder geladen werden soll (z. B. „History“).
         */
        public FileJSONRepository(string type)
        {
            // Dynamisch den Pfad festlegen
            if (type == "History")
            {
                _filePath = "..\\..\\..\\..\\History.json";
            }
            else if (type == "UnitDefinition")
            {
                _filePath = "..\\..\\..\\..\\UnitDefinition.json";
            }
            else
            {
                _filePath = "..\\..\\..\\..\\Categories.json";
            }
        }

        /**
         * \brief Speichert eine Liste von Objekten als JSON-Datei.
         * 
         * \tparam T Der Typ der Objekte in der Liste.
         * \param items Die zu speichernde Liste von Objekten.
         */
        public void Save<T>(List<T> items)
        {
            var json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        /**
         * \brief Lädt eine Liste von Objekten aus einer JSON-Datei.
         * 
         * \tparam T Der Typ der zu ladenden Objekte.
         * \return Eine Liste der geladenen Objekte. Gibt eine leere Liste zurück, wenn die Datei nicht existiert.
         */
        public List<T> Load<T>()
        {
            if (!File.Exists(_filePath))
                return new List<T>();

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }
    }
}
