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
            grao.Id = _graos.Any() ? _graos.Max(g => g.Id) + 1 : 1;
            _graos.Add(grao);
            SalvarNoArquivo();
        }

        public void Atualizar(Grao grao)
        {
            var existente = _graos.FirstOrDefault(g => g.Id == grao.Id);
            if (existente != null)
            {
                existente.Nome = grao.Nome;
                existente.Tipo = grao.Tipo;
                existente.DataColheita = grao.DataColheita;
                existente.Quantidade = grao.Quantidade;
                SalvarNoArquivo();
            }
        }

        public void Remover(int id)
        {
            var grao = _graos.FirstOrDefault(g => g.Id == id);
            if (grao != null)
            {
                _graos.Remove(grao);
                SalvarNoArquivo();
            }
        }

        public Grao ObterPorId(int id)
        {
            return _graos.FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<Grao> ObterTodos()
        {
            return _graos;
        }
    }
}