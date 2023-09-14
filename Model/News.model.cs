using System;
namespace HackerNews.Model
{
    public class News
    {
        public string Title
        {
            get;
            set;
        }
        public string URL
        {
            get;
            set;
        }
        public string PostedBy
        {
            get;
            set;
        }
        public DateTimeOffset Time
        {
            get;
            set;
        }
        public int Score
        {
            get;
            set;
        }
        public int CommentCount
        {
            get;
            set;
        }
    }
}
