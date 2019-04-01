using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuizWebHookBot.DTO;

namespace QuizWebHookBot.QuizBackendRequests
{
    public class QuizApi
    {
        private static readonly HttpClient Client = new HttpClient();
        private const string BackendUrl = "https://complexitybot.azurewebsites.net";
        
        public async Task<IEnumerable<TopicInfoDTO>> GetTopics()
        {
            var response = await Client.GetStringAsync($"{BackendUrl}/api/topics");
            
            return JsonConvert.DeserializeObject<IEnumerable<TopicInfoDTO>>(response);
        }
    }
}