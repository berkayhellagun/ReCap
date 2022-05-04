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

        public async Task<IDataResult<User>> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            var user = RegisterModule(userForRegisterDto, password);
            await _userService.AsyncAdd(user);
            return new SuccessDataResult<User>(user, "Registered");
        }

        public async Task<IDataResult<User>> Login(UserForLoginDto userForLoginDto)
        {
            var user = await CheckEmail(userForLoginDto);
            if (user == null)
            {
                return new ErrorDataResult<User>("User Not Found");
            }

            var checkPassword = BusinessRules.Run(VerifyPassword(userForLoginDto).Result);
            if (checkPassword != null)
            {
                return new ErrorDataResult<User>("Invalid password.");
            }
            return new SuccessDataResult<User>(user, "Successful Login");
        }

        public async Task<IDataResult<AccessToken>> CreateAccessToken(User user)
        {
            var claims = await _userService.AsyncGetClaims(user);
            var accessToken = await _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, "Access-Token Created");
        }

        public async Task<IResult> UserExists(string email)
        {
            if (await _userService.AsyncGetByMail(email) != null)
            {
                return new ErrorResult("User already exists");
            }
            return new SuccessResult();
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

        private async Task<User> CheckEmail(UserForLoginDto userForLoginDto)
        {
            var result = await _userService.AsyncGetByMail(userForLoginDto.Email);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        private async Task<IResult> VerifyPassword(UserForLoginDto userForLoginDto)
        {
            var userToCheck = await CheckEmail(userForLoginDto);
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
    }
}
