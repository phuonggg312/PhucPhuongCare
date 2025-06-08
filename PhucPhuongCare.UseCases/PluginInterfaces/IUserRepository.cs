using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace PhucPhuongCare.UseCases.PluginInterfaces
{
    public interface IUserRepository // << Đã đổi thành public
    {
        Task<string> GetPatientEmailByIdAsync(string id);
        Task<IdentityUser> GetUserByIdAsync(string id);
    }
}