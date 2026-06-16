using BingeStack.Models;

namespace BingeStack.Views;

public partial class DodajSadrzaj : ContentPage
{
    public DodajSadrzaj()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);
    }

    private void VrstaPromijenjena(object sender, EventArgs e)
    {
        status.Items.Clear();

        if (vrsta.SelectedItem?.ToString() == "Knjiga")
        {
            status.Items.Add("Želim proèitati");
            status.Items.Add("Proèitao sam");
        }
        else
        {
            status.Items.Add("Želim gledati");
            status.Items.Add("Pogledao sam");
        }
    }

    private void StatusPromijenjen(object sender, EventArgs e)
    {
        string odabraniStatus = status.SelectedItem?.ToString();

        bool zavrseno =
            odabraniStatus == "Pogledao sam" ||
            odabraniStatus == "Proèitao sam";

        ocjenaSekcija.IsVisible = zavrseno;
        osvrtSekcija.IsVisible = zavrseno;
    }

    private async void Spremi_Clicked(object sender, EventArgs e)
    {
        if (App.TrenutniKorisnik == null)
        {
            await DisplayAlert("Greška", "Korisnik nije prijavljen.", "OK");
            return;
        }

        if (vrsta.SelectedItem == null || status.SelectedItem == null)
        {
            await DisplayAlert("Greška", "Odaberi vrstu i status.", "OK");
            return;
        }

        Sadrzaj novi = new()
        {
            Naziv = naziv.Text,
            Vrsta = vrsta.SelectedItem?.ToString(),
            Status = status.SelectedItem?.ToString(),
            Osvrt = osvrt.Text ?? "",
            Ocjena = ocjena.SelectedItem == null
                ? 0
                : Convert.ToInt32(ocjena.SelectedItem),

            UserId = App.TrenutniKorisnik.UserId
        };

        App.db.Insert(novi);

        await DisplayAlert("", "Sadržaj uspješno spremljen.", "OK");

        await Navigation.PopAsync();
    }

    private async void Kucica_Clicked(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new Glavna());
    }

    private void Plus_Clicked(object sender, TappedEventArgs e)
    {
    }

    private async void Profil_Clicked(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new Profil());
    }
}