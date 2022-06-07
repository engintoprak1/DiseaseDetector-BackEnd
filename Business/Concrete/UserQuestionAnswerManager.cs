using Business.Abstract;
using Business.BusinessAspects.Autofac.Authentication;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Question;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class UserQuestionAnswerManager : IUserQuestionAnswerService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IUserQuestionAnswerDal _userQuestionAnswerDal;

        public UserQuestionAnswerManager(IHttpContextAccessor httpContextAccessor, IUserQuestionAnswerDal userQuestionAnswerDal)
        {
            _httpContextAccessor = httpContextAccessor;
            _userQuestionAnswerDal = userQuestionAnswerDal;
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
