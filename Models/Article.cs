using System;
using System.Collections.Generic;

namespace MyBlogApp.Models
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Tag Tag { get; set; }
        public List<Comment> Сomments { get; set; }
    }
}
