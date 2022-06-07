using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos.Question;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IUserQuestionAnswerService _userQuestionAnswerService;

        public QuestionsController(IQuestionService questionService, IUserQuestionAnswerService userQuestionAnswerService)
        {
            _questionService = questionService;
            _userQuestionAnswerService = userQuestionAnswerService;
        }

        [HttpGet("getallquestions")]
        public IActionResult GetAllQuestions()
        {
            var result = _questionService.GetAllQuestions();
            return StatusCode(result.Success ? 200 : 400, result);
        }

        [HttpGet("getresumequestions")]
        public IActionResult GetResumeQuestions()
        {
            var result = _questionService.GetResumeQuestions();
            return StatusCode(result.Success ? 200 : 400, result);
        }

        [HttpGet("getpregnancyquestions")]
        public IActionResult GetPregnancyQuestions()
        {
            var result = _questionService.GetPregnancyQuestions();
            return StatusCode(result.Success ? 200 : 400, result);
        }

        

        [HttpPost("saveanswer")]
        public IActionResult SaveAnswers(List<QuestionWithAnswersDto> answers)
        {
            var result = _userQuestionAnswerService.SaveQuestionAnswers(answers);
            return StatusCode(result.Success ? 200 : 400, result);
        }
    }
}
