using BingeStack.Models;

namespace BingeStack.Views;

public partial class Glavna : ContentPage
{
    public Glavna()
    {
        InitializeComponent();

        UcitajSadrzaj();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UcitajSadrzaj();
    }

    private void UcitajSadrzaj()
    {
        if (App.TrenutniKorisnik == null)
            return;

        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);

        var sadrzaji = App.db.Table<Sadrzaj>()
            .Where(x => x.UserId == App.TrenutniKorisnik.UserId)
            .ToList();

        if (sadrzaji.Count == 0)
        {
            nemaSadrzaja.IsVisible = true;

            listaZelja.IsVisible = false;
            listaZavrseno.IsVisible = false;
        }
        else
        {
            nemaSadrzaja.IsVisible = false;

            var zelje = sadrzaji.Where(x =>
                x.Status == "Želim gledati" ||
                x.Status == "Želim proèitati")
                .ToList();

            var zavrseno = sadrzaji.Where(x =>
                x.Status == "Pogledao sam" ||
                x.Status == "Proèitao sam")
                .ToList();

            listaZelja.ItemsSource = zelje;
            listaZavrseno.ItemsSource = zavrseno;

            listaZelja.IsVisible = zelje.Count > 0;
            listaZavrseno.IsVisible = zavrseno.Count > 0;
        }
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
        await Navigation.PushAsync(new Profil());
    }

    private async void SadrzajTapped(object sender, TappedEventArgs e)
    {
        var border = sender as Border;
        var item = border?.BindingContext as Sadrzaj;

        if (item == null)
            return;

        await DisplayAlert(
            item.Naziv,
            $"Vrsta: {item.Vrsta}\nStatus: {item.Status}\nOcjena: {item.Ocjena}\nOsvrt: {item.Osvrt}",
            "OK");
    }

    private async void ObrisiSadrzaj(object sender, EventArgs e)
    {
        var item = (sender as SwipeItem)?.BindingContext as Sadrzaj;

        if (item == null)
            return;

        bool potvrda = await DisplayAlert(
            "Brisanje",
            "Želite li obrisati sadržaj?",
            "Da",
            "Ne");

        if (!potvrda)
            return;

        App.db.Delete(item);

        UcitajSadrzaj();
    }

    private async void Pretraga_Clicked(object sender, EventArgs e)
    {
        string tekst = pretraga.Text?.Trim();

        if (string.IsNullOrWhiteSpace(tekst))
            return;

        var rezultat = App.db.Table<Sadrzaj>()
            .Where(x => x.UserId == App.TrenutniKorisnik.UserId)
            .FirstOrDefault(x =>
                x.Naziv.ToLower().Contains(tekst.ToLower()));

        if (rezultat == null)
        {
            await DisplayAlert(
                "Pretraga",
                $"Sadržaj '{tekst}' nije pronaðen.",
                "OK");

            return;
        }

        string poruka =
            $"Vrsta: {rezultat.Vrsta}\n" +
            $"Status: {rezultat.Status}";

        if (rezultat.Ocjena > 0)
            poruka += $"\nOcjena: {rezultat.Ocjena}";

        if (!string.IsNullOrWhiteSpace(rezultat.Osvrt))
            poruka += $"\n\nOsvrt:\n{rezultat.Osvrt}";

        await DisplayAlert(
            rezultat.Naziv,
            poruka,
            "OK");
    }
}