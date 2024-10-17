namespace TesteTecnicoL2.Domain.Embalagens.CaixasDisponiveis
{
    public class Caixa1 : Caixa
    {
        public override double Altura { get; set; } = 30;
        public override double Largura { get; set; } = 40;
        public override double Comprimento { get; set; } = 80;
        public override string Modelo => nameof(Caixa1).Insert(5, " ");

    }

    public class Caixa2 : Caixa
    {
        public override double Altura { get; set; } = 80;
        public override double Largura { get; set; } = 50;
        public override double Comprimento { get; set; } = 40;
        public override string Modelo => nameof(Caixa2).Insert(5, " ");

    }

    public class Caixa3 : Caixa
    {
        public override double Altura { get; set; } = 50;
        public override double Largura { get; set; } = 80;
        public override double Comprimento { get; set; } = 60;
        public override string Modelo { get; set; } = nameof(Caixa3).Insert(5, " ");
    }
}
