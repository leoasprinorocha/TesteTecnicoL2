using TesteTecnicoL2.Domain.Produtos;

namespace TesteTecnicoL2.Domain.Embalagens
{
    public abstract class Caixa
    {
        public virtual double Altura { get; set; }
        public virtual double Largura { get; set; }
        public virtual double Comprimento { get; set; }
        public virtual string Modelo { get; set; }
        public virtual double EspacoRestante { get; set; }
        public virtual List<string> Produtos { get; set; }
        public virtual string Observacao { get; set; }
    }
}
