using SOA_BaiTap.CoreLayer.DTO;
using SOA_BaiTap.CoreLayer.Entities;

namespace SOA_BaiTap.ServiceLayer.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetAllUsersAsync();
        public  Task<User> GetUserByIdAsync(int id) ;

        public  Task<User> GetUserByEmailAsync(string email);

        public  Task AddUserAsync(UserDTO user) ;

        public  Task UpdateUserAsync(int id,UserDTO user);

        public  Task DeleteUserAsync(int id) ;
    }
}
