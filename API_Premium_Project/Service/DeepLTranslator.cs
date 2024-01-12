using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace API_Premium_Project.Service
{
    public class DeepLTranslator
    {
        private const string DeepLApiUrl = "https://api.deepl.com/v2/translate";
        private const string AuthKey = "ce9b1eb7-b1f6-8fde-127b-1a5716785abc";


        public async Task<string> TranslateText(string text, string targetLanguage)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "DeepL-Auth-Key " + AuthKey);

                // Ajoutez la spécification de la langue source (fr pour français)
                var requestBody = $"{{\"text\":[\"{text}\"],\"source_lang\":\"FR\",\"target_lang\":\"{targetLanguage}\"}}";
                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(DeepLApiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return ExtractTranslationFromJson(jsonResponse);
                }
                else
                {
                    // Gérez les erreurs de manière appropriée
                    Console.WriteLine($"Erreur de traduction: {response.StatusCode}");
                    return null;
                }
            }


        }

        private string ExtractTranslationFromJson(string jsonResponse)
        {
            try
            {
                dynamic jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse);

                // Vérifiez si la réponse contient la clé "translations"
                if (jsonObject.translations != null && jsonObject.translations.Count > 0)
                {
                    string translation = jsonObject.translations[0].text;
                    return translation;
                }
                else
                {
                    Console.WriteLine("La réponse JSON ne contient pas de traduction valide.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'extraction de la traduction : {ex.Message}");
                return null;
            }
        }

    }
}
