using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GerenciadorGraos.Entidades;

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
                var cmd = new SqlCommand("SELECT Id, Nome, Capacidade, Localizacao FROM Silo", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        silos.Add(new Silo
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Capacidade = reader.GetDecimal(2),
                            Localizacao = reader.GetString(3)
                        });
                    }
                }
            }
            return silos;
        }

        public Silo ObterPorId(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT Id, Nome, Capacidade, Localizacao FROM Silo WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Silo
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Capacidade = reader.GetDecimal(2),
                            Localizacao = reader.GetString(3)
                        };
                    }
                }
            }
            return null;
        }

        public void Adicionar(Silo silo)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO Silo (Nome, Capacidade, Localizacao) VALUES (@Nome, @Capacidade, @Localizacao)", conn);
                cmd.Parameters.AddWithValue("@Nome", silo.Nome);
                cmd.Parameters.AddWithValue("@Capacidade", silo.Capacidade);
                cmd.Parameters.AddWithValue("@Localizacao", silo.Localizacao);
                cmd.ExecuteNonQuery();
            }
        }

        public void Atualizar(Silo silo)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE Silo SET Nome = @Nome, Capacidade = @Capacidade, Localizacao = @Localizacao WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", silo.Id);
                cmd.Parameters.AddWithValue("@Nome", silo.Nome);
                cmd.Parameters.AddWithValue("@Capacidade", silo.Capacidade);
                cmd.Parameters.AddWithValue("@Localizacao", silo.Localizacao);
                cmd.ExecuteNonQuery();
            }
        }

        public void Remover(int id)
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
