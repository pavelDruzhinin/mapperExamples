using System;
using System.Collections.Generic;

namespace Common.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SocialDescription { get; set; }
        public string Content { get; set; }
        public DateTime? PublishedUtcDateTime { get; set; }
        public string LinkedPosts { get; set; }
        public string RawContent { get; set; }

        public DateTime LastUpdateUtcDateTime { get; set; }
        public DateTime? DeleteUtcDateTime { get; set; }

        public virtual List<User> WhoHasReads { get; set; }
    }
}