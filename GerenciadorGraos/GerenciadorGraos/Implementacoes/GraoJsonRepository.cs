using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using GerenciadorGraos.Models;
using GerenciadorGraos.Interfaces;

namespace GerenciadorGraos.Implementacoes
{
    public class GraoJsonRepository : IGraoRepository
    {
        private readonly string _caminhoArquivo;
        private List<Grao> _graos;

        public GraoJsonRepository(string caminhoArquivo = "graos.json")
        {
            _caminhoArquivo = caminhoArquivo;
            _graos = CarregarDoArquivo();
        }

        private List<Grao> CarregarDoArquivo()
        {
            if (!File.Exists(_caminhoArquivo))
                return new List<Grao>();

            var json = File.ReadAllText(_caminhoArquivo);
            return JsonSerializer.Deserialize<List<Grao>>(json) ?? new List<Grao>();
        }

        private void SalvarNoArquivo()
        {
            var json = JsonSerializer.Serialize(_graos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_caminhoArquivo, json);
        }

        public void Adicionar(Grao grao)
        {
            grao.Id = Guid.NewGuid();
            _graos.Add(grao);
            SalvarNoArquivo();
        }

        public void Atualizar(Grao grao)
        {
            var existente = _graos.FirstOrDefault(g => g.Id == grao.Id);
            if (existente != null)
            {
                existente.Nome = grao.Nome;
                existente.NumeroLote = grao.NumeroLote;
                existente.Peso = grao.Peso;
                existente.Quantidade = grao.Quantidade;
                existente.DataEntrega = grao.DataEntrega;
                existente.ValorUnitario = grao.ValorUnitario;
                existente.FornecedorId = grao.FornecedorId;
                SalvarNoArquivo();
            }
        }

        public void Remover(Guid id)
        {
            var grao = _graos.FirstOrDefault(g => g.Id == id);
            if (grao != null)
            {
                _graos.Remove(grao);
                SalvarNoArquivo();
            }
        }

        public Grao? ObterPorId(Guid id)
        {
            return _graos.FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<Grao> ObterTodos()
        {
            return _graos;
        }
    }
}