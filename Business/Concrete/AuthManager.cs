using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        IUserService _userService;
        ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            var user = RegisterModule(userForRegisterDto, password);
            _userService.Add(user);
            return new SuccessDataResult<User>(user, "Registered");
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var user = CheckEmail(userForLoginDto);
            if (user == null)
            {
                return new ErrorDataResult<User>("User Not Found");
            }

            var checkPassword = BusinessRules.Run(VerifyPassword(userForLoginDto));
            if (checkPassword != null)
            {
                return new ErrorDataResult<User>("Invalid password.");
            }
            return new SuccessDataResult<User>(user, "Successful Login");
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult("User Already Exists");
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>("Access Token Created");
        }

        private User RegisterModule(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            return user;
        }

        private User CheckEmail(UserForLoginDto userForLoginDto)
        {
            var result = _userService.GetByMail(userForLoginDto.Email);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        private IResult VerifyPassword(UserForLoginDto userForLoginDto)
        {
            var userToCheck = CheckEmail(userForLoginDto);
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorResult("Password Error");
            }
            return new SuccessResult();
        }
    }
}
