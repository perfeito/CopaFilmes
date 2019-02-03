using System.Collections.Generic;

namespace MovieCup.ViewModels
{
    public class ResultViewModel : BaseViewModel
    {
        public ResultViewModel()
        {
            Title = "Resultado";
        }

        private Dictionary<string, string> items;
        public Dictionary<string, string> Items
        {
            get { return items; }
            set
            {
                if (items != null && items.Equals(value))
                    return;
                SetProperty(ref items, value);
            }
        }
    }
}
