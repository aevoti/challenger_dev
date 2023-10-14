using Forum.Product;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.ConcreteProduct
{
    [Table("Topico")]
    public class Topico : Conteudo
    {
        [Column("Id")]
        private int? _id;
        [Column("IdUsuario")]
        private int _idUsuario;
        [Column("Data")]
        private DateTime _data;
        [Column("Descricao")]
        private string _descricao;

        public Topico() { }
        public Topico(int? id, int idUsuario, DateTime data, string descricao) 
        {
            this._id = id;
            this._idUsuario = idUsuario;
            this._data = data;
            this._descricao = descricao;
        }
        public override int? Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public override int IdUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }
        public override DateTime Data
        {
            get { return _data; }
            set { _data = value; }
        }
        public override string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
    }
}
