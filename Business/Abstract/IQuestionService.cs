using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos.Question;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IQuestionService
    {
        IDataResult<List<QuestionWithAnswersDto>> GetAllQuestions();
        IResult SaveQuestionAnswers(List<QuestionWithAnswersDto> questions);
    }
}
