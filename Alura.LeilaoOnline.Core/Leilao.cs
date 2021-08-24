using System;
using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public enum EstadoLeilao
    {
        AntesDoPregao,
        EmAndamento,
        Finalizado
    }

    public class Leilao
    {
        private Interessada _ultimoCliente;
        private IList<Lance> _lances;
        private readonly IModalidadeAvaliacao _avaliador;

        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        public EstadoLeilao Estado { get; set; }

        public Leilao(string peca, IModalidadeAvaliacao modalidadeAvaliacao)
        {
            Peca = peca;
            _avaliador = modalidadeAvaliacao;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.AntesDoPregao;
        }

        private bool LanceAceito(Interessada cliente, double valor)
        {
            return (Estado == EstadoLeilao.EmAndamento)
                && (cliente != _ultimoCliente);
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if (LanceAceito(cliente, valor))
            {
                _lances.Add(new Lance(cliente, valor));
                _ultimoCliente = cliente;
            }
        }

        public void IniciaPregao()
        {
            Estado = EstadoLeilao.EmAndamento;
        }

        public void TerminaPregao()
        {
            if (Estado != EstadoLeilao.EmAndamento)
            {
                throw new InvalidOperationException("Não é possível terminar o pregão sem que ele tenha começado");
            }

            Ganhador = _avaliador.Avalia(this);

            Estado = EstadoLeilao.Finalizado;
        }
    }
}
