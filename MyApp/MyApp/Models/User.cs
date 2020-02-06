using SQLite;
using System;

namespace MyApp.Models
{
    [Table("User")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(16)]
        public string Username { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; } 
        public DateTime Age { get; set; }
        public byte[] ProfileImage { get; set; } 
        public byte[] BannerImage { get; set; } 
    }
}
