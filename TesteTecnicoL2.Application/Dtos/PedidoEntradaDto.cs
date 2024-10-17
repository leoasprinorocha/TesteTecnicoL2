namespace TesteTecnicoL2.Application.Dtos
{
    public class PedidoEntradaDto
    {
        public int pedido_id { get; set; }
        public List<ProdutoDto> produtos { get; set; }
    }
}
