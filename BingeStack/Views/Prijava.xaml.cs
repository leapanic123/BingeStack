using BingeStack.Models;

namespace BingeStack.Views;

public partial class Prijava : ContentPage
{
    private bool isPasswordVisible = false;

    public Prijava()
    {
        InitializeComponent();
    }

    private async void prijavaGumb_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(emailPolje.Text) ||
            string.IsNullOrWhiteSpace(zaporkaPolje.Text))
        {
            await DisplayAlert(
                "Obavijest",
                "Unesite email i lozinku.",
                "U redu");

            return;
        }

        string email = emailPolje.Text.Trim();
        string pass = zaporkaPolje.Text.Trim();

        var u = App.db.Table<User>()
                      .FirstOrDefault(x =>
                          x.Email == email &&
                          x.Password == pass);

        if (u != null)
        {
            App.TrenutniKorisnik = u;

            Application.Current.MainPage =
                new NavigationPage(new Glavna());
        }
        else
        {
            await DisplayAlert(
                "Obavijest",
                "Pogrešan email ili lozinka. Molimo pokušajte ponovno.",
                "U redu");
        }
    }

    private void registracijaGumb_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage =
            new Registracija();
    }

    private void okoOtvoreno_Clicked(object sender, EventArgs e)
    {
        isPasswordVisible = !isPasswordVisible;

        zaporkaPolje.IsPassword = !isPasswordVisible;

        var button = (ImageButton)sender;

        button.Source = isPasswordVisible
            ? "oko_zatvoreno.png"
            : "oko_otvoreno.png";
    }

    private async void ZaboravljenaZaporka_Tapped(object sender, TappedEventArgs e)
    {
        await DisplayAlert(
            "Obavijest",
            "Poslan Vam je mail za ponovno postavljanje zaporke.",
            "U redu");
    }
}