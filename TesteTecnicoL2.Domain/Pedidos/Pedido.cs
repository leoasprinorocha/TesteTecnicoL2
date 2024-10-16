using TesteTecnicoL2.Domain.Produtos;

namespace TesteTecnicoL2.Domain.Pedidos
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public IList<Produto> Produtos { get; set; }
    }
}
