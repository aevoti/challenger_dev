using Newtonsoft.Json;
namespace Models
{
    public class Retorno
    {
        private int codigo;
        private string mensagem;
        private object dados;
        private string dadosstr;

        public int Codigo { get => codigo; set => codigo = value; }
        public string Mensagem { get => mensagem; set => mensagem = value; }
        public object Dados { get => dados; set => dados = value; }
        public string Dadosstr { get => dadosstr; set => dadosstr = value; }

        public Retorno() { }
        public Retorno(int cod, string msg, object obj) { this.codigo = cod; this.mensagem = msg; this.dados = obj; }
        public Retorno(int cod, string msg, string dadosstr) { this.codigo = cod; this.mensagem = msg; this.dadosstr = dadosstr; }
        public Retorno(int cod, string msg) { this.codigo = cod; this.mensagem = msg; }
        public Retorno(int cod, object obj) { this.codigo = cod; this.dados = obj; }

        public override string ToString() { return Newtonsoft.Json.JsonConvert.SerializeObject(this); }
    }
}
