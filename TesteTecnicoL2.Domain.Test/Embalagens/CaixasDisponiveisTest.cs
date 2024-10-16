using System.Reflection;

namespace TesteTecnicoL2.Domain.Test.Embalagens
{
    [TestClass]
    public class CaixasDisponiveisTest
    {
        [DataTestMethod]
        [DataRow(30, 40, 80, 1)]
        [DataRow(80, 50, 40, 2)]
        [DataRow(50, 80, 60, 3)]
        public void AoSolicitarNovaCaixaDimensoesDevemEstarDeAcordo(double altura, double largura,double comprimento, int modeloCaixa)
        {
            string nomeClasse = $"TesteTecnicoL2.Domain.Embalagens.Caixa{modeloCaixa}";
            Assembly domainAssembly = Assembly.Load("TesteTecnicoL2.Domain");
            
            Type caixa = domainAssembly.GetType(nomeClasse);
            if (caixa != null)
            {
                object instance = Activator.CreateInstance(caixa);
                FieldInfo alturaCaixa = caixa.GetField("Altura");
                FieldInfo larguraCaixa = caixa.GetField("Largura");
                FieldInfo comprimentoCaixa = caixa.GetField("Comprimento");

                Assert.AreEqual(Convert.ToDouble(alturaCaixa.GetValue(null)), altura);
                Assert.AreEqual(Convert.ToDouble(larguraCaixa.GetValue(null)), largura);
                Assert.AreEqual(Convert.ToDouble(comprimentoCaixa.GetValue(null)), comprimento);

            }
            
        }
    }
}
