using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class PoliclinicManager : IPoliclinicService
    {
        private readonly IPoliclinicDal _policlinicDal;

        public PoliclinicManager(IPoliclinicDal policlinicDal)
        {
            _policlinicDal = policlinicDal;
        }

        public IDataResult<Policlinic> GetById(int id)
        {
            var result = _policlinicDal.Get(p=>p.Id == id);
            if (result == null)
            {
                return new ErrorDataResult<Policlinic>();
            }
            return new SuccessDataResult<Policlinic>(result);
        }
    }
}
