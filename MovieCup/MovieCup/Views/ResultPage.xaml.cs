using MovieCup.Models;
using MovieCup.ViewModels;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieCup.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage
    {
        private ResultViewModel ViewModel
        {
            get { return (ResultViewModel)BindingContext; }
            set { BindingContext = value; }
        }

        public ResultPage(IEnumerable<Movie> movies)
        {
            InitializeComponent();
            ViewModel = new ResultViewModel();
            var items = new Dictionary<string, string>();
            for (int i = 0; i < movies.Count(); i++)
                items.Add($@"{i+1}°", movies.ElementAt(i).Title);
            ViewModel.Items = items;
        }
    }
}