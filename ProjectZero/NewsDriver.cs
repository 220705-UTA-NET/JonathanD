using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Collections;
using Newtonsoft.Json;


namespace ProjectZero{

    class NewsDriver{ 

        public NewsDriver(){
            
        }

        private string apikey = Environment.GetEnvironmentVariable("SECRET_KEY");
        private string baseURL = "https://newsapi.org/v2/";
        private HttpClient client = new HttpClient();

        public static void getTopNews(string key){
            Console.Clear();

            var client = new NewsApiClient(key);
            var response = client.GetTopHeadlines(new TopHeadlinesRequest
            {
                Language = Languages.EN,
                PageSize = 10
            });

            Console.WriteLine("Here are the top 10 articles globally!");

            foreach (var article in response.Articles){ 
                Console.WriteLine("\n"+article.Title);
                Console.WriteLine(article.Description);
                Console.WriteLine(article.Url);
            }

            Console.WriteLine("\n Press enter to continue...");
            Console.ReadLine();//making the articles linger
        }//end getTopNews
        
        public static void searchNews(string key){
            Console.Clear();

            Console.WriteLine("Please enter a search term.");

            string term = "";
            term = Generic.getString(term);

            Console.Clear();

            var client = new NewsApiClient(key);

            var response = client.GetEverything(new EverythingRequest{
                Q = term,
                Language = Languages.EN,
                PageSize = 10,
                SortBy = SortBys.Relevancy,
                From = new DateTime(2022, 7, 16)
            });

            Console.WriteLine("Here are the top 10 articles related to " + term);

            foreach (var article in response.Articles){ 
                Console.WriteLine("\n"+article.Title);
                Console.WriteLine(article.Description);
                Console.WriteLine(article.Url);
            }

            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
        }

        public async Task detailedSearch(){
            
            this.client.DefaultRequestHeaders.Add("user-agent", "ProjectZero 0.88");//adding headers to client
            this.client.DefaultRequestHeaders.Add("a-api-key", this.apikey);



            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"https://newsapi.org/v2/everything?q=bitcoin&apiKey={this.apikey}");
            var httpResponse = await this.client.SendAsync(httpRequest);

            var json = await httpResponse.Content?.ReadAsStringAsync();

            var articleList = JsonConvert.DeserializeObject<dynamic>(json)!;

            var list = articleList.articles;
            
            Console.WriteLine(list);


        }
        

    }
}