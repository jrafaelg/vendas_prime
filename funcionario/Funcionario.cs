using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace vendas_prime
{
    internal class Funcionario
    {

        public int id;
        public string dataContratacao;

        public Funcionario() { }

        public Funcionario(int id, string dataContratacao)
        {
            this.id = id;
            this.dataContratacao = dataContratacao ?? "now()";
        }

        public int Cadastrar(MySqlConnection conn)
        {
            try
            {
                string insert = $"insert into funcionarios values ({this.id}, STR_TO_DATE('{this.dataContratacao}', '%d/%m/%Y' ))";
                MySqlCommand ComandoSQL = conn.CreateCommand();
                ComandoSQL.CommandText = insert;
                ComandoSQL.ExecuteNonQuery();
                return Convert.ToInt32(ComandoSQL.LastInsertedId);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro - funcionario: " + ex.Message);
            }

        }

        public int Atualizar(MySqlConnection conn)
        {
            try
            {

                string sql = $"UPDATE funcionarios SET dataContratacao = STR_TO_DATE('{this.dataContratacao}', '%d/%m/%Y' ) WHERE  id={this.id};";
                MySqlCommand ComandoSQL = conn.CreateCommand();
                ComandoSQL.CommandText = sql;
                ComandoSQL.ExecuteNonQuery();
                return Convert.ToInt32(ComandoSQL.LastInsertedId);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro - funcionario: " + ex.Message);
            }

        }

        public MySqlDataReader Listar(MySqlConnection conn)
        {

            var sql = new MySqlCommand(
                "SELECT p.id, p.nome, p.cpf, DATE_FORMAT(f.dataContratacao, '%d/%m/%Y') AS dataContratacao FROM pessoas p INNER JOIN funcionarios f ON p.id = f.id",
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
                    $"SELECT p.id, p.nome, p.cpf, DATE_FORMAT(f.dataContratacao, '%d/%m/%Y') AS dataContratacao FROM pessoas p INNER JOIN funcionarios f ON p.id = f.id where p.id = {id}",
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
                string delete = $"delete from funcionarios where id='{id}'";
                MySqlCommand ComandoSQL = conn.CreateCommand();
                ComandoSQL.CommandText = delete;
                ComandoSQL.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro - funcionario: " + ex.Message);
                return false;
            }

        }




    }


}
