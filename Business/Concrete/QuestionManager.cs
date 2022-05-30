using Business.Abstract;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Question;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Business.BusinessAspects.Autofac.Authentication;
using System.Linq;

namespace Business.Concrete
{
    public class QuestionManager : IQuestionService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IQuestionDal _questionDal;
        private readonly IUserQuestionAnswerDal _userQuestionAnswerDal;

        public QuestionManager(IHttpContextAccessor httpContextAccessor, IQuestionDal questionDal, IUserQuestionAnswerDal userQuestionAnswerDal)
        {

            _httpContextAccessor = httpContextAccessor;
            _questionDal = questionDal;
            _userQuestionAnswerDal = userQuestionAnswerDal;
        }
        [Authentication]
        public IDataResult<List<QuestionWithAnswersDto>> GetAllQuestions()
        {
            var result = _questionDal.GetAllQuestionsWithAnswers();
            if (result == null)
            {
                return new ErrorDataResult<List<QuestionWithAnswersDto>>();
            }
            return new SuccessDataResult<List<QuestionWithAnswersDto>>(result);
        }

        [Authentication]
        public IResult SaveQuestionAnswers(List<QuestionWithAnswersDto> questions)
        {
            int userId = _httpContextAccessor.HttpContext.User.GetAuthenticatedUserId();
            foreach (var item in questions)
            {
                var selectedAnswer = item.Answers.FirstOrDefault(a => a.IsSelected);
                if (selectedAnswer != null)
                {
                    UserQuestionAnswer answer = new UserQuestionAnswer()
                    {
                        AnswerDate = DateTime.UtcNow,
                        AnswerId = selectedAnswer.Id,
                        UserId = userId
                    };
                    _userQuestionAnswerDal.Add(answer);
                }
            }
            return new SuccessResult(Messages.AnswersSaved);
        }
    }
}
