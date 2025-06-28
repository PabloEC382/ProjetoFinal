using System;
using System.Collections.Generic;
using GerenciadorGraos.Models;

namespace GerenciadorGraos.Interfaces
{
    public interface IGraoRepository
    {
        void Adicionar(Grao grao);
        void Atualizar(Grao grao);
        void Remover(Guid id);
        Grao? ObterPorId(Guid id);
        IEnumerable<Grao> ObterTodos();
    }
}