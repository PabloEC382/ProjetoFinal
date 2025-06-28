using System;
using System.Collections.Generic;

namespace GerenciadorGraos.Interfaces
{
    public interface IRepository<T>
    {
        void Adicionar(T entidade);
        void Remover(Guid id);
        void Atualizar(T entidade);
        T? ObterPorId(Guid id);
        IEnumerable<T> ObterTodos();
    }
}