using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using webapi.Data;
using webapi.Entities;
using webapi.Mappers;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api")]
    public class ForumController : Controller
    {
        private readonly ForumDbContext _context;

        public ForumController(ForumDbContext context)
        {
            _context = context;
        }

        [HttpGet("forum")]
        public IActionResult GetTopics()
        {
            var topics = _context.Topics.ToList();
            var list = new List<TopicModel>();
            foreach (var topic in topics)
            {
                list.Add(ModelToEntityMapper.FromEntity(topic));
            }
            return Ok(list);
        }

        [HttpGet("topico/{id}")]
        public IActionResult GetTopic(int id)
        {
            // Consulta o tópico com base no ID e inclui os comentários relacionados a ele
            var topic = _context.Topics
                .Include(t => t.Comments)
                .FirstOrDefault(t => t.Id == id);

            if (topic == null)
                return NotFound();

            return Ok(topic);
        }

        [HttpPost("topico")]
        public async Task<IActionResult> CreateTopic([FromBody] TopicModel topicModel)
        {
            if (topicModel == null)
                return BadRequest();

            // Define qualquer lógica adicional antes de adicionar ao banco de dados, se necessário.

            var topic = ModelToEntityMapper.FromModel(topicModel);

            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTopic), new { id = topic.Id }, topic);
        }

        [HttpPut("topico/{id}")]
        public async Task<IActionResult> UpdateTopic(int id, [FromBody] TopicModel updatedTopic)
        {
                      

            try
            {
                var topic = await _context.Topics.FindAsync(id);
                if (topic == null)
                    return NotFound();
                topic.Title = updatedTopic.Title;
                topic.Content = updatedTopic.Content;
                _context.Topics.Update(topic);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Topics.Any(t => t.Id == id))
                    return NotFound();
                else
                    throw;
            }
            return Ok(updatedTopic);
        }

        [HttpDelete("topico/{id}")]
        public async Task<IActionResult> DeleteTopic(int id)
        {
            var topic = await _context.Topics.FindAsync(id);
            if (topic == null)
                return NotFound();

            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("comentario/{idTopico}")]
        public async Task<IActionResult> CreateComment(int idTopico, [FromBody] CommentModel commentsModel)
        {
            if (string.IsNullOrWhiteSpace(commentsModel.Content))
                return BadRequest();

            try
            {
                // Verifique se o tópico com o idTopico existe
                var topic = await _context.Topics.FindAsync(idTopico);
                if (topic == null)
                    return NotFound("Tópico não encontrado");
                var user = await _context.Users.FindAsync(commentsModel.UsersId);
                if (user == null)
                    return NotFound("Usuario não encontrado");
                var comment = ModelToEntityMapper.FromModel(commentsModel);
                comment.TopicId = idTopico;
                // Adicione o comentário ao contexto do EF Core
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                return CreatedAtRoute("GetComment", new { idTopico = idTopico, id = comment.Id }, comment);
            }
            catch (Exception ex)
            {
                // Trate erros de validação ou de chave duplicada aqui, se necessário
                return BadRequest("Erro ao criar o comentário");
            }
        }

        [HttpGet("comentario/{idTopico}/{id}", Name = "GetComment")]
        public IActionResult GetComment(int idTopico, int id)
        {
            // Encontre o tópico com o ID especificado
            var topic = _context.Topics.FirstOrDefault(t => t.Id == idTopico);

            // Verifique se o tópico existe
            if (topic == null)
            {
                return NotFound("Tópico não encontrado");
            }

            // Encontre o comentário com o ID especificado e associado ao tópico
            var comment = _context.Comments.FirstOrDefault(c => c.Id == id && c.TopicId == idTopico);

            // Verifique se o comentário existe
            if (comment == null)
            {
                return NotFound("Comentário não encontrado");
            }

            // Se o tópico e o comentário existirem, retorne o comentário
            return Ok(comment);
        }

        [HttpPut("comentario/{idTopico}/{id}")]
        public async Task<IActionResult> UpdateComment(int idTopico, int id, [FromBody] CommentModel commentsModel)
        {

            if (string.IsNullOrWhiteSpace(commentsModel.Content))
                return BadRequest();

            try
            {
                // Verifique se o tópico com o idTopico existe
                var topic = await _context.Topics.FindAsync(idTopico);
                if (topic == null)
                    return NotFound("Tópico não encontrado");
                var user = await _context.Users.FindAsync(commentsModel.UsersId);
                if (user == null)
                    return NotFound("Usuario não encontrado");

                var comment = await _context.Comments.FindAsync(id);
                if (comment == null)
                    return NotFound("Comentário não encontrado");

                comment.Content = commentsModel.Content;
                _context.Comments.Update(comment);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Comments.Any(c => c.Id == id && c.TopicId == idTopico))
                    return NotFound();
                else
                    throw;
            }

            return Ok(commentsModel);
        }        
        [HttpDelete("comentario/{idTopico}/{id}")]
        public async Task<IActionResult> DeleteComment(int idTopico, int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null || comment.TopicId != idTopico)
                return NotFound();

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
