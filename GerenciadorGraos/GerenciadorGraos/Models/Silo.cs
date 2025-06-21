using System;

namespace GerenciadorGraos.Models
{
    public class Silo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double CapacidadeMaxima { get; set; }
        public double QuantidadeAtual { get; set; }
        public string Localizacao { get; set; }

        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }

        public DateTime DataUltimaEntrada { get; set; }
        public DateTime? DataUltimaSaida { get; set; }
    }
}