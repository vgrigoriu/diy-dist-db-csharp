using System.Net.Http;
using System.Threading.Tasks;

namespace diydistdb
{
    public static class Node
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task<Thing> GetThing(string url, int id)
        {
            var endpoint = $"{url}/things/{id}";
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            request.Headers.Accept.ParseAdd("application/json");
            var response = await httpClient.SendAsync(request);
            return await response.Content.ReadAsAsync<Thing>();
        }
    }
}
