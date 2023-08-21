using AngularEShop.Core.DTOs.Account;
using AngularEShop.DataLayer.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEShop.Core.Services.Interfaces
{
   public interface IUserService:IDisposable
    {
        Task<List<User>> GetAllUsers();
        Task<RegisterUserResult> RegisterUser(RegisterUserDTO register);
        bool IsExistsUserByEmail(string email);
        Task<LoginUserResult> LoginUser(LoginUserDTO login,bool checkAdminRole=false);
        Task<User>  GetUserByEmail(string email);
        Task<User> GetUserById(long userId);
        Task<User> GetUserByEmailActivateCode(string emailActiveCode);
        void ActivateUser(User user);
        Task EditUserInfo(EditUserDTO user, long id);
        Task<bool> IsUserAdmin(long userId);
    }
}
