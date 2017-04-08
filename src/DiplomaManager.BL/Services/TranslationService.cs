using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DiplomaManager.BLL.Configuration;
using DiplomaManager.BLL.Interfaces;
using Newtonsoft.Json;

namespace DiplomaManager.BLL.Services
{
    public class TranslationService : ITranslationService
    {
        private ITranslationConfiguration Configuration { get; }

        public TranslationService(ITranslationConfiguration translationConfiguration)
        {
            Configuration = translationConfiguration;
        }

        public async Task<TranslationResult> Translate(string text, string lang)
        {
            var resultContent = string.Empty;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Configuration.BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var parameters = CreateApiParameters(text, lang);

                using (var response = await client.GetAsync(GetApiParameters(parameters)))
                {
                    if (response.IsSuccessStatusCode)
                        resultContent = await response.Content.ReadAsStringAsync();
                }
            }

            var result = JsonConvert.DeserializeObject<TranslationResult>(resultContent);
            return result;
        }

        private Dictionary<string, string> CreateApiParameters(string text, string lang)
        {
            var parameters = new Dictionary<string, string>
            {
                {"key", Configuration.ApiKey},
                {"text", text},
                {"lang", lang}
            };
            return parameters;
        }

        private string GetApiParameters(Dictionary<string, string> parameters)
        {
            var builder = new StringBuilder();
            builder.Append($"{Configuration.TranslateBaseApiPath}?");

            foreach (var parametr in parameters)
            {
                builder.Append($"{ parametr.Key }={ parametr.Value }&");
            }

            return builder.ToString();
        }
    }

    public class TranslationResult
    {
        public string Lang { get; set; }
        public IEnumerable<string> Text { get; set; }
    }
}
