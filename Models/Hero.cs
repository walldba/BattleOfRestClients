using System.Collections.Generic;

namespace BattleOfRestClients.Models
{
    public class Hero
    {
        public Data data { get; set; }
    }
    public class Thumbnail
    {
        public string path { get; set; }
        public string extension { get; set; }
    }


    public class Url
    {
        public string type { get; set; }
        public string url { get; set; }
    }

    public class Result
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Thumbnail thumbnail { get; set; }
        public string resourceURI { get; set; }
    }

    public class Data
    {
        public List<Result> results { get; set; }
    }


}