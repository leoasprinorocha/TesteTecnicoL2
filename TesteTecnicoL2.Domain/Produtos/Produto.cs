namespace TesteTecnicoL2.Domain.Produtos
{
    public class Produto
    {
        private readonly string _idProduto;
        
        private readonly Dimensoes _dimensoes;
        public Produto(string idProduto, double altura, double largura, double comprimento)
        {
            this._idProduto = idProduto;
            this._dimensoes = new Dimensoes(altura, largura, comprimento);
        }
        public string ProdutoId => _idProduto;
        public Dimensoes Dimensoes  => _dimensoes;
    }
}
