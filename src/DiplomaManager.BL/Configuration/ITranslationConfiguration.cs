namespace DiplomaManager.BLL.Configuration
{
    public interface ITranslationConfiguration
    {
        string BaseUrl { get; set; }
        string TranslateBaseApiPath { get; set; }
        string ApiKey { get; set; }
    }
}