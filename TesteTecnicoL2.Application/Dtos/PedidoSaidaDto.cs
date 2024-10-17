namespace TesteTecnicoL2.Application.Dtos
{
    public class PedidoSaidaDto
    {
        public int pedido_id { get; set; }
        public List<CaixaSaidaDto> caixas { get; set; }
    }
}
