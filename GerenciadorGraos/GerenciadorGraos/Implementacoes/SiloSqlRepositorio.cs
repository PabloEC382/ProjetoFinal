using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using GerenciadorGraos.Entidades;
using GerenciadorGraos.Models;

namespace GerenciadorGraos.Implementacoes
{
    public class SiloSqlRepositorio
    {
        private readonly string _connectionString;

        public SiloSqlRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Silo> ObterTodos()
        {
            var silos = new List<Silo>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT Id, Nome, CapacidadeMaxima, CapacidadeAtual, Temperatura, TipoGrao FROM Silo", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        silos.Add(new Silo
                        {
                            Id = reader.GetGuid(0),
                            Nome = reader.GetString(1),
                            CapacidadeMaxima = reader.GetDouble(2),
                            CapacidadeAtual = reader.IsDBNull(3) ? null : reader.GetDouble(3),
                            Temperatura = reader.GetDouble(4),
                            // Aqui está a correção: nunca atribua null para string não anulável
                            TipoGrao = reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
                        });
                    }
                }
            }
            return silos;
        }

        public Silo? ObterPorId(Guid id)
        {
            Silo? silo = null;
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT Id, Nome, CapacidadeMaxima, CapacidadeAtual, Temperatura, TipoGrao FROM Silo WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        silo = new Silo
                        {
                            Id = reader.GetGuid(0),
                            Nome = reader.GetString(1),
                            CapacidadeMaxima = reader.GetDouble(2),
                            CapacidadeAtual = reader.IsDBNull(3) ? null : reader.GetDouble(3),
                            Temperatura = reader.GetDouble(4),
                            TipoGrao = reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
                        };
                    }
                }
            }
            return silo;
        }

        public void Adicionar(Silo silo)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO Silo (Id, Nome, CapacidadeMaxima, CapacidadeAtual, Temperatura, TipoGrao) VALUES (@Id, @Nome, @CapacidadeMaxima, @CapacidadeAtual, @Temperatura, @TipoGrao)", conn);
                cmd.Parameters.AddWithValue("@Id", silo.Id);
                cmd.Parameters.AddWithValue("@Nome", silo.Nome);
                cmd.Parameters.AddWithValue("@CapacidadeMaxima", silo.CapacidadeMaxima);
                cmd.Parameters.AddWithValue("@CapacidadeAtual", silo.CapacidadeAtual.HasValue ? (object)silo.CapacidadeAtual.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Temperatura", silo.Temperatura);
                // Aqui também: nunca envie null para string não anulável
                cmd.Parameters.AddWithValue("@TipoGrao", string.IsNullOrEmpty(silo.TipoGrao) ? (object)DBNull.Value : silo.TipoGrao);
                cmd.ExecuteNonQuery();
            }
        }

        public void Atualizar(Silo silo)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE Silo SET Nome = @Nome, CapacidadeMaxima = @CapacidadeMaxima, CapacidadeAtual = @CapacidadeAtual, Temperatura = @Temperatura, TipoGrao = @TipoGrao WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", silo.Id);
                cmd.Parameters.AddWithValue("@Nome", silo.Nome);
                cmd.Parameters.AddWithValue("@CapacidadeMaxima", silo.CapacidadeMaxima);
                cmd.Parameters.AddWithValue("@CapacidadeAtual", silo.CapacidadeAtual.HasValue ? (object)silo.CapacidadeAtual.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Temperatura", silo.Temperatura);
                cmd.Parameters.AddWithValue("@TipoGrao", string.IsNullOrEmpty(silo.TipoGrao) ? (object)DBNull.Value : silo.TipoGrao);
                cmd.ExecuteNonQuery();
            }
        }

        public void Remover(Guid id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM Silo WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
