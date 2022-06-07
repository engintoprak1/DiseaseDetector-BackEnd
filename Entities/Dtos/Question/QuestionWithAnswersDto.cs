using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.Question
{
    public class QuestionWithAnswersDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public List<AnswerWithSelectionDto> Answers { get; set; }
    }
}
