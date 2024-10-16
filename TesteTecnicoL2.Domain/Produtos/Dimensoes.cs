namespace TesteTecnicoL2.Domain.Produtos
{
    public class Dimensoes
    {
        private readonly double _altura;
        private readonly double _largura;
        private readonly double _comprimento;
        public Dimensoes(double altura, double largura, double comprimento)
        {
            this._altura = altura;
            this._largura = largura;
            this._comprimento = comprimento;
        }
        public double Altura => _altura;
        public double Largura => _largura;
        public double Comprimento => _comprimento;
        public double MetrosCubicos => Altura * Largura * Comprimento;
    }
}
