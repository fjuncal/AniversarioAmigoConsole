using System;
using System.Collections.Generic;
using System.Data;
using aniversario_repository.Entity;
using Npgsql;

namespace aniversario_repository.RepositoryAmigo
{
    public class AmigoRepositorio : IRepositoryAmigo
    {
        private string connectionString =
            "Server=127.0.0.1; port=5432; user id = postgres; password = admin; database=AMIGOCONSOLE_TB; pooling = true";
        
        public void Create(Amigo amigo)
        {
            using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connectionString))
            {
                pgsqlConnection.Open();
                string cmdInserir = String.Format("Insert Into AMIGOCONSOLE_TB(nome,sobrenome,dataAniversario) values('{0}','{1}','{2}')",amigo.Nome,amigo.Sobrenome,amigo.DataAniversario);

                using (NpgsqlCommand pgsqlCommand = new NpgsqlCommand(cmdInserir, pgsqlConnection))
                {
                    pgsqlCommand.ExecuteNonQuery();
                }
                pgsqlConnection.Close();
            }
        }

        public void Update(Amigo amigo, int id)
        {
            using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connectionString))
            {
                pgsqlConnection.Open();

                using (NpgsqlCommand teste = new NpgsqlCommand("update AMIGOCONSOLE_TB set nome= @nome, sobrenome = @sobrenome, dataaniversario = @dataAniversario where id = @id", pgsqlConnection))
                {
                    teste.Parameters.Add(new NpgsqlParameter("id", id));
                    teste.Parameters.Add(new NpgsqlParameter("nome", amigo.Nome));
                    teste.Parameters.Add(new NpgsqlParameter("sobrenome", amigo.Sobrenome));
                    teste.Parameters.Add(new NpgsqlParameter("dataaniversario", amigo.DataAniversario));
                    
                    teste.ExecuteNonQuery();

                }
                pgsqlConnection.Close();

            }
        }

        public void Delete(int id)
        {
            using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connectionString))
            {
                pgsqlConnection.Open();
                string cmdDeletar = String.Format("Delete From AMIGOCONSOLE_TB Where id = '{0}'", id);

                using (NpgsqlCommand pgsqlCommand = new NpgsqlCommand(cmdDeletar, pgsqlConnection))
                {
                    pgsqlCommand.ExecuteNonQuery();
                }
                pgsqlConnection.Close();
            }
        }

        public DataTable GetAmigo(int id)
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connectionString))
            {
                pgsqlConnection.Open();
                string cmdSelecionar = "Select * from AMIGOCONSOLE_TB Where id = " + id;

                using (NpgsqlDataAdapter pgsqlDataAdapter = new NpgsqlDataAdapter(cmdSelecionar, pgsqlConnection))
                {
                    pgsqlDataAdapter.Fill(dt);
                    pgsqlConnection.Close();
                }
                
                
            }

            return dt;
        }

        public DataTable GetAll()
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connectionString))
            {
                pgsqlConnection.Open();
                string cmdListaAmigo = "Select * from AMIGOCONSOLE_TB order by id";

                using (NpgsqlDataAdapter pgsqlDataAdapter = new NpgsqlDataAdapter(cmdListaAmigo, pgsqlConnection))
                {
                    pgsqlDataAdapter.Fill(dt);
                }
                pgsqlConnection.Close();
            }

            return dt;
        }

        public DataTable GetAmigoPalavraChave(String nome)
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connectionString))
            {
                pgsqlConnection.Open();
                string cmdFiltrarPorPalavraChave = "SELECT * FROM AMIGOCONSOLE_TB WHERE nome LIKE" + "'%" + nome + "%'";
                using (NpgsqlDataAdapter pgsqlDataAdapter = new NpgsqlDataAdapter(cmdFiltrarPorPalavraChave, pgsqlConnection))
                {
                    pgsqlDataAdapter.Fill(dt);
                }
                pgsqlConnection.Close();
            }

            return dt;
        }
    }
}