using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Premium_Project.Service
{
    public class Api_Actu
    {
        public Api_Actu()
            {

        }

        internal Task<List<Article>> GetNews(string countryCode, string apiKey)
        {
            throw new NotImplementedException();
        }

        public class Article
        {
            public Source source { get; set; }
            public string author { get; set; }
            public string title { get; set; }
            public object description { get; set; }
            public string url { get; set; }
            public object urlToImage { get; set; }
            public DateTime publishedAt { get; set; }
            public object content { get; set; }
        }

        public class Root
        {
            public string status { get; set; }
            public int totalResults { get; set; }
            public List<Article> articles { get; set; }
        }

        public class Source
        {
            public string id { get; set; }
            public string name { get; set; }
        }



    }
}
