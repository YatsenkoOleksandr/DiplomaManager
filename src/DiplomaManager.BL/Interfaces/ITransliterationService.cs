namespace DiplomaManager.BLL.Interfaces
{
    public enum TransliterationType
    {
        Gost,
        ISO
    }

    public interface ITransliterationService
    {
        string Front(string text, TransliterationType type = TransliterationType.ISO);

        string Back(string text, TransliterationType type = TransliterationType.ISO);
    }
}
