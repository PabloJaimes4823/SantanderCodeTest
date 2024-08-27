using System.Security.Principal;

namespace CodeTestSantander.Models
{
    public class HackerNewsModel
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
        public string Type { get; set; }
        public string By { get; set; }
        public int Time { get; set; }
        public string Text { get; set; }
        public bool Dead { get; set; }
        public int Parent { get; set; }
        public int Poll { get; set; }
        public int[] Kids { get; set; }
        public string Url { get; set; }
        public int Score { get; set; }
        public string Title { get; set; }
        public int[] Parts { get; set; }
        public int Descendants { get; set; }
    }
}
