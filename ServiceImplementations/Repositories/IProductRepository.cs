using eXercise.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceImplementations.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}
