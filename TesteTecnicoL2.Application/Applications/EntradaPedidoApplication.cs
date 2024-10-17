using TesteTecnicoL2.Application.Applications.Interfaces;
using TesteTecnicoL2.Application.Dtos;
using TesteTecnicoL2.Domain.Embalagens;
using TesteTecnicoL2.Domain.Produtos;

namespace TesteTecnicoL2.Application.Applications
{
    public class EntradaPedidoApplication : IEntradaPedidoApplication
    {
        public async Task<List<PedidoSaidaDto>> RealizaProcessamentoDosPedidos(List<PedidoEntradaDto> pedidos)
        {
            List<PedidoSaidaDto> pedidosSaida = new();

            foreach (var pedido in pedidos)
            {
                PedidoSaidaDto pedidoSaida = new();
                Embalagem embaladorDeProdutos = new();
                List<Caixa> caixasComProdutosEmbalados = embaladorDeProdutos.DistribuiProdutosEmCaixaOtimizandoPorMetrosCubicos(ConverteProdutoDtoParaProdutoDomain(pedido.produtos));
                pedidoSaida.pedido_id = pedido.pedido_id;
                pedidoSaida.caixas = ConverteCaixaDomainParaCaixaSaidaDto(caixasComProdutosEmbalados);
                pedidosSaida.Add(pedidoSaida);
            }

            return pedidosSaida;
        }

        private List<Produto> ConverteProdutoDtoParaProdutoDomain(List<ProdutoDto> produtoDtos)
        {
            List<Produto> produtosDomain = new();
            foreach (var produto in produtoDtos)
            {
                Produto produtoDomain = new(produto.produto_id, produto.dimensoes.altura, produto.dimensoes.largura, produto.dimensoes.comprimento);
                produtosDomain.Add(produtoDomain);
            }

            return produtosDomain;
        }

        private List<CaixaSaidaDto> ConverteCaixaDomainParaCaixaSaidaDto(List<Caixa> caixas)
        {
            List<CaixaSaidaDto> caixasDto = new();
            foreach (var caixa in caixas)
            {
                CaixaSaidaDto caixaSaidaDto = new() { caixa_id = caixa.Modelo, produtos = caixa.Produtos, observacao = caixa.Observacao };
                caixasDto.Add(caixaSaidaDto);
            }

            return caixasDto;
        }
    }
}
