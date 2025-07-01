using System;
using GerenciadorGraos.Entidades;

namespace GerenciadorGraos.Models
{
    public class Grao : IEntidade
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string NumeroLote { get; set; } = string.Empty;
        public double Peso { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataEntrega { get; set; }
        public decimal ValorUnitario { get; set; }
        public string FornecedorNome { get; set; } = string.Empty;
        public decimal ValorTotalLote { get; set; }

        public void AtualizarValorTotal()
        {
            ValorTotalLote = ValorUnitario * Quantidade;
        }
    }
}