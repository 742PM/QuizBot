using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;

namespace QuizRequestService
{
    public class Requester : IQuizService
    {
        private readonly string serverUri;

        public Requester(string serverUri)
        {
            this.serverUri = serverUri;
        }

        public IEnumerable<TopicDTO> GetTopics()
        {
            var client = new RestClient(serverUri + "/api/topics");
            var content = SendGetRequest(client, Method.GET);
            var topics = JsonConvert.DeserializeObject<List<TopicDTO>>(content);
            return topics;
        }

        public IEnumerable<LevelDTO> GetLevels(Guid topicId)
        {
            var client = new RestClient(serverUri + $"/api/{topicId}/levels");
            var content = SendGetRequest(client, Method.GET);
            var levels = JsonConvert.DeserializeObject<List<LevelDTO>>(content);
            return levels;
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

        public TaskDTO GetTaskInfo(Guid userId, Guid topicId, Guid levelId)
        {
            var client = new RestClient(serverUri + $"/api/{userId}/{topicId}/{levelId}/task");
            var content = SendGetRequest(client, Method.GET);
            var task = JsonConvert.DeserializeObject<TaskDTO>(content);
            return task;
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

        public bool? SendAnswer(Guid userId, string answer)
        {
            var client = new RestClient(serverUri + $"/api/{userId}/sendAnswer");
            var parameter = new Parameter("application/json", answer, ParameterType.RequestBody);
            var content = SendGetRequest(client, Method.POST, parameter);
            switch (content)
            {
                case "true":
                    return true;
                case "false":
                    return false;
            }
            return null;
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