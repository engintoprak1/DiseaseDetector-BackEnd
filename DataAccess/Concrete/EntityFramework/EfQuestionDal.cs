using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Question;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfQuestionDal : EfEntityRepositoryBase<Question, DiseaseDetectionContext>, IQuestionDal
    {
        public List<QuestionWithAnswersDto> GetAllQuestionsWithAnswers()
        {
            using(DiseaseDetectionContext database = new DiseaseDetectionContext())
            {
                var questions = database.Questions.Select(i=>new QuestionWithAnswersDto() { 
                        Question=i.QuestionString,
                        Answers = database.Answers.Where(j=>j.QuestionId==i.Id).Select(j=> new AnswerWithSelectionDto() { Id=j.Id,Answer=j.AnswerString,IsSelected=false}).ToList()
                }).ToList();
                return questions;
            }
        }
    }
}
