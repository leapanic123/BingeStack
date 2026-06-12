using BingeStack.Models;

namespace BingeStack.Views;

public partial class DodajSadrzaj : ContentPage
{
    public DodajSadrzaj()
    {
        InitializeComponent();
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
        osvrt.IsVisible = zavrseno;
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

        await DisplayAlert("Uspjeh", "Sadržaj spremljen.", "OK");

        await Navigation.PopAsync();
    }
}