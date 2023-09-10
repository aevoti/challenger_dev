using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ForumAEVO.Models
{
    [Table("User")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        [JsonIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();//Guid armazena o UUID como número hexadecimal

        [Required]
        [MaxLength(100)]
        [Column("Nome")]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [EmailAddress]//valida Email
        [MaxLength(150)]
        [Column("Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column("Foto")]//validar foto
        public string Foto { get; set; } = string.Empty;

        // O usuário pode ter zero ou muitos comentários
        [JsonIgnore]
        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

        // O usuário pode ter zero ou muitos Topicos
        [JsonIgnore]
        public ICollection<Topico> Topicos { get; set; } = new List<Topico>();
    }
}
