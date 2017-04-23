using System;

namespace Common.Dto
{
    public class AnotherUserDto
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public bool IsPartner { get; set; }
        public string About { get; set; }
        public bool? IsWithoutAccess { get; set; }
        public int Rating { get; set; }

        public DateTime CreatedUtcDateTime { get; set; }
        public DateTime? DeleteUtcDateTime { get; set; }
    }
}