using System;
using System.Collections.Generic;
using GerenciadorGraos.Models;

namespace GerenciadorGraos.Interfaces
{
    public interface IFornecedorRepository
    {
        void Adicionar(Fornecedor fornecedor);
        void Atualizar(Fornecedor fornecedor);
        void Remover(Guid id);
        Fornecedor? ObterPorId(Guid id);
        IEnumerable<Fornecedor> ObterTodos();
    }
}