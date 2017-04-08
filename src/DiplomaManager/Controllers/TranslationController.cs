using System.Threading.Tasks;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaManager.Controllers
{
    public class TranslationController : Controller
    {
        private ITranslationService TranslationService { get; }

        public TranslationController(ITranslationService translationService)
        {
            TranslationService = translationService;
        }

        public async Task<TranslationResult> Translate(string text, string lang)
        {
            var result = await TranslationService.Translate(text, lang);
            return result;
        }
    }
}