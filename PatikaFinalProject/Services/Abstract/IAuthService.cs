using Entities.Concrete;
using Entities.Dtos;
using Services.Utilities.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IAuthService
    {
        Task<string> Register(RegisterDto registerDto);
        Task<string> Login(LoginDto loginDto);
        AccessToken CreateToken(User user);
        Task<User> GetUserByEmail(string email);

    }
}
