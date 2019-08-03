using ConverterStringFormat.ViewModels;
using Xamarin.Forms;

namespace ConverterStringFormat
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await ((ViewModel_MainPage)this.BindingContext).RefreshData();
        }
    }
}
