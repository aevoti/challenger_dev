using webapi.Entities;
using webapi.Models;

namespace webapi.Mappers
{
    public static class ModelToEntityMapper
    {
        public static Comment FromModel(CommentModel model)
        {
            return new Comment
            {
                Content = model.Content,
                UsersId = model.UsersId
            };
        }

        public static Usuario FromModel(UsuarioModel model)
        {
            return new Usuario
            {
                Email = model.Email,
                Foto = model.Foto,
                Name = model.Name
            };
        }

        public static Topic FromModel(TopicModel model)
        {
            return new Topic
            {
                Title = model.Title,
                Content = model.Content
            };
        }
        public static TopicModel FromEntity(Topic entity)
        {
            return new TopicModel
            {
                Id = entity.Id,
                Title = entity.Title,
                Content = entity.Content
            };
        }
    }
}
