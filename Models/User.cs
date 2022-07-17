using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DVHome.Models
{
    public class User
    {
        [Key]
        public string Username {get; set; }

        public string Password { get; set;}
        public string? Token {get; set;}

    }
}