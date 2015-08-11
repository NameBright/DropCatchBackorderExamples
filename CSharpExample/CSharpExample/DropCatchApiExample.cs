using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSharpExample
{
    class DropCatchApiExample
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                PrintUsage();
                return;
            }


            string accessToken = RetrieveAccessToken();

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.ExpectContinue = false;
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "httpclient/c#");
                httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", accessToken ?? String.Empty));
                httpClient.BaseAddress = new Uri("https://www.dropcatch.com/");

                var request = new BackorderRequest()
                {
                    routingCode = args[1],
                    domains = args.Skip(2).ToList()
                };
                
                string data = JsonConvert.SerializeObject(request);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage result = null;
                if (args[0] == "backorder")
                {
                    result = httpClient.PostAsync("/api/BackorderDomainsApi/BackOrderDomains", content).Result;
                }
                else if (args[0] == "cancel")
                {
                    result = httpClient.PostAsync("/api/BackorderDomainsApi/CancelBackOrders", content).Result;
                }
                else
                {
                    throw new Exception("Invalid command. Expecting 'backorder' or 'cancel'");
                }
                string resultContent = result.Content.ReadAsStringAsync().Result;
                Console.WriteLine(resultContent);
            }
        }

        static string RetrieveAccessToken()
        {
            string accessToken = null;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://api.namebright.com/");

                //TODO: fill in client_id and client_secret below.
                // Your application name will be in the form of "account name:application name". e.g. "MyAccount:MyApp"
                // Go here to manage api applications for your account: https://www.namebright.com/Settings#Api
                var content = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", /* FILL ME IN */ ""),
                    new KeyValuePair<string, string>("client_secret", /* FILL ME IN */ "")
                });
                var result = httpClient.PostAsync("/auth/token", content).Result;
                string resultContent = result.Content.ReadAsStringAsync().Result;
                Console.WriteLine(resultContent);
                var anonType = new { access_token = string.Empty };
                var tokenObject = JsonConvert.DeserializeAnonymousType(resultContent, anonType);
                accessToken = tokenObject.access_token;
            }
            return accessToken;
        }

        static void PrintUsage() 
        {
            Console.WriteLine("CSharpExample.exe cancel|backorder standard|discount domain1[,maxbid] [domain2[,maxbid]] [domain3[,maxbid]] ... [domainn[,maxbid]]");
        }
    }

    public class BackorderRequest
    {
        public string routingCode { get; set; }
        public List<string> domains { get; set; }
    }
}
