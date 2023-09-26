using mvcproject.Dto;
using mvcproject.Enitiy;
using mvcproject.Repository;

namespace mvcproject.Authservis
{
    public class JwtServis
    {
        private readonly IUserRepository _userRepository;

        public JwtServis(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Login(LoginDto loginDto)
        {
            if (loginDto.Email != null)
            {
                var user = _userRepository.GetUserByEmail(loginDto.Email);
                if (user.Result.Password == loginDto.Password)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        public async Task<User> GetUserByEmail(string email) => await _userRepository.GetUserByEmail(email);
    }

}
