using Microsoft.EntityFrameworkCore;
using MVVM_Einheitenumrechner.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Einheitenumrechner.NewFolder
{
    /**
     * \brief Datenbankkontext für den Einheitenumrechner.
     * 
     * Verwaltet die Verbindung zur Datenbank und definiert die Zuordnung der Entitäten zu Tabellen und Beziehungen.
     * Diese Klasse wird von Entity Framework Core verwendet.
     */
    public class UnitCalculatorContext : DbContext
    {
        /**
         * \brief Historieneinträge in der Datenbank.
         */
        public DbSet<HistoryEntry> HistoryEntries { get; set; }

        /**
         * \brief Kategorien in der Datenbank.
         */
        public DbSet<Category> Categories { get; set; }

        /**
         * \brief Einheitendefinitionen in der Datenbank.
         */
        public DbSet<UnitDefinition> UnitDefinitions { get; set; }

        /**
         * \brief Konfiguriert die Datenbankverbindung.
         * 
         * Hier wird die SQL Server-Verbindungszeichenfolge definiert. 
         * TrustServerCertificate wird gesetzt, um Zertifikatswarnungen zu umgehen.
         * 
         * \param optionsBuilder Der Options-Builder für DbContext.
         */
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-OIR8S4A\\SQLEXPRESS;Initial Catalog=UnitCalculator;Integrated Security=True;TrustServerCertificate=True;");
        }

        /**
         * \brief Konfiguriert das Modell und die Tabellenstruktur.
         * 
         * Legt Tabellenzuordnungen und Beziehungen zwischen den Entitäten fest.
         * 
         * \param modelBuilder Der ModelBuilder zum Konfigurieren der Entitäten.
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistoryEntry>().ToTable("ConversionHistory");
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<UnitDefinition>().ToTable("UnitDefinitions");

            modelBuilder.Entity<Category>()
                .HasMany(c => c.HistoryEntries)
                .WithOne(h => h.CategoryName)
                .HasForeignKey(h => h.CategoryID);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.UnitDefinitions)
                .WithOne(ud => ud.Category)
                .HasForeignKey(ud => ud.CategoryID);
        }
    }
}
