using BingeStack.Models;

namespace BingeStack.Views;

public partial class Profil : ContentPage
{
    public Profil()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);

        UcitajStatistiku();
    }

    private void UcitajStatistiku()
    {
        if (App.TrenutniKorisnik == null)
            return;

        var userId = App.TrenutniKorisnik.UserId;

        var q = App.db.Table<Sadrzaj>()
                        .Where(x => x.UserId == userId);

        filmovi.Text = q.Count(x => x.Vrsta == "Film").ToString();
        serije.Text = q.Count(x => x.Vrsta == "Serija").ToString();
        knjige.Text = q.Count(x => x.Vrsta == "Knjiga").ToString();

        ime.Text = App.TrenutniKorisnik.Name;
        email.Text = App.TrenutniKorisnik.Email;

        filmAktivnost.Text = q.Any(x => x.Vrsta == "Film")
? " Ima dodanih filmova"
: " Nema dodanih filmova";

        serijaAktivnost.Text = q.Any(x => x.Vrsta == "Serija")
            ? " Ima dodanih serija"
            : " Nema dodanih serija";

        knjigaAktivnost.Text = q.Any(x => x.Vrsta == "Knjiga")
            ? " Ima dodanih knjiga"
            : " Nema dodanih knjiga";
    }

    private void Kucica_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new Glavna());
    }

    private async void Plus_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DodajSadrzaj());
    }

    private async void Profil_Clicked(object sender, EventArgs e)
    {

    }

    private async void UrediProfil_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Info", "Uređivanje profila još nije implementirano.", "OK");
    }

    private async void Odjava_Clicked(object sender, EventArgs e)
    {
        bool potvrda = await DisplayAlert(
            "Odjava",
            "Jeste li sigurni da se želite odjaviti?",
            "Da",
            "Ne");

        if (!potvrda)
            return;

        App.TrenutniKorisnik = null;

        Application.Current.MainPage = new NavigationPage(new Pocetna());
    }
}