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

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public IActionResult GetAllQuestions()
        {
            var result = _questionService.GetAllQuestions();
            return StatusCode(result.Success ? 200 : 400, result);
        }
        [HttpPost]
        public IActionResult SaveAnswers(List<QuestionWithAnswersDto> answers)
        {
            var result = _questionService.SaveQuestionAnswers(answers);
            return StatusCode(result.Success ? 200 : 400, result);
        }
    }
}
