using System;

namespace GerenciadorGraos.Entidades
{
    public interface IEntidade
    {
        Guid Id { get; set; }
    }

    public class Cliente : IEntidade
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
    }

    public class Categoria : IEntidade
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
    }

    public class Veiculo : IEntidade
    {
        public Guid Id { get; set; }
        public string Placa { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public int Ano { get; set; }
        public string Cor { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = new Categoria();
    }

    public class Funcionario : IEntidade
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
    }

    public class Locacao : IEntidade
    {
        public Guid Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = new Cliente();
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; } = new Veiculo();
        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; } = new Funcionario();
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public decimal ValorTotal { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class Pagamento : IEntidade
    {
        public Guid Id { get; set; }
        public int LocacaoId { get; set; }
        public Locacao Locacao { get; set; } = new Locacao();
        public DateTime DataPagamento { get; set; }
        public decimal ValorPago { get; set; }
        public string FormaPagamento { get; set; } = string.Empty;
    }
}
