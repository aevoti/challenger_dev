using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace webapi.Entities
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public List<Comment>? Comments { get; set; }
    }
}
