using SQLite;

namespace BingeStack
{
    public partial class App : Application
    {
        public static string dbAdresa = Path.Combine(FileSystem.AppDataDirectory, "dbBingeStack.db");
        public static SQLiteConnection db = new SQLiteConnection(dbAdresa);
        public App()
        {
            InitializeComponent();

            MainPage = new Pocetna();
        }
    }
}
