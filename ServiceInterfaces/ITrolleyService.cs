using eXercise.Entities;
using System.Threading.Tasks;

namespace eXercise.ServiceInterfaces
{
    public interface ITrolleyService
    {
        public Task<decimal> GetTrolleyTotalAsync(TrolleyRequest trolleyRequest);
    }
}
