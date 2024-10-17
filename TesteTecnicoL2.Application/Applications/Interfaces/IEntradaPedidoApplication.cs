using TesteTecnicoL2.Application.Dtos;

namespace TesteTecnicoL2.Application.Applications.Interfaces
{
    public interface IEntradaPedidoApplication
    {
        Task<List<PedidoSaidaDto>> RealizaProcessamentoDosPedidos(List<PedidoEntradaDto> pedidos);
    }
}
