using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using GerenciadorGraos.Models;

namespace GerenciadorGraos.Implementacoes
{
    public class FornecedorJsonRepository
    {
        private readonly string _filePath = "fornecedores.json";

        public List<Fornecedor> ObterTodos()
        {
            if (!File.Exists(_filePath))
                return new List<Fornecedor>();

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Fornecedor>>(json) ?? new List<Fornecedor>();
        }

        public void SalvarTodos(List<Fornecedor> fornecedores)
        {
            var json = JsonSerializer.Serialize(fornecedores, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public void Adicionar(Fornecedor fornecedor)
        {
            var fornecedores = ObterTodos();
            fornecedor.Id = Guid.NewGuid();
            fornecedores.Add(fornecedor);
            SalvarTodos(fornecedores);
        }

        public void Remover(Guid id)
        {
            var fornecedores = ObterTodos();
            var novoList = fornecedores.Where(f => f.Id != id).ToList();
            SalvarTodos(novoList);
        }

        public void Atualizar(Fornecedor fornecedor)
        {
            var fornecedores = ObterTodos();
            var index = fornecedores.FindIndex(f => f.Id == fornecedor.Id);
            if (index >= 0)
            {
                fornecedores[index] = fornecedor;
                SalvarTodos(fornecedores);
            }
        }

        public Fornecedor? ObterPorId(Guid id)
        {
            return ObterTodos().FirstOrDefault(f => f.Id == id);
        }
    }
}
