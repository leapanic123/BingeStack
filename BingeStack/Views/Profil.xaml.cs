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

        if (App.TrenutniKorisnik != null)
        {
            ime.Text = App.TrenutniKorisnik.Name;
            email.Text = App.TrenutniKorisnik.Email;
        }

        datumVrijeme.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
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

    private async void ObrisiProfil_Clicked(object sender, EventArgs e)
    {
        if (App.TrenutniKorisnik == null)
            return;

        bool potvrda = await DisplayAlert(
            "Brisanje profila",
            "Jeste li sigurni da želite obrisati profil? Ova radnja je nepovratna.",
            "DA",
            "NE");

        if (!potvrda)
            return;

        try
        {
            var userId = App.TrenutniKorisnik.UserId;

            var sadrzaji = App.db.Table<Sadrzaj>()
                                 .Where(x => x.UserId == userId)
                                 .ToList();

            foreach (var s in sadrzaji)
            {
                App.db.Delete(s);
            }

            var user = App.db.Table<User>()
                             .FirstOrDefault(x => x.UserId == userId);

            if (user != null)
            {
                App.db.Delete(user);
            }

            App.TrenutniKorisnik = null;

            await DisplayAlert("Obrisano", "Profil je uspješno obrisan.", "OK");

            Application.Current.MainPage = new Pocetna();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Greška", ex.Message, "OK");
        }
    }
}