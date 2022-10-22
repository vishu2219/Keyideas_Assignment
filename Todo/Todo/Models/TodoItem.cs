using Newtonsoft.Json;
using SQLite;

namespace Todo.Models
{
    public class TodoItem
    {
        [PrimaryKey, AutoIncrement]
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        //public string Username { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }


    }
}