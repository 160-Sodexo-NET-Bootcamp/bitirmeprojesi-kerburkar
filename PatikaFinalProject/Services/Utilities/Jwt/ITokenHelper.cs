using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utilities.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user);
    }
}
