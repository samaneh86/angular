using AngularEShop.Core.DTOs.Account;
using AngularEShop.Core.Services.Interfaces;
using AngularEShop.Core.Utilities.Convertor;
using AngularEShop.DataLayer.Entities.Account;
using AngularEShop.DataLayer.Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AngularEShop.DataLayer.Entities.Access;
using AngularEShop.Core.Utilities.Common;

namespace AngularEShop.Core.Services.Implementations
{
    public class UserService : IUserService
    {
        private IGenericRepository<User> userRepository;
        private IGenericRepository<UserRole> userRoleRepository;
        public object CryptographyHelperregister { get; private set; }
        private IEmailSender emailSender;
        private IViewRenderService renderView;
        public UserService(IGenericRepository<User> _userRepository, IEmailSender emailSender, IViewRenderService renderView, IGenericRepository<UserRole> userRoleRepository)
        {
            userRepository = _userRepository;
            this.renderView = renderView;
            this.emailSender = emailSender;
            this.userRoleRepository = userRoleRepository;
        }



        public async Task<List<User>> GetAllUsers()
        {
            return await userRepository.GetEntitiesQuery().ToListAsync();
        }

        public async Task<RegisterUserResult> RegisterUser(RegisterUserDTO register)
        {

            if (IsExistsUserByEmail(register.Email))
                return RegisterUserResult.EmailExists;
            var user = new User
            {
                Email = register.Email,
                FirstName = register.FirstName,
                LastName = register.LastName,
                Address = register.Address,
                EmailActivateCode = Guid.NewGuid().ToString(),
                Password = CryptographyHelper.Encrypt(register.Password)
            };
            await userRepository.AddEntity(user);
            await userRepository.SaveChanges();

            var body = renderView.RenderToStringAsync("Email/ActivateAccount", user);
            emailSender.Send("samaneh.vafaeinejad@gmail.com", "ایمیل فعالسازی", body);

            return RegisterUserResult.Success;

        }
        public bool IsExistsUserByEmail(string email)
        {

            return userRepository.GetEntitiesQuery().Any(e => e.Email == email.ToLower().Trim());

        }
        public async Task<LoginUserResult> LoginUser(LoginUserDTO login, bool checkAdminRole = false)
        {

            string password = CryptographyHelper.Encrypt(login.Password);
            var user = await userRepository.GetEntitiesQuery().Where(e => e.Email == login.Email.ToLower().Trim() && e.Password == password).FirstOrDefaultAsync();
            if (user == null)
                return LoginUserResult.IncorrectData;
            if (!user.IsActivated)
                return LoginUserResult.NotActivated;
            if (checkAdminRole)
            {
                
                if (!await IsUserAdmin(user.Id))
                {
                    return LoginUserResult.NotAdmin;
                }
            }

            return LoginUserResult.Success;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await userRepository.GetEntitiesQuery().Where(e => e.Email == email.ToLower().Trim()).FirstOrDefaultAsync();
        }


        public async Task<User> GetUserById(long userId)
        {
            return await userRepository.GetEntityById(userId);
        }
        public async Task<User> GetUserByEmailActivateCode(string emailActiveCode)
        {
            return await userRepository.GetEntitiesQuery().SingleOrDefaultAsync(s => s.EmailActivateCode == emailActiveCode);
        }
        public void ActivateUser(User user)
        {
            user.IsActivated = true;
            user.EmailActivateCode = Guid.NewGuid().ToString();
            userRepository.UpdateEntity(user);
            userRepository.SaveChanges();

        }
        public async Task EditUserInfo(EditUserDTO user, long userId)
        {

            var mainUser = await userRepository.GetEntityById(userId);
            if (mainUser != null)
            {
                mainUser.FirstName = user.FirstName;
                mainUser.LastName = user.LastName;
                mainUser.Address = user.Address;
                userRepository.UpdateEntity(mainUser);
                await userRepository.SaveChanges();
            }
        }

        public async Task<bool> IsUserAdmin(long userId)
        {
            return await userRoleRepository.GetEntitiesQuery()
                .Include(s => s.Role)
                .AsQueryable().AnyAsync(s =>s.UserId == userId && s.Role.Name == "Admin");
         
        }
        public void Dispose()
        {
            userRepository?.Dispose();
        }
    }
}
