using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Dtos.User;

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

        
        public IDataResult<UserLoginResultDto> Login(UserForLoginDto userForLoginDto)
        {
            var user = _userService.GetByIdentificationNumber(userForLoginDto.IdentificationNumber);
            if (user.Data == null)
            {
                return new ErrorDataResult<UserLoginResultDto>(user.Message);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, user.Data.PasswordHash, user.Data.PasswordSalt))
            {
                return new ErrorDataResult<UserLoginResultDto>(Messages.PasswordIncorrect);
            }

            var token = CreateAccessToken(user.Data);
            UserLoginResultDto result = new UserLoginResultDto { AccessToken = token.Token, TokenExpires = token.Expiration, Name = user.Data.Name, Surname = user.Data.Surname, Gsm = user.Data.Gsm, IdentificationNumber = user.Data.IdentificationNumber };
            return new SuccessDataResult<UserLoginResultDto>(result);
        }

        [ValidationAspect(typeof(UserForRegisterValidator))]
        public IDataResult<UserRegisterDto> Register(UserForRegisterDto userForRegisterDto)
        {
            var businessRules = BusinessRules.Run(UserShouldNotExistWithIdentity(userForRegisterDto.IdentificationNumber), UserShouldNotExistWithGsm(userForRegisterDto.Gsm));
            if (businessRules != null) return new ErrorDataResult<UserRegisterDto>(businessRules.Message);
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                IdentificationNumber = userForRegisterDto.IdentificationNumber,
                Name = userForRegisterDto.Name,
                Surname = userForRegisterDto.Surname,
                Gsm = userForRegisterDto.Gsm,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            _userService.Add(user);
            UserRegisterDto result = new UserRegisterDto() { Gsm = user.Gsm, IdentificationNumber = user.IdentificationNumber, Name = user.Name, Surname = user.Surname};
            return new SuccessDataResult<UserRegisterDto>(result, Messages.UserRegistered);
        }

        public AccessToken CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user.Id);
            return _tokenHelper.CreateToken(user, claims.Data);
        }

        #region private methods

        #endregion
        private IResult UserShouldNotExistWithGsm(string gsm)
        {
            if (_userService.GetByGsm(gsm).Success)
            {
                return new ErrorResult(Messages.UserAlreadyExistWithGsm);
            }
            return new SuccessResult();
        }

        private IResult UserShouldNotExistWithIdentity(string idNumber)
        {
            if (_userService.GetByIdentificationNumber(idNumber).Success)
            {
                return new ErrorResult(Messages.UserAlreadyExistWithIdentity);
            }
            return new SuccessResult();
        }






    }
}
