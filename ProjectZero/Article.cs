namespace ProjectZero{

    class Article{

        //Fields
        public Dictionary<string, string>? source;
        public string? author;
        public string? title;
        public string? description;
        public string? url;
        public string? urlToImage;
        public string? publishedAt;
        public string? content;

        //Constructor
        public Article( string author, string title, string description, string url, string urlToImage, 
        string publishedAt, string content){
            this.author = author;
            this.title = title;
            this.description = description;
            this.url = url;
            this.urlToImage = urlToImage;
            this.publishedAt = publishedAt;
            this.content = content;
        }

    }

}