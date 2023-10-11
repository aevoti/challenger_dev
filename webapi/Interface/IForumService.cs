using webapi.Entities;

namespace webapi.Interface
{
    public interface IForumService
    {
        List<Topic> GetAllTopics();
        Topic GetTopicById(int id);
        Topic CreateTopic(Topic topic);
        Topic UpdateTopic(int id, Topic updatedTopic);
        bool DeleteTopic(int id);
        Comment CreateComment(int topicId, Comment comment);
        Comment UpdateComment(int topicId, int commentId, Comment updatedComment);
        bool DeleteComment(int topicId, int commentId);
    }
}
