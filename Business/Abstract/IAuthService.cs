using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<UserRegisterDto> Register(UserForRegisterDto userForRegisterDto);
        IDataResult<UserLoginResultDto> Login(UserForLoginDto userForLoginDto);
    }
}
