using System;
using System.Collections.Generic;

namespace Common.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public bool IsPartner { get; set; }
        public string About { get; set; }
        public bool? IsWithoutAccess { get; set; }
        public int Rating { get; set; }

        public DateTime CreatedUtcDateTime { get; set; }
        public DateTime? DeleteUtcDateTime { get; set; }

        public List<BlogPost> ReadPosts { get; set; }
    }
}