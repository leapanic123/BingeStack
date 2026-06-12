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

        var sadrzaji = App.db.Table<Sadrzaj>()
            .Where(x => x.UserId == App.TrenutniKorisnik.UserId)
            .ToList();

        if (sadrzaji.Count == 0)
        {
            nemaSadrzaja.IsVisible = true;
            listaSadrzaja.IsVisible = false;
        }
        else
        {
            nemaSadrzaja.IsVisible = false;
            listaSadrzaja.IsVisible = true;

            listaSadrzaja.ItemsSource = sadrzaji;
        }
    }

    private async void Kucica_Clicked(object sender, EventArgs e)
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

        if (item == null) return;

        await DisplayAlert(
            item.Naziv,
            $"Vrsta: {item.Vrsta}\nStatus: {item.Status}\nOcjena: {item.Ocjena}\nOsvrt: {item.Osvrt}",
            "OK");
    }
    private async void ObrisiSadrzaj(object sender, EventArgs e)
    {
        var item = (sender as SwipeItem)?.BindingContext as Sadrzaj;

        if (item == null) return;

        bool ok = await DisplayAlert("Brisanje", "Obrisati sadržaj?", "Da", "Ne");
        if (!ok) return;

        App.db.Delete(item);
        UcitajSadrzaj();
    }
}