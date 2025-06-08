using Microsoft.AspNetCore.Identity;
using PhucPhuongCare.UseCases.PluginInterfaces;
using System.Threading.Tasks;

namespace PhucPhuongCare.DataStore.EFCore.Repositories
{
    public class UserRepository : IUserRepository // << Đã đổi thành public và kế thừa interface
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GetPatientEmailByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user?.Email ?? "Không tìm thấy";
        }

        public async Task<IdentityUser> GetUserByIdAsync(string id)
        {
            // FindByIdAsync có thể trả về null, chúng ta sẽ xử lý ở UseCase/UI
            return await _userManager.FindByIdAsync(id);
        }
    }
}