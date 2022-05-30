using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AnswerManager : IAnswerService
    {
        private readonly IAnswerDal _answerDal;

        public AnswerManager(IAnswerDal answerDal)
        {
            _answerDal = answerDal;
        }

        public IResult Add(Answer answer)
        {
            _answerDal.Add(answer);
            return new SuccessResult();
        }

        public IDataResult<List<Answer>> GetAnswersWithQuestionId(int id)
        {
            var answers = _answerDal.GetAll(q=>q.QuestionId == id);
            return new SuccessDataResult<List<Answer>>(answers);
        }
    }
}
