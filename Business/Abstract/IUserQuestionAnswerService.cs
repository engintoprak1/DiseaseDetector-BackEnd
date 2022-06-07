using Core.Utilities.Results;
using Entities.Dtos.Question;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IUserQuestionAnswerService
    {
        IResult SaveQuestionAnswers(List<QuestionWithAnswersDto> questions);
    }
}
