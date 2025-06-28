using System;
using GerenciadorGraos.Entidades;

namespace GerenciadorGraos.Models
{
    public class Fornecedor : IEntidade
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CnpjOuCaf { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
    }
}