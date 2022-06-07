using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos.Question;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IQuestionDal : IEntityRepository<Question>
    {
        List<QuestionWithAnswersDto> GetAllQuestionsWithAnswers();

        List<QuestionWithAnswersDto> GetResumeQuestionsWithAnswers();

        List<QuestionWithAnswersDto> GetPregnancyQuestionsWithAnswers();
    }
}
