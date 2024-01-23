using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Net
{
    public class Policy
    {

       public static async Task Main()
        {
            string rabbitMqBaseUrl = "http://localhost:15672/api/";

            // Replace with your RabbitMQ username and password
            string rabbitMqUsername = "guest";
            string rabbitMqPassword = "guest";

            // Replace with the policy parameters
            //string policyName = "p-max-lenght";
            string policyName = "p-max-length2";
            string queuePattern = "^q4.*$"; // This is a regular expression pattern for queue names
            int maxLength = 2;

            await ApplyQueuePolicy(rabbitMqBaseUrl, rabbitMqUsername, rabbitMqPassword, policyName, queuePattern, maxLength);

            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();
        }

        public static async Task ApplyQueuePolicy(string baseUrl, string username, string password, string policyName, string queuePattern, int maxLength)
        {
            using (var httpClient = new HttpClient())
            {
                // Set RabbitMQ Management API credentials
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));

                // Define the policy settings
                var policySettings = new
                {
                    pattern = queuePattern,
                    definition = new
                    {
                        haMode = "all",         // High availability mode
                        haSyncMode = "automatic",
                        maxLength = maxLength ,   // Maximum length of the queue
                    },
                    applyTo="queue",
                    priority = 0
                };

                // Serialize the policy settings to JSON
                var policyJson = Newtonsoft.Json.JsonConvert.SerializeObject(policySettings);

                // Construct the API URL for applying the policy
                //string apiUrl = $"{baseUrl}policies/vhost/{policyName}";
                string apiUrl = $"{baseUrl}policies/vhost/{policyName}";

                // Send the HTTP PUT request to apply the policy
                var response = await httpClient.PutAsync(apiUrl, new StringContent(policyJson, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Policy '{policyName}' applied successfully to queues matching pattern '{queuePattern}'");
                }
                else
                {
                    Console.WriteLine($"Failed to apply policy. Status code: {response.StatusCode}");
                }
            }
        }
    }
}
