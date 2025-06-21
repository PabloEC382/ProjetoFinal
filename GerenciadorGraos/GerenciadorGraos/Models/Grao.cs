using System;
using GerenciadorGraos.Entidades; // Corrigido o namespace

namespace GerenciadorGraos.Models
{
    public class Grao : IEntidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public DateTime DataColheita { get; set; }
        public double Quantidade { get; set; }

        public Grao() { }

        public Grao(int id, string nome, string tipo, DateTime dataColheita, double quantidade)
        {
            Id = id;
            Nome = nome;
            Tipo = tipo;
            DataColheita = dataColheita;
            Quantidade = quantidade;
        }
    }
}