using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using GerenciadorGraos.Models;

namespace GerenciadorGraos.Implementacoes
{
    public class SiloJsonRepository
    {
        private readonly string _filePath = "silos.json";

        public List<Silo> ObterTodos()
        {
            if (!File.Exists(_filePath))
                return new List<Silo>();

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Silo>>(json) ?? new List<Silo>();
        }

        public void SalvarTodos(List<Silo> silos)
        {
            var json = JsonSerializer.Serialize(silos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public void Adicionar(Silo silo)
        {
            var silos = ObterTodos();
            silo.Id = Guid.NewGuid();
            silos.Add(silo);
            SalvarTodos(silos);
        }

        public void Remover(Guid id)
        {
            var silos = ObterTodos();
            var silo = silos.FirstOrDefault(s => s.Id == id);
            if (silo != null)
            {
                silos.Remove(silo);
                SalvarTodos(silos);
            }
        }

        public void Atualizar(Silo silo)
        {
            var silos = ObterTodos();
            var index = silos.FindIndex(s => s.Id == silo.Id);
            if (index >= 0)
            {
                silos[index] = silo;
                SalvarTodos(silos);
            }
        }

        public Silo? ObterPorId(Guid id)
        {
            return ObterTodos().FirstOrDefault(s => s.Id == id);
        }
    }
}