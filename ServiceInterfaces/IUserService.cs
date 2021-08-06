using eXercise.Entities;
using System.Threading.Tasks;

namespace eXercise.ServiceInterfaces
{
    public interface IUserService
    {
        Task<User> GetUserAsync();
    }
}
