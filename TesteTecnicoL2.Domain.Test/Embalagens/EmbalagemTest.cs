using TesteTecnicoL2.Domain.Embalagens;
using TesteTecnicoL2.Domain.Produtos;

namespace TesteTecnicoL2.Domain.Test.Embalagens
{
    [TestClass]
    public class EmbalagemTest
    {
        [TestMethod]
        public void SeProdutoTiverDimensoesValidasParaAlgumModeloDeCaixaDeveRetornarEsteModelo()
        {

            Embalagem embalagem = new Embalagem();
            List<Produto> produtos = new List<Produto>();

            Produto produto1 = new Produto("Teclado", 15, 20, 40); 
            produtos.Add(produto1);
            Produto produto2 = new Produto("Mouse", 15, 20, 40); 
            produtos.Add(produto2);
            Produto produto3 = new Produto("CPU", 20, 25, 45);
            produtos.Add(produto3);
            Produto produto4 = new Produto("RAM", 25, 30, 50);
            produtos.Add(produto4);
            Produto produto5 = new Produto("Bios", 30, 35, 55);
            produtos.Add(produto5);
            Produto produto6 = new Produto("Head", 35, 40, 60);
            produtos.Add(produto6);
            Produto produto7 = new Produto("PC", 40, 45, 65);
            produtos.Add(produto7);
            Produto produto8 = new Produto("Controle", 45, 50, 70);
            produtos.Add(produto8);

            embalagem.DistribuiProdutosEmCaixaOtimizandoPorMetrosCubicos(produtos);
            
        }
    }
}
