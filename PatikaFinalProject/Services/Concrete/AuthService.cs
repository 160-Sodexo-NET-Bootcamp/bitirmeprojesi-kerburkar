using Data.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Services.Abstract;
using Services.Helpers;
using Services.Utilities.Jobs;
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
            //kullanıcının mail adresinin kontrolü için;
            var mailExist = await _unitOfWork.Users.GetAsync(q => q.Email == registerDto.Email);
            if (mailExist != null)
            {
                return new ErrorResult("Mail Adresi Kayıtlıdır.");
            }

            //hash'lenmiş şifte ve salt için;
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

            //oluşturulan kullanıcının veri tabanına gönderilmesi için;
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveAsync();

            //gönderilen kullanıcı mailine hangfire üzerinden hoş geldiniz maili gönderilmesi için;
            SendMailJob.SendMailEnqueue(new Utilities.Services.Models.MailRequest()
            {
                ToEmail = registerDto.Email,
                Subject = "Hoş Geldiniz.",
                Body = "Kayıt olduğunuz için teşekkürler. Hoş Geldiniz! :)"
            });
            return new SuccessResult("Kaydınız Oluşturuldu.");

        }
        //giriş işlemleri için;
        public async Task<IDataResult<User>> Login(LoginDto loginDto)
        {
            //gelen mail'i veritabanından kontrol edilmesi için;
            var user = await _unitOfWork.Users.GetAsync(q=>q.Email==loginDto.Email);
            if (user == null)
            {
                return new ErrorDataResult<User>(null, "Kullanıcı Bulunamadı.");
            }
            //gelen şifre ile o kullanıcının veritabanındaki passwordHash ve passwordSalt değerleri karşılaştırılamsı için;
            var checkPassword = HashingHelper.VerifyPasswordHash(loginDto.Password,user.PasswordHash,user.PasswordSalt);
            if (!checkPassword)
            {
                //kullanıcı tablosunda minumum 2 hatalı giriş yapılmışsa bu da 3. olacağı için hesabı kitlenecek.
                if (user.FailedLoginCount >= 2)
                {
                    await BlockUser(user);
                    SendMailJob.SendMailEnqueue(new Utilities.Services.Models.MailRequest()
                    {
                        ToEmail = user.Email,
                        Subject = "Hesabınız Kitlenmiştir.",
                        Body = "Şifrenizi 3 Kez Yanlış Girdiniz. Hesabınız kitlenmiştir. :("
                    });
                    return new ErrorDataResult<User>(null, "Hesabınız Kitlenmiştir.");
                }
                else
                {
                    await UpdateFailedCount(user);
                }
                return new ErrorDataResult<User>(null, "Şifre Yanlış.");
            }
            if (user.FailedLoginCount != 0)
            {
                await ResetFailedCount(user);
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

        //kullanıcı şifre kontrolü için;
        private async Task BlockUser(User user)
        {
            user.IsBlocked = true;
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveAsync();
        }

        //kullanıcı yanlış girişlerini güncellemek için;
        private async Task UpdateFailedCount(User user)
        {

            user.FailedLoginCount = user.FailedLoginCount + 1;
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveAsync();
        }

        //kullanıcı şifresini doğru girdiğin hatalı giriş sayısını sıfırlamak için;
        private async Task ResetFailedCount(User user)
        {
            user.FailedLoginCount = 0;
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveAsync();
        }

    }
}
