using System.Text.Json.Serialization;

namespace webapi.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string Content { get; set; }
        public int? UsersId { get; set; } 

        public virtual Usuario Users { get; set; }
        [JsonIgnore]
        public Topic? Topic { get; set; } 
    }
}
