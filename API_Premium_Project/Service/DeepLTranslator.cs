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
        private const string DeepLApiUrl = "https://api.deepl.com/v2/translate"; // URL de l'API
        private const string AuthKey = "ce9b1eb7-b1f6-8fde-127b-1a5716785abc"; // Clé API


        public async Task<string> TranslateText(string text, string targetLanguage) // Fonction qui permet de traduire un texte
        {
            using (HttpClient client = new HttpClient()) // On instancie un nouveau client HTTP
            {
                client.DefaultRequestHeaders.Add("Authorization", "DeepL-Auth-Key " + AuthKey); // Ajoutez la clé d'authentification à l'en-tête de la requête

                // Ajoutez la spécification de la langue source (fr pour français)
                var requestBody = $"{{\"text\":[\"{text}\"],\"source_lang\":\"FR\",\"target_lang\":\"{targetLanguage}\"}}"; // On définit le corps de la requête
                var content = new StringContent(requestBody, Encoding.UTF8, "application/json"); // On définit le contenu de la requête

                var response = await client.PostAsync(DeepLApiUrl, content); // On récupère la réponse de l'API

                if (response.IsSuccessStatusCode) // Si la réponse est bonne
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync(); // On récupère le contenu de la réponse
                    return ExtractTranslationFromJson(jsonResponse);    // On retourne la traduction
                }
                else
                {
                    // Gérez les erreurs de manière appropriée
                    Console.WriteLine($"Erreur de traduction: {response.StatusCode}");
                    return null;
                }
            }


        }

        private string ExtractTranslationFromJson(string jsonResponse) // Fonction qui permet d'extraire la traduction du JSON
        { 
            try
            {
                dynamic jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse); // On désérialise le JSON

                // Vérifiez si la réponse contient la clé "translations"
                if (jsonObject.translations != null && jsonObject.translations.Count > 0)
                {
                    string translation = jsonObject.translations[0].text; // On récupère la traduction
                    return translation; // On retourne la traduction
                }
                else
                {
                    Console.WriteLine("La réponse JSON ne contient pas de traduction valide."); // On retourne une erreur
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
