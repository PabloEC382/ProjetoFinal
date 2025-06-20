using System.Collections.Generic;
using GerenciadorGraos.Models;

namespace GerenciadorGraos.Interfaces
{
    public interface IGraoRepository
    {
        void Adicionar(Grao grao);
        void Atualizar(Grao grao);
        void Remover(int id);
        Grao ObterPorId(int id);
        IEnumerable<Grao> ObterTodos();
    }
}