using Redmine.library.Models;
using System.Threading.Tasks;

namespace Redmine.library.Services
{
    public interface IUserService
    {
        Task<UserResponse> GetCurrentUserAsync(string authKey);
    }
}