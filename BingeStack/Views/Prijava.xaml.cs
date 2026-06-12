using BingeStack.Models;

namespace BingeStack.Views;

public partial class Prijava : ContentPage
{
	public Prijava()
	{
		InitializeComponent();
	}
    private async void prijavaGumb_Clicked(object sender, EventArgs e)
    {
        var u = App.db.Table<User>().Where(x => x.Email == emailPolje.Text && x.Password == zaporkaPolje.Text).FirstOrDefault();
        if (u != null)
        {
            await DisplayAlert("Obavijest", "Uspješna prijava.", "Nastavi");
            App.Current.MainPage = new NavigationPage(new Glavna());
        }
        else
        {
            await DisplayAlert("Obavijest", "Pogrešan email ili lozinka. Molimo pokušajte ponovno.", "U redu");
        }

    }

    private void registracijaGumb_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new Registracija();
    }

    private bool isPasswordVisible = false;
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