using System.Windows;

namespace MVVM_Einheitenumrechner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Modus für das Repository (0 = Datenbank, sonst JSON o.ä.)
        /// </summary>
        public static int CheckSlide { get; private set; } = 0;

        /// <summary>
        /// Setzt den Modus des Repositorys.
        /// </summary>
        /// <param name="mode">0 für Datenbank, 1 für JSON (oder andere)</param>
        public static void SetRepositoryMode(int mode)
        {
            CheckSlide = mode;
        }
    }
}
