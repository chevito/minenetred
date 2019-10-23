using Redmine.library.Models;
using System.Threading.Tasks;

namespace Redmine.library.Services
{
    public interface IUserService
    {
        Task<UserServiceModel> GetCurrentUserAsync(string authKey);
    }
}