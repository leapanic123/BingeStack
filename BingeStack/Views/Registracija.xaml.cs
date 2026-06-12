using BingeStack.Models;
using SQLite;
using System.Text.RegularExpressions;

namespace BingeStack.Views;

public partial class Registracija : ContentPage
{
	public Registracija()
	{
        InitializeComponent();
	}

    private void prijavaGumb_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage= new Prijava();
    }

    private async void registracijaGumb_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty (imPrezPolje.Text) || string.IsNullOrEmpty(emailPolje.Text) || string.IsNullOrEmpty(zaporkaPolje.Text))
        {
            await DisplayAlert("Obavijest", "Molimo ispunite sva obavezna polja.", "U redu");
            return;
        }

        if (!uvjetiCheckBox.IsChecked)
        {
            await DisplayAlert(
                "Obavijest",
                "Morate prihvatiti uvjete korištenja i pravila privatnosti.",
                "U redu");

            return;
        }

        if (zaporkaPolje.Text != potvrdiZaporkaPolje.Text)
        {
            await DisplayAlert("Obavijest", "Zaporke se ne podudaraju.", "U redu");
            return;
        }

        if (!Regex.IsMatch(emailPolje.Text, "^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$"))
        {
            await DisplayAlert("Obavijest", "Unesite valjanu email adresu.", "U redu");
            return;
        }

        App.db.CreateTable<User>();
        var u = App.db.Table<User>().FirstOrDefault(x => x.Email == emailPolje.Text);
        if (u != null)
        {
            await DisplayAlert("Obavijest", "Raèun s tom e-adresom veæ postoji.", "U redu");
            return;
        }

        if (!Regex.IsMatch(zaporkaPolje.Text, "^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$"))
        {
            await DisplayAlert("Obavijest", "Zaporka mora imati najmanje 8 znakova i sadržavati slovo, broj te poseban znak (npr. @, !, %, &).", "U redu");
            return;
        }

        User user = new User()
        {
            Name = imPrezPolje.Text,
            Email = emailPolje.Text,
            Password = zaporkaPolje.Text
        };

        App.db.Insert(user);
        await DisplayAlert("Obavijest", "Raèun je uspješno kreiran!", "U redu");
        App.Current.MainPage = new Prijava();
    }

    private bool isPasswordVisible = false;
    private bool isConfirmPasswordVisible = false;

    private void okoOtvoreno_Clicked(object sender, EventArgs e)
    {
        isPasswordVisible = !isPasswordVisible;

        zaporkaPolje.IsPassword = !isPasswordVisible;

        var button = (ImageButton)sender;

        button.Source = isPasswordVisible
            ? "oko_zatvoreno.png"
            : "oko_otvoreno.png";
    }

    private void okoOtvoreno2_Clicked(object sender, EventArgs e)
    {
        isConfirmPasswordVisible = !isConfirmPasswordVisible;

        potvrdiZaporkaPolje.IsPassword = !isConfirmPasswordVisible;

        var button = (ImageButton)sender;

        button.Source = isConfirmPasswordVisible
            ? "oko_zatvoreno.png"
            : "oko_otvoreno.png";
    }
}