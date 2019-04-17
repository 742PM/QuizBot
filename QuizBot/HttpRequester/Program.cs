using System;

namespace HttpRequester
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var requester = new Requester("https://complexitybot.azurewebsites.net");
            Console.WriteLine(requester.GetTopics());
        }
    }
}