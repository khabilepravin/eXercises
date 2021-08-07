using eXercise.Entities;
using System.Threading.Tasks;
using eXercise.ServiceInterfaces;
using ServiceImplementations;
using Microsoft.Extensions.Options;

namespace eXercise.ServiceImplementations
{
    public class UserService : IUserService
    {
        private readonly ExternalServiceSettings _externalServiceSettings;
        public UserService(IOptions<ExternalServiceSettings> options)
        {
            _externalServiceSettings = options.Value;
        }
        public async Task<User> GetUserAsync()
        {   
            await Task.CompletedTask;
            return new User() { Token = _externalServiceSettings.Token, Name = _externalServiceSettings.UserName };
        }
    }
}
