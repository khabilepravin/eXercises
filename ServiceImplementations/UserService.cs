using eXercise.Entities;
using System.Threading.Tasks;
using eXercise.ServiceInterfaces;

namespace eXercise.ServiceImplementations
{
    public class UserService : IUserService
    {
        public async Task<User> GetUserAsync()
        {
            await Task.CompletedTask;
            return new User() { Token = "861feec1-23fc-44c1-870c-adf66caa4d9a", Name = "Pravin Khabile" };
        }
    }
}
