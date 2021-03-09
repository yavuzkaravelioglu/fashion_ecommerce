using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IBasketRepository
    {
         Task<CustomerBasket> GetBasketAsync(string basketId);
         Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
         //takes instance of CustomerBasket as parameter
         Task<bool> DeleteBasketAsync(string basketId);
         
    }
}
