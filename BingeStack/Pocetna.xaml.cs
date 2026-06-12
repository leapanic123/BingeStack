using BingeStack.Views;
namespace BingeStack;

public partial class Pocetna : ContentPage
{
	public Pocetna()
	{
		InitializeComponent();
	}

    private void Image_SizeChanged(object sender, EventArgs e)
    {

    }

    private void zapocniGumb_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new Prijava();
    }
}