using Forum.ConcreteProduct;
using Forum.Product;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Creator
{
    public abstract class ConteudoFactory : Controller
    {
        public abstract Task<ActionResult<IEnumerable<Conteudo>>> BuscarTodoConteudo(int? idTopico);
        public abstract Task<ActionResult<IEnumerable<Conteudo>>> BuscarTodoConteudoTexto(string descricao);
        public abstract Task<ActionResult<Conteudo>> BuscarConteudo(int id);
        public abstract Task<IActionResult> CadastrarConteudo();
        public abstract Task<IActionResult> EditarConteudo(int id);
        public abstract Task<IActionResult> DeletarConteudo(int id);

    }
}
