using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieCup.Services
{
    public interface IDataStore<T>
    {
        Task<IEnumerable<T>> GetItemsAsync();
        Task<IEnumerable<T>> CheckOutWinnerAsync(List<T> items);
    }
}
