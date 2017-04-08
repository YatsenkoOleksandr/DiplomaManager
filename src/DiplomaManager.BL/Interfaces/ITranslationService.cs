using System.Threading.Tasks;
using DiplomaManager.BLL.Services;

namespace DiplomaManager.BLL.Interfaces
{
    public interface ITranslationService
    {
        Task<TranslationResult> Translate(string text, string lang);
    }
}