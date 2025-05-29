using MVVM_Einheitenumrechner.Class;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace MVVM_Einheitenumrechner.ViewModel
{
    public class HistoryViewModel : INotifyPropertyChanged
    {
       // private string connectionString = @"Data Source=DESKTOP-OIR8S4A\SQLEXPRESS;Trusted_Connection=yes;Database=UnitCalculator;Connection Timeout=10;";
        private string connectionString = @"Data Source=DESKTOP-L6EO2E6\MSSQLSERVER01;Trusted_Connection=yes;Database=UnitConverter1;Connection Timeout=10;";

        public ObservableCollection<HistoryEntry> HistoryEntries { get; set; } = new();

        public HistoryViewModel()
        {
            LoadHistory();
        }

        public void LoadHistory()
        {
            HistoryEntries.Clear();

            using var connection = new SqlConnection(connectionString);
            string query = @"
        SELECT ch.ConversionID, ch.Timestamp, ch.CategoryID, c.CategoryName, ch.FromUnit, ch.ToUnit, ch.InputValue, ch.ResultValue
        FROM ConversionHistory ch
        JOIN Categories c ON ch.CategoryID = c.CategoryID
        ORDER BY ch.Timestamp DESC";

            connection.Open();
            var command = new SqlCommand(query, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                HistoryEntries.Add(new HistoryEntry
                {
                    ConversionID = (int)reader["ConversionID"],
                    Timestamp = (DateTime)reader["Timestamp"],
                    CategoryID = (int)reader["CategoryID"],
                    CategoryName = reader["CategoryName"].ToString(),  // hinzufügen
                    FromUnit = reader["FromUnit"].ToString(),
                    ToUnit = reader["ToUnit"].ToString(),
                    InputValue = (double)reader["InputValue"],
                    ResultValue = reader["ResultValue"].ToString(),
                });

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
