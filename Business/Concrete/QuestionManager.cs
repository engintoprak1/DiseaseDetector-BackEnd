using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class QuestionManager : IQuestionService
    {
        private readonly IQuestionDal _questionDal;

        public QuestionManager(IQuestionDal questionDal)
        {
            _questionDal = questionDal;
        }

        public IDataResult<Question> GetById(int id)
        {
            var result = _questionDal.Get(q=>q.Id == id);
            if (result == null)
            {
                return new ErrorDataResult<Question>();
            }
            return new SuccessDataResult<Question>(result);
        }
    }
}
