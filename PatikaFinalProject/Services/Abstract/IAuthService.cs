using Entities.Concrete;
using Entities.Dtos;
using Services.Utilities.Jwt;
using Services.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IAuthService
    {
        Task<IResult> Register(RegisterDto registerDto);
        Task<IDataResult<User>> Login(LoginDto loginDto);
        AccessToken CreateToken(User user);
        Task<User> GetUserByEmail(string email);

    }
}
