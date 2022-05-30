using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAnswerService
    {
        IResult Add(Answer answer);

        IDataResult<List<Answer>> GetAnswersWithQuestionId(int id);
    }
}
