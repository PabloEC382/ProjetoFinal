using System;
using GerenciadorGraos.Entidades;

namespace GerenciadorGraos.Models
{
    public class Silo : IEntidade
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public double CapacidadeMaxima { get; set; }
        public double? CapacidadeAtual { get; set; }
        public double Temperatura { get; set; }
        public string TipoGrao { get; set; } = string.Empty;
    }
}