using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConverterStringFormat.Helper
{
    public class SimpleWebClient
    {
        public static SimpleWebClient Instance => new Lazy<SimpleWebClient>().Value;

        public async Task<T> RequestAsync<T>(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(url);

                    if (response != null)
                    {
                        string json = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<T>(json);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("*debug: " + ex.ToString());
                }

                return (T)Convert.ChangeType(null, typeof(T));
            }
        }
    }
}
