using Microsoft.AspNetCore.Mvc;

namespace Forum.Product
{
    public abstract class Conteudo
    {
        public abstract int? Id { get; set;  }
        public abstract int IdUsuario { get; set; }
        public abstract DateTime Data { get; set; }
        public abstract string Descricao { get; set; }

    }
}
