using System;
using System.Collections.Generic;
using System.Linq;
using GerenciadorGraos.Models;

namespace GerenciadorGraos.Implementacoes
{
    public class SiloSqlRepositorio
    {
        public void Adicionar(Silo silo)
        {
            using var ctx = new SiloDbContext();
            silo.Id = Guid.NewGuid();
            ctx.Silos.Add(silo);
            ctx.SaveChanges();
        }

        public void Atualizar(Silo silo)
        {
            using var ctx = new SiloDbContext();
            ctx.Silos.Update(silo);
            ctx.SaveChanges();
        }

        public void Remover(Guid id)
        {
            using var ctx = new SiloDbContext();
            var silo = ctx.Silos.Find(id);
            if (silo != null)
            {
                ctx.Silos.Remove(silo);
                ctx.SaveChanges();
            }
        }

        public Silo? ObterPorId(Guid id)
        {
            using var ctx = new SiloDbContext();
            return ctx.Silos.FirstOrDefault(s => s.Id == id);
        }

        public List<Silo> ObterTodos()
        {
            using var ctx = new SiloDbContext();
            return ctx.Silos.ToList();
        }
    }
}
