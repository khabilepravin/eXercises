using eXercise.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eXercise.ServiceInterfaces
{
    public interface IShopperHistoryService
    {
        Task<IEnumerable<ShopperHistory>> GetShopperHistoryAsync();
    }
}
