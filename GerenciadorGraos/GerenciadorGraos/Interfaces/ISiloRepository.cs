using System;
using System.Collections.Generic;
using GerenciadorGraos.Models;

namespace GerenciadorGraos.Interfaces
{
    public interface ISiloRepository
    {
        void Adicionar(Silo silo);
        void Atualizar(Silo silo);
        void Remover(Guid id);
        Silo? ObterPorId(Guid id);
        IEnumerable<Silo> ObterTodos();
    }
}