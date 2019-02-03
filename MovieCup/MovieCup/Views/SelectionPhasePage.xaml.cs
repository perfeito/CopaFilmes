using MovieCup.ViewModels;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieCup.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectionPhasePage : ContentPage
    {
        private SelectionPhaseViewModel ViewModel
        {
            get { return (SelectionPhaseViewModel)BindingContext; }
            set { BindingContext = value; }
        }

        public SelectionPhasePage()
        {
            InitializeComponent();
            ViewModel = new SelectionPhaseViewModel();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel.Items != null && ViewModel.Items.Any() && !ViewModel.NavigatedResult)
                return;
            ViewModel.RefreshCommand.Execute(null);
            ViewModel.NavigatedResult = false;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            Task.Delay(500);            
            if (sender is ListView lv) lv.SelectedItem = null;
        }
    }
}