using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromptBuilder.Services;

namespace PromptBuilder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnglishController : ControllerBase
    {

        private readonly PromptBuilderService _promptBuilder;
    

        public EnglishController(PromptBuilderService promptGeneratorService)
        {
            _promptBuilder = promptGeneratorService;
        }

        [HttpGet("en-vi")]
        public IActionResult GeneratePrompt()
        {
            string prompt = _promptBuilder.GeneratePrompt();
            return Ok(new { Prompt = prompt });
        }
    }
}
