using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace vendas_prime.produto
{
    internal class Produto
    {


        public int id;
        public string nome;
        public decimal preco;
        public int? estoque;


        public Produto() { }

        public Produto(string nome, decimal preco, int? estoque)
        {
            this.nome = nome;
            this.preco = preco;
            this.estoque = estoque ?? 0;
        }

        public Produto(int id, string nome, decimal preco, int? estoque)
        {
            this.id = id;
            this.nome = nome;
            this.preco = preco;
            this.estoque = estoque ?? 0;

        }

        public int Cadastrar(MySqlConnection conn)
        {
            try
            {

                string sql = $"insert into produtos values ('{this.id}', '{this.nome}', '{this.preco.ToString().Replace(',', '.')}', '{this.estoque}')";
                MySqlCommand ComandoSQL = conn.CreateCommand();
                ComandoSQL.CommandText = sql;
                ComandoSQL.ExecuteNonQuery();
                return Convert.ToInt32(ComandoSQL.LastInsertedId);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro - produto: " + ex.Message);
            }

        }

        public int Atualizar(MySqlConnection conn)
        {
            try
            {

                string sql = $"UPDATE produtos SET nome='{this.nome}', preco='{this.preco}', estoque='{this.estoque}' WHERE  id={this.id};";
                MySqlCommand ComandoSQL = conn.CreateCommand();
                ComandoSQL.CommandText = sql;
                ComandoSQL.ExecuteNonQuery();
                return Convert.ToInt32(ComandoSQL.LastInsertedId);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro - produto: " + ex.Message);
            }

        }

        public int AtualizarEstoque(MySqlConnection conn)
        {
            try
            {

                string sql = $"UPDATE produtos SET estoque='{this.estoque}' WHERE  id={this.id};";
                MySqlCommand ComandoSQL = conn.CreateCommand();
                ComandoSQL.CommandText = sql;
                ComandoSQL.ExecuteNonQuery();
                return Convert.ToInt32(ComandoSQL.LastInsertedId);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro - atualizar estoque do produto: " + ex.Message);
            }

        }

        public MySqlDataReader Listar(MySqlConnection conn)
        {

            var sql = new MySqlCommand(
                "SELECT p.id, p.nome, p.preco, p.estoque FROM produtos p",
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
                    $"SELECT p.id, p.nome, p.preco, p.estoque FROM produtos p where p.id = {id}",
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
                string sql = $"delete from produtos where id='{id}'";
                MySqlCommand ComandoSQL = conn.CreateCommand();
                ComandoSQL.CommandText = sql;
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
