using MovieCup.Models;
using MovieCup.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MovieCup.ViewModels
{
    public class SelectionPhaseViewModel : BaseViewModel
    {
        public ICommand RemoveItemCommand { get; private set; }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await ExecuteLoadMoviesCommand();
                });
            }
        }

        public ICommand SendMoviesCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (Items.Count!= 8)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ops...", "Para continuar, selecione 8 filmes!", "OK");
                        return;
                    }
                    if (IsBusy)
                        return;

                    IsBusy = true;
                    
                    var list = new ObservableCollection<Movie>(await DataStore.CheckOutWinnerAsync(Items.ToList()));
                    if (!list.Any())
                        await Application.Current.MainPage.DisplayAlert("Ops...", "Algo não deu certo tente novamente!", "OK");
                    else
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new ResultPage(list));
                        NavigatedResult = true;
                        Items = new ObservableCollection<Movie>();
                        IsRefreshing = true;
                    }
                        
                    IsBusy = false;
                });
            }
        }

        public SelectionPhaseViewModel()
        {
            IsRefreshing = true;
            Title = "Fase de Seleção";
            Items = new ObservableCollection<Movie>();
            RemoveItemCommand = new Command<Movie>(ExecuteRemoveItemCommand);
        }

        private ObservableCollection<Movie> items;
        public ObservableCollection<Movie> Items
        {
            get { return items; }
            set
            {
                if (items != null && items.Equals(value))
                    return;
                SetProperty(ref items, value);
                LegendListMovies = $"Selecionados {Items.Count} de 8 Filmes";
            }
        }

        private string legendListMovies;
        public string LegendListMovies
        {
            get { return legendListMovies; }
            set
            {
                if (legendListMovies != null && legendListMovies.Equals(value))
                    return;
                SetProperty(ref legendListMovies, value);
            }
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                SetProperty(ref _isRefreshing, value);
            }
        }

        public bool NavigatedResult = false;

        void ExecuteRemoveItemCommand(Movie item)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            
            if (Items.Count == 8)
                Application.Current.MainPage.DisplayAlert("Ops...", "Você já selecionou a quantidade correta de filmes!", "OK");
            else
            {
                if (item != null)
                    Items.Remove(item);
                LegendListMovies = $"Selecionados {Items.Count} de 8 Filmes";
            }
            
            IsBusy = false;
        }

        async Task ExecuteLoadMoviesCommand()
        {
            var list = new ObservableCollection<Movie>( await DataStore.GetItemsAsync());
            if (!list.Any())
                await Application.Current.MainPage.DisplayAlert("Ops...", "Algo não deu certo tente novamente!", "OK");
            else
                Items = list;

            IsRefreshing = false;
        }
    }
}
