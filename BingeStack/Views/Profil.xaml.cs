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
        UcitajStatistiku();
    }

    private void UcitajStatistiku()
    {
        if (App.TrenutniKorisnik == null)
            return;

        var q = App.db.Table<Sadrzaj>()
            .Where(x => x.UserId == App.TrenutniKorisnik.UserId);

        filmovi.Text = q.Count(x => x.Vrsta == "Film").ToString();
        serije.Text = q.Count(x => x.Vrsta == "Serija").ToString();
        knjige.Text = q.Count(x => x.Vrsta == "Knjiga").ToString();

        ime.Text = App.TrenutniKorisnik.Name;
        email.Text = App.TrenutniKorisnik.Email;
    }

    private async void UrediProfil_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert(
            "Info",
            "Ureðivanje profila još nije implementirano.",
            "OK");
    }

    private async void Odjava_Clicked(object sender, EventArgs e)
    {
        App.TrenutniKorisnik = null;

        Application.Current.MainPage = new Pocetna();
    }
}