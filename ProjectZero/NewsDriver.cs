using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections;
using Newtonsoft.Json;


namespace ProjectZero{

    class NewsDriver{ 

        public NewsDriver(){ //empty constructor

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

//Method to use a queryBuilder to build a query string
        public async Task detailedSearch(){
            
            this.client.DefaultRequestHeaders.Add("user-agent", "ProjectZero 0.88");//adding headers to client
            this.client.DefaultRequestHeaders.Add("a-api-key", this.apikey);

            
            StringBuilder url = new();
            url.Append(baseURL + "/everything?");
            string finalQuery = CustomQuery(url);

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, finalQuery);
            var httpResponse = await this.client.SendAsync(httpRequest);

            var json = await httpResponse.Content?.ReadAsStringAsync()!;

            var articleJson = JsonConvert.DeserializeObject<dynamic>(json)!;

            var list = articleJson.articles;


            Console.Clear();
            Console.WriteLine(list.ToString());
            Console.ReadKey();
        }

//Building a custom query string
        public string CustomQuery(StringBuilder sb){
            
            Boolean done = false;
            int selection;

            while(!done)
            {
                Console.Clear();
                Console.WriteLine("Please select from the following options");
                Console.WriteLine("[1] Add keyword (use + and - to denote must and must not).");
                Console.WriteLine("[2] Add domain (eg: techcrunch.com)");
                Console.WriteLine("[3] Add 'from date' (Max 1 month, use YYYY-MM-DD format)");
                Console.WriteLine("[4] Add add language (en=english, es=spanish, etc)");
                Console.WriteLine("[5] Finish and send.");

                selection = Convert.ToInt32(Console.ReadLine());

                switch(selection)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Please enter your keyword");
                        sb.Append("q=" + Console.ReadLine() + "&");
                        break;
                    case 2:
                        Console.Clear(); 
                        Console.WriteLine("Please enter the domain");
                        sb.Append("domains=" + Console.ReadLine() + "&");
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Please enter the date you wish to search from.");
                        sb.Append("from=" + Console.ReadLine() + "&");
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Please enter the language");
                        sb.Append("language=" + Console.ReadLine() + "&");
                        break;
                    case 5:
                        Console.Clear();
                        done = true;
                        break;
                    default:
                        break;
                }

            }

            sb.Append($"sortBy=relevancy&pageSize=10&apiKey={this.apikey}");
            return sb.ToString(); //sending final string
        }

    }
}