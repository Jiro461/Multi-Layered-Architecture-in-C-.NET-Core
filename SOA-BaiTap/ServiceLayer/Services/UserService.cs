using SOA_BaiTap.CoreLayer.DTO;
using SOA_BaiTap.CoreLayer.Entities;
using SOA_BaiTap.RepositoryLayer;
using SOA_BaiTap.RepositoryLayer.Interfaces;
using SOA_BaiTap.ServiceLayer.Services;

namespace SOA_BaiTap.ServiceLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync() => await _userRepository.GetAllAsync();

        public async Task<User> GetUserByIdAsync(int id) => await _userRepository.GetByIdAsync(id);

        public async Task<User> GetUserByEmailAsync(string email) => await _userRepository.GetUserByEmailAsync(email);

        public async Task AddUserAsync(UserDTO user) => await _userRepository.AddAsync(new User { UserName = user.UserName, Email = user.Email });

        public async Task UpdateUserAsync(int id, UserDTO user) {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null) return;
            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
            await _userRepository.UpdateAsync(existingUser);
        }

        public async Task DeleteUserAsync(int id) => 
            await _userRepository.DeleteAsync(id);
    }
}
