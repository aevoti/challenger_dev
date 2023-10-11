using webapi.Entities;
using webapi.Interface;

namespace webapi.Services
{
    public class ForumService : IForumService
    {
        private List<Topic> _topics = new List<Topic>();
        private List<Comment> _comments = new List<Comment>();
        private int _nextTopicId = 1;
        private int _nextCommentId = 1;

        public List<Topic> GetAllTopics()
        {
            return _topics;
        }

        public Topic GetTopicById(int id)
        {
            return _topics.FirstOrDefault(t => t.Id == id);
        }

        public Topic CreateTopic(Topic topic)
        {
            topic.Id = _nextTopicId++;
            _topics.Add(topic);
            return topic;
        }

        public Topic UpdateTopic(int id, Topic updatedTopic)
        {
            var existingTopic = _topics.FirstOrDefault(t => t.Id == id);
            if (existingTopic == null)
                return null;

            existingTopic.Title = updatedTopic.Title;
            existingTopic.Content = updatedTopic.Content;

            return existingTopic;
        }

        public bool DeleteTopic(int id)
        {
            var existingTopic = _topics.FirstOrDefault(t => t.Id == id);
            if (existingTopic == null)
                return false;

            _topics.Remove(existingTopic);
            return true;
        }

        public Comment CreateComment(int topicId, Comment comment)
        {
            comment.Id = _nextCommentId++;
            comment.TopicId = topicId;
            _comments.Add(comment);
            return comment;
        }

        public Comment UpdateComment(int topicId, int commentId, Comment updatedComment)
        {
            var existingComment = _comments.FirstOrDefault(c => c.Id == commentId && c.TopicId == topicId);
            if (existingComment == null)
                return null;

            existingComment.Content = updatedComment.Content;

            return existingComment;
        }

        public bool DeleteComment(int topicId, int commentId)
        {
            var existingComment = _comments.FirstOrDefault(c => c.Id == commentId && c.TopicId == topicId);
            if (existingComment == null)
                return false;

            _comments.Remove(existingComment);
            return true;
        }
    }
}
