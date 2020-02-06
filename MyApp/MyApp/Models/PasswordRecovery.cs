using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Models
{
    [Table("PasswordRecovery")]
    public class PasswordRecovery
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public DateTime CurrentTime { get; set; }
    }
}
