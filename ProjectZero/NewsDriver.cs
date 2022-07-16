using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;

namespace ProjectZero{

    class NewsDriver{ 

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




    }
}