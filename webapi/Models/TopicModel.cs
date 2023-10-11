using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace webapi.Models
{
    public class TopicModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
