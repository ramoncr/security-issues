using Newtonsoft.Json.Linq;
using Noteing.API.Models;

namespace Noteing.API.Services
{
    public class SummarizeService
    {
        private const string CHOICES_PROPERTY = "choices";
        private const string MESSAGE_PROPERTY = "message";
        private const string CONTENT_PROPERTY = "content";
        private const string MODEL_PROPERTY = "model";

        // LLM gebruiken om iets aan te sturen?  System prompt
        public async Task<Summary> SummarizeMessage(string message, string summaryType)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "<API_KEY>");
            httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer 846f191ad3f083d76048571cb20652d8911445317d00bc20bbed3073978c716f");

            var response = await httpClient.PostAsync("https://api.openai.com/v1/chat", new StringContent("{\"model\":\"gpt-3.5-turbo\",\"messages\": [  {\"role\": \"system\",\"content\": \"You are a mad scientist, that is very skilled in summarizing introduction of spell books. The summaries provided should be of the ofllowing type: " + summaryType + "\"  },  {\"role\": \"user\",\"content\": \"Can you summarize the following message? The message: " + message + "\"  }]  }"));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadFromJsonAsync<JObject>();
            return new Summary
            {
                ModelName = content[MODEL_PROPERTY].ToString(),
                Content = content.GetValue(CHOICES_PROPERTY)[0][MESSAGE_PROPERTY][CONTENT_PROPERTY].ToString()
            };
        }
    }
}
