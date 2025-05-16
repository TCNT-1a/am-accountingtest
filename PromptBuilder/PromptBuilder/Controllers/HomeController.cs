using Microsoft.AspNetCore.Mvc;
using PromptBuilder.Services;

namespace PromptBuilder.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            
            ViewData["vocabularies"] = PromptBuilderService.VocabularyTopics;
            return View();
        }
    }
}
