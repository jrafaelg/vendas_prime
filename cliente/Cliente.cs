using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace vendas_prime.cliente
{
    internal class Cliente
    {

        public int id;
        public string telefone;
        public string email;

        public Cliente() { }

        public Cliente(int id, string telefone, string email)
        {
            this.id = id;
            this.telefone = telefone;
            this.email = email;
        }

        public int Cadastrar(MySqlConnection conn)
        {
            try
            {
                string insert = $"insert into clientes values ({this.id}, '{this.telefone}', '{this.email}')";
                MySqlCommand ComandoSQL = conn.CreateCommand();
                ComandoSQL.CommandText = insert;
                ComandoSQL.ExecuteNonQuery();
                return Convert.ToInt32(ComandoSQL.LastInsertedId);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro - cliente: " + ex.Message);
            }

        }

        public int Atualizar(MySqlConnection conn)
        {
            try
            {

                string sql = $"UPDATE clientes SET telefone='{this.telefone}', email='{this.email}' WHERE  id={this.id};";
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

        public MySqlDataReader Listar(MySqlConnection conn)
        {

            var sql = new MySqlCommand(
                "SELECT p.id, p.nome, p.cpf, c.telefone, c.email FROM pessoas p INNER JOIN clientes c ON p.id = c.id",
                conn
                );
            var exec = sql.ExecuteReader();

            return exec;

        }

        public MySqlDataReader BuscarById(int id)
        {
            try
            {
                Conexao conectaBD = new Conexao();
                var conn = conectaBD.RealizaConexao();

                var sql = new MySqlCommand(
                    $"SELECT p.id, p.nome, p.cpf, c.telefone, c.email FROM pessoas p INNER JOIN clientes c ON p.id = c.id where p.id = {id}",
                    conn
                    );
                var exec = sql.ExecuteReader();
                exec.Read();
                return exec;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Deletar(MySqlConnection conn, int id)
        {
            try
            {
                string delete = $"delete from clientes where id='{id}'";
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
