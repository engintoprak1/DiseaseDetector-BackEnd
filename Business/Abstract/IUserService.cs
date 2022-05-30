using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        IResult Add(User user);

        IDataResult<User> GetByGsm(string gsm);

        IDataResult<User> GetByIdentificationNumber(string idNumber);
    }
}
