using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LanceConstrutor
    {
        [Fact]
        public void LancaArgumentoExceptionDadoValorNegativo()
        {
            // Arrange
            var valorNegativo = -100;

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                new Lance(null, valorNegativo);
            });
        }
    }
}
