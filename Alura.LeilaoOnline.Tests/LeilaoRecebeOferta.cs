using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeOferta
    {
        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoClienteRealizouUltimoLance()
        {
            // Arrange
            var leilao = new Leilao("Van Gogh", new MaiorValor());
            var fulano = new Interessada("Fulano", leilao);

            leilao.IniciaPregao();
            leilao.RecebeLance(fulano, 1000);

            // Act
            leilao.RecebeLance(fulano, 1000);

            // Assert
            var quantidadeEsperada = 1;
            var quantidadeObtida = leilao.Lances.Count();
            Assert.Equal(quantidadeEsperada, quantidadeObtida);
        }

        [Theory]
        [InlineData(2, new double[] { 800, 900 })]
        [InlineData(4, new double[] { 800, 900, 1000, 1200 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(
            int quantidadeEsperada, double[] ofertas)
        {
            // Arrange
            var leilao = new Leilao("Van Gogh", new MaiorValor());
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                var cliente = i % 2 == 0 ? fulano : maria;
                leilao.RecebeLance(cliente, valor);
            }

            leilao.TerminaPregao();

            // Act
            leilao.RecebeLance(fulano, 1000);

            // Assert
            var quantidadeObtida = leilao.Lances.Count();
            Assert.Equal(quantidadeEsperada, quantidadeObtida);
        }
    }
}
