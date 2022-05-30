using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class HospitalManager : IHospitalService
    {
        private readonly IHospitalDal _hospitalDal;

        public HospitalManager(IHospitalDal hospitalDal)
        {
            _hospitalDal = hospitalDal;
        }

        public IDataResult<Hospital> GetById(int id)
        {
            var result = _hospitalDal.Get(q => q.Id == id);
            if (result == null)
            {
                return new ErrorDataResult<Hospital>();
            }
            return new SuccessDataResult<Hospital>(result);
        }
    }
}
