using System.Text.Json;
using UsingApi;

namespace UsingApi
{
    public class Program
    {
        static async Task Main(string[] args)
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://pokeapi.co/api/v2/berry");
            request.Method = HttpMethod.Get;


            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(result);
            var bResponse = JsonSerializer.Deserialize<BerryResponse>(result);

            List<Berry> bList = new List<Berry>();

            int i = 1;
            foreach (Berry berry in bResponse.results)
            {
                Console.WriteLine(i + ". " + berry.ToString());
                i++;
            }

            Console.WriteLine(bResponse);





        }
    }
}