using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace vendas_prime
{
    internal class Conexao
    {
        public string server;
        public string uid;
        public string password;
        public string database;
        private MySqlConnection conn;

        public Conexao()
        {
            this.server = "localhost";
            this.uid = "root";
            this.password = "";
            this.database = "vendas_ifsp";
        }

        public MySqlConnection RealizaConexao()
        {
            conn = null;

            try
            {
                var str = $"server={this.server}; uid={this.uid}; database={this.database}; password={this.password}";
                conn = new MySqlConnection(str);
                conn.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("erro: " + ex.Message);
                //throw;
            }
            return conn;
        }
    }
}
