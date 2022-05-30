using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult();
        }

        public IDataResult<User> GetById(int id)
        {
            var result = _userDal.Get(u => u.Id == id);
            if (result == null)
                return new ErrorDataResult<User>();
            return new SuccessDataResult<User>(result);
        }

        public IDataResult<User> GetByGsm(string gsm)
        {
            var result = _userDal.Get(g => g.Gsm == gsm);
            if (result == null)
            {
                return new ErrorDataResult<User>();
            }
            return new SuccessDataResult<User>(result);
        }

        public IDataResult<User> GetByIdentificationNumber(string idNumber)
        {
            var result = _userDal.Get(g => g.IdentificationNumber == idNumber);
            if (result == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFoundWithIdentificationNumber);
            }
            return new SuccessDataResult<User>(result);
        }

        public IDataResult<List<OperationClaim>> GetClaims(int userId)
        {
            if (!GetById(userId).Success)
                return new ErrorDataResult<List<OperationClaim>>();
            var claims = _userDal.GetOperationClaims(userId);
            return new SuccessDataResult<List<OperationClaim>>(claims);
        }
    }
}
