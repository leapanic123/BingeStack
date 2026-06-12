using SQLite;
using BingeStack.Models;

namespace BingeStack
{
    public partial class App : Application
    {
        public static string dbAdresa =
            Path.Combine(FileSystem.AppDataDirectory, "dbBingeStack.db");

        public static SQLiteConnection db =
            new SQLiteConnection(dbAdresa);
        public static User TrenutniKorisnik;

        public App()
        {
            InitializeComponent();

            db.CreateTable<User>();
            db.CreateTable<Sadrzaj>();

            MainPage = new Pocetna();
    }
    }
}