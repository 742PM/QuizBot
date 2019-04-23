using System;
using RestSharp;

namespace HttpRequester
{
    public class Requester
    {
        private readonly string serverUri;

        public Requester(string serverUri)
        {
            this.serverUri = serverUri;
        }

        public string GetTopics()
        {
            var client = new RestClient(serverUri + "/api/topics");
            return SendGetRequest(client, Method.GET);
        }

        public string GetLevels(Guid topicId)
        {
            var client = new RestClient(serverUri + $"/api/{topicId}/levels");
            return SendGetRequest(client, Method.GET);
        }

        public string GetAvailableLevels(Guid userId, Guid topicId)
        {
            var client = new RestClient(serverUri + $"/api/{userId}/{topicId}/availableLevels");
            return SendGetRequest(client, Method.GET);
        }
        

        public string GetCurrentProgress(Guid userId, Guid topicId, Guid levelId)
        {
            var client = new RestClient(serverUri + $"/api/{userId}/{topicId}/{levelId}/currentProgress");
            return SendGetRequest(client, Method.GET);
        }

        public string GetTaskInfo(Guid userId, Guid topicId, Guid levelId)
        {
            var client = new RestClient(serverUri + $"/api/{userId}/{topicId}/{levelId}/task");
            return SendGetRequest(client, Method.GET);
        }

        public string GetNextTaskInfo(Guid userId)
        {
            var client = new RestClient(serverUri + $"/api/{userId}/nextTask");
            return SendGetRequest(client, Method.GET);
        }

        public string GetHint(Guid userId)
        {
            var client = new RestClient(serverUri + $"/api/{userId}/hint");
            return SendGetRequest(client, Method.GET);
        }

        public string SendAnswer(Guid userId, string answer)
        {
            var client = new RestClient(serverUri + $"/api/{userId}/sendAnswer");
            var parameter = new Parameter("application/json", answer, ParameterType.RequestBody);
            return SendGetRequest(client, Method.POST, parameter);
        }
        
        private string SendGetRequest(IRestClient client, Method method, Parameter parameter = null)
        {
            var request = new RestRequest(method);
            if (parameter != null)
                request.AddParameter(parameter);
            var response = client.Execute(request);
            return response.Content;
        }
    }
}