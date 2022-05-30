using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        [ValidationAspect(typeof(UserForRegisterValidator))]
        public IResult Register(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Name = userForRegisterDto.Name,
                Surname = userForRegisterDto.Surname,
                Gsm = userForRegisterDto.Gsm,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };

            var businessRules = BusinessRules.Run(UserShouldNotExistWithGsm(userForRegisterDto.Gsm));

            if (businessRules != null)
            {
                return businessRules;
            }

            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByIdentificationNumber(userForLoginDto.IdentificationNumber);
            if (userToCheck.Data == null)
            {
                return new ErrorDataResult<User>(userToCheck.Message);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordIncorrect);
            }

            return new SuccessDataResult<User>(userToCheck.Data);
        }

        public IDataResult<UserLoginResultDto> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user.Id);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            UserLoginResultDto result = new UserLoginResultDto { AccessToken = accessToken, Name = user.Name, Surname = user.Surname, Gsm = user.Gsm, IdentificationNumber = user.IdentificationNumber };
            return new SuccessDataResult<UserLoginResultDto>(result, Messages.AccessTokenCreated);
        }

        private IResult UserShouldNotExistWithGsm(string gsm)
        {
            if (_userService.GetByGsm(gsm).Success)
            {
                return new ErrorResult(Messages.UserAlreadyExistWithGsm);
            }
            return new SuccessResult();
        }




    }
}
