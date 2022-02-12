using Data.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Services.Abstract;
using Services.Helpers;
using Services.Utilities.Jwt;
using Services.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    //kayıt işlemleri için;
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenHelper _tokenHelper;
        public AuthService(IUnitOfWork unitOfWork, ITokenHelper tokenHelper)
        {
            _unitOfWork = unitOfWork;
            _tokenHelper = tokenHelper;
        }

        public async Task<IResult> Register(RegisterDto registerDto)
        {
            var mailExist = await _unitOfWork.Users.GetAsync(q => q.Email == registerDto.Email);
            if (mailExist != null)
            {
                return new ErrorResult("Mail Adresi Kayıtlıdır.");
            }
            HashingHelper.CreatePasswordHash(registerDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User()
            {
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IsBlocked = false
            };
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveAsync();
            return new SuccessResult("Kaydınız Oluşturuldu.");
            //NOT: MAİL GÖNDERİLECEK.
        }
        //giriş işlemleri için;
        public async Task<IDataResult<User>> Login(LoginDto loginDto)
        {
            var user = await _unitOfWork.Users.GetAsync(q=>q.Email==loginDto.Email);
            if (user == null)
            {
                return new ErrorDataResult<User>(null, "Kullanıcı Bulunamadı.");
            }
            var checkPassword = HashingHelper.VerifyPasswordHash(loginDto.Password,user.PasswordHash,user.PasswordSalt);
            if (!checkPassword)
            {
                return new ErrorDataResult<User>(null, "Şifre Yanlış.");
            }
            return new SuccessDataResult<User>(user, "Başarıyla Giriş Yaptınız.");
            
        }
        public AccessToken CreateToken(User user)
        {
            var accessToken = _tokenHelper.CreateToken(user);
            return accessToken;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _unitOfWork.Users.GetAsync(q => q.Email == email);
            return user;
        }

    }
}
