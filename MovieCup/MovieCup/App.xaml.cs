using MovieCup.Services;
using MovieCup.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MovieCup
{
    public partial class App : Application
    {
        public static string AzureBackendUrl = "https://moviecupfunction.azurewebsites.net";
        public static bool UseMockDataStore = false;

        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();

            MainPage = new NavigationPage(new SelectionPhasePage()) { BarBackgroundColor = Color.FromHex("#6E6E6E"), BarTextColor = Color.White};
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
