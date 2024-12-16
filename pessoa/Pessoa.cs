using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace vendas_prime
{
    internal class Pessoa
    {
        public int id;
        public string nome;
        public string cpf;

        public Pessoa() { }

        public Pessoa(string nome, string cpf)
        {
            this.nome = nome;
            this.cpf = cpf;
        }

        public Pessoa(int id, string nome, string cpf)
        {
            this.id = id;
            this.nome = nome;
            this.cpf = cpf;
        }

        public int Cadastrar(MySqlConnection conn)
        {
            try
            {
                string insert = $"insert into pessoas values ('{this.id}','{this.nome}','{this.cpf}')";
                MySqlCommand ComandoSQL = conn.CreateCommand();
                ComandoSQL.CommandText = insert;
                ComandoSQL.ExecuteNonQuery();
                return Convert.ToInt32(ComandoSQL.LastInsertedId);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro - pessoa: " + ex.Message);
            }

        }

        public int Atualizar(MySqlConnection conn)
        {
            try
            {

                string sql = $"UPDATE pessoas SET nome='{this.nome}',cpf='{this.cpf}' WHERE  id={this.id};";
                MySqlCommand ComandoSQL = conn.CreateCommand();
                ComandoSQL.CommandText = sql;
                ComandoSQL.ExecuteNonQuery();
                return Convert.ToInt32(ComandoSQL.LastInsertedId);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro - pessoa: " + ex.Message);
            }

        }

        public bool Deletar(MySqlConnection conn, int id)
        {
            try
            {
                string delete = $"delete from pessoas where id='{id}'";
                MySqlCommand ComandoSQL = conn.CreateCommand();
                ComandoSQL.CommandText = delete;
                ComandoSQL.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro" + ex.Message);
                return false;
            }

        }


    }
}
