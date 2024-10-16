using System.Collections.Generic;
using TesteTecnicoL2.Domain.Embalagens.CaixasDisponiveis;
using TesteTecnicoL2.Domain.Produtos;

namespace TesteTecnicoL2.Domain.Embalagens
{
    public class Embalagem
    {

        private const double METRO_CUBICOS_CAIXA1 = 96000;
        private const double METRO_CUBICOS_CAIXA2 = 160000;
        private const double METRO_CUBICOS_CAIXA3 = 240000;


        public List<Caixa> DistribuiProdutosEmCaixaOtimizandoPorMetrosCubicos(List<Produto> produtos)
        {
            List<Caixa> caixas = new();

            produtos = produtos.OrderBy(a => a.Dimensoes.MetrosCubicos).ToList();

            var produtosEmpacotadosEmCaixaModelo3 = EmpacotamentoDeProdutosQueCabemEmCaixaModelo3(produtos, ref caixas);
            produtos.RemoveAll(a => produtosEmpacotadosEmCaixaModelo3.Contains(a.ProdutoId));

            var produtosEmpacotadosEmCaixaModelo2 = EmpacotamentoDeProdutosQueCabemEmCaixaModelo2(produtos, ref caixas);
            produtos.RemoveAll(a => produtosEmpacotadosEmCaixaModelo2.Contains(a.ProdutoId));

            var produtosEmpacotadosEmCaixaModelo1 = EmpacotamentoDeProdutosQueCabemEmCaixaModelo1(produtos, ref caixas);
            produtos.RemoveAll(a => produtosEmpacotadosEmCaixaModelo1.Contains(a.ProdutoId));

            TentaDistribuirProdutosSemCaixaEmCaixasComEspacoSobrando(ref produtos, ref caixas);

            CriaNovasCaixasParaProdutosRestantesQueNaoCouberamNasCaixasComEspacosVazios(produtos, ref caixas);

            return caixas;

        }

        private void TentaDistribuirProdutosSemCaixaEmCaixasComEspacoSobrando(ref List<Produto> produtos, ref List<Caixa> caixas)
        {
            if (produtos.Any())
            {
                caixas = caixas.OrderByDescending(a => a.EspacoRestante).ToList();
                foreach (var caixaPedido in caixas)
                {
                    var produtosQueCabemNaCaixa = RetornaProdutosDaListaConsiderandoSomaLimiteDeMetrosCubicos(produtos, caixaPedido.EspacoRestante);
                    if (produtosQueCabemNaCaixa.Any())
                    {
                        caixaPedido.Produtos.AddRange(produtosQueCabemNaCaixa.Select(a => a.ProdutoId));
                        produtos.RemoveAll(a => produtosQueCabemNaCaixa.Select(b => b.ProdutoId).Contains(a.ProdutoId));
                    }
                }
            }


        }

        private void CriaNovasCaixasParaProdutosRestantesQueNaoCouberamNasCaixasComEspacosVazios(List<Produto> produtos, ref List<Caixa> caixas)
        {
            if (produtos.Any())
            {
                var produtosQueCabemEmCaixa1 = RetornaProdutosDaListaConsiderandoSomaLimiteDeMetrosCubicos(produtos, METRO_CUBICOS_CAIXA1);
                if (produtosQueCabemEmCaixa1.Any())
                {
                    Caixa1 caixa1 = new();
                    caixa1.Produtos = produtosQueCabemEmCaixa1.Select(a => a.ProdutoId).ToList();
                    caixas.Add(caixa1);
                    produtos.RemoveAll(a => produtosQueCabemEmCaixa1.Select(b => b.ProdutoId).Contains(a.ProdutoId));
                }

                var produtosQueCabemEmCaixa2 = RetornaProdutosDaListaConsiderandoSomaLimiteDeMetrosCubicos(produtos, METRO_CUBICOS_CAIXA2);
                if (produtosQueCabemEmCaixa1.Any())
                {
                    Caixa2 caixa2 = new();
                    caixa2.Produtos = produtosQueCabemEmCaixa2.Select(a => a.ProdutoId).ToList();
                    caixas.Add(caixa2);
                    produtos.RemoveAll(a => produtosQueCabemEmCaixa2.Select(b => b.ProdutoId).Contains(a.ProdutoId));
                }

                var produtosQueCabemEmCaixa3 = RetornaProdutosDaListaConsiderandoSomaLimiteDeMetrosCubicos(produtos, METRO_CUBICOS_CAIXA3);
                if (produtosQueCabemEmCaixa3.Any())
                {
                    Caixa3 caixa3 = new();
                    caixa3.Produtos = produtosQueCabemEmCaixa2.Select(a => a.ProdutoId).ToList();
                    caixas.Add(caixa3);

                }

            }

        }

        private List<string> EmpacotamentoDeProdutosQueCabemEmCaixaModelo3(List<Produto> produtos, ref List<Caixa> caixasDoPedido)
        {
            List<string> produtosPreparadosCaixa3 = new();
            double metrosCubicosDosProdutos = 0;
            double espacoQueSobrouNaCaixa = 0;

            foreach (var produto in produtos)
            {
                metrosCubicosDosProdutos += produto.Dimensoes.MetrosCubicos;

                if (metrosCubicosDosProdutos <= METRO_CUBICOS_CAIXA3)
                {
                    produtosPreparadosCaixa3.Add(produto.ProdutoId);
                }
                else
                {

                    break;
                }

            }

            if (produtosPreparadosCaixa3.Any())
            {
                espacoQueSobrouNaCaixa = METRO_CUBICOS_CAIXA3 - metrosCubicosDosProdutos;
                Caixa3 caixa3 = new();
                caixa3.Produtos = produtosPreparadosCaixa3;
                caixa3.EspacoRestante = espacoQueSobrouNaCaixa < 0 ? espacoQueSobrouNaCaixa : 0;
                caixasDoPedido.Add(caixa3);
            }

            return produtosPreparadosCaixa3;
        }

        private List<string> EmpacotamentoDeProdutosQueCabemEmCaixaModelo2(List<Produto> produtos, ref List<Caixa> caixasDoPedido)
        {
            List<string> produtosPreparadosCaixa2 = new();
            double metrosCubicosDosProdutos = 0;
            double espacoQueSobrouNaCaixa = 0;

            foreach (var produto in produtos)
            {
                metrosCubicosDosProdutos += produto.Dimensoes.MetrosCubicos;
                if (metrosCubicosDosProdutos <= METRO_CUBICOS_CAIXA2)
                {
                    produtosPreparadosCaixa2.Add(produto.ProdutoId);
                }
                else
                {
                    break;
                }
            }

            if (produtosPreparadosCaixa2.Any())
            {
                espacoQueSobrouNaCaixa = METRO_CUBICOS_CAIXA2 - metrosCubicosDosProdutos;
                Caixa2 caixa2 = new();
                caixa2.Produtos = produtosPreparadosCaixa2;
                caixa2.EspacoRestante = espacoQueSobrouNaCaixa < 0 ? espacoQueSobrouNaCaixa : 0;
                caixasDoPedido.Add(caixa2);
            }
            return produtosPreparadosCaixa2;
        }

        private List<string> EmpacotamentoDeProdutosQueCabemEmCaixaModelo1(List<Produto> produtos, ref List<Caixa> caixasDoPedido)
        {
            List<string> produtosPreparadosCaixa1 = new();
            double metrosCubicosDosProdutos = 0;
            double espacoQueSobrouNaCaixa = 0;

            foreach (var produto in produtos)
            {
                metrosCubicosDosProdutos += produto.Dimensoes.MetrosCubicos;
                if (metrosCubicosDosProdutos <= METRO_CUBICOS_CAIXA1)
                {
                    produtosPreparadosCaixa1.Add(produto.ProdutoId);
                }
                else
                {
                    break;
                }

                if (produtosPreparadosCaixa1.Any())
                {
                    espacoQueSobrouNaCaixa = METRO_CUBICOS_CAIXA2 - metrosCubicosDosProdutos;
                    Caixa1 caixa1 = new Caixa1();
                    caixa1.Produtos = produtosPreparadosCaixa1;
                    caixa1.EspacoRestante = espacoQueSobrouNaCaixa < 0 ? espacoQueSobrouNaCaixa : 0;
                    caixasDoPedido.Add(caixa1);
                }
            }

            return produtosPreparadosCaixa1;
        }

        public List<Produto> RetornaProdutosDaListaConsiderandoSomaLimiteDeMetrosCubicos(List<Produto> produtos, double tamanhoLimite)
        {
            List<Produto> produtosSelecionados = new();
            double metrosCubicosDosProdutos = 0;

            foreach (var produto in produtos)
            {
                if (metrosCubicosDosProdutos + produto.Dimensoes.MetrosCubicos > tamanhoLimite)
                {
                    break;
                }

                produtosSelecionados.Add(produto);
                metrosCubicosDosProdutos += produto.Dimensoes.MetrosCubicos;
            }

            return produtosSelecionados;
        }
    }
}
