using System.Net.Http;
using System.Threading.Tasks;

namespace diydistdb
{
    public static class Node
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task<Thing> GetThingAsync(string url, int id)
        {
            var endpoint = $"{url}/things/{id}";
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            request.Headers.Accept.ParseAdd("application/json");
            var response = await httpClient.SendAsync(request);
            return await response.Content.ReadAsAsync<Thing>();
        }

        public static async Task<Thing> PutThingAsync(string url, Thing thing)
        {
            var endpoint = $"{url}/things";
            var response = await httpClient.PostAsJsonAsync(endpoint, thing);
            return await response.Content.ReadAsAsync<Thing>();
        }
    }
}
