using System;

namespace GerenciadorGraos.Entidades
{
    public interface IEntidade
    {
        int Id { get; set; }
    }

    public class Cliente : IEntidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
    }

    public class Categoria : IEntidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }

    public class Veiculo : IEntidade
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public int Ano { get; set; }
        public string Cor { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }

    public class Funcionario : IEntidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Cargo { get; set; }
    }

    public class Locacao : IEntidade
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }
        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public decimal ValorTotal { get; set; }
        public string Status { get; set; }
    }

    public class Pagamento : IEntidade
    {
        public int Id { get; set; }
        public int LocacaoId { get; set; }
        public Locacao Locacao { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorPago { get; set; }
        public string FormaPagamento { get; set; }
    }
}
