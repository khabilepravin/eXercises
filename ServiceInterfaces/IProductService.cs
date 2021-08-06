using eXercise.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eXercise.ServiceInterfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetSortedProductsAsync(string sortOption);
    }
}
