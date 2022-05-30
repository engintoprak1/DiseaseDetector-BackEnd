using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Answer : IEntity
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public string AnswerString { get; set; }
    }
}
