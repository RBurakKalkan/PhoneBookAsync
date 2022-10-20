using StackExchange.Redis;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace MessageBroker.Listener
{
    class ReportListener
    {
        public static ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(ConfigurationSettings.AppSettings["Redis"]);
        public void Listen()
        {
            var pubSub = connectionMultiplexer.GetSubscriber();
            pubSub.Subscribe("report-q", (channel, message) => DisplayMessage(message));
            Console.ReadLine();
        }

        private static void DisplayMessage(RedisValue message)
        {
            Console.WriteLine(message);
        }
        public async Task<string> GetJson(string Location)
        {
            var url = ConfigurationSettings.AppSettings["ReportApi"] + "/GetStatisticReport/" + Location;
            var responseString = "";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "PUT";
            request.ContentType = "application/json";

            using (var response = await request.GetResponseAsync())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    responseString = await reader.ReadToEndAsync();
                }
            }

            return responseString;
        }
    }
}
