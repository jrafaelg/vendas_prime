using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace vendas_prime.cliente
{
    public partial class frmListarClientes : Form
    {
        public frmListarClientes()
        {
            InitializeComponent();
        }

        private void ListarClientes_Load(object sender, System.EventArgs e)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nome", typeof(string));
            dt.Columns.Add("CPF", typeof(string));
            dt.Columns.Add("Telefone", typeof(string));
            dt.Columns.Add("E-mail", typeof(string));
            dt.Columns.Add("Ação", typeof(string));
            dt.Columns.Add("Ação2", typeof(string));

            //List<Cliente> listCliente = new List<Cliente>();


            /**
             *  https://stackoverflow.com/questions/67890952/c-sharp-mysql-populate-listt
            var result = carro.ListarLista();
            
            lstResultado.Items.Clear();
            while (result.Read())
            {
                lstResultado.Items.Add($"{result["car_nome"]} placa: {result["car_nome"]}");
            }
            */

            Cliente cliente = new Cliente();

            try
            {
                Conexao conectaBD = new Conexao();
                var conn = conectaBD.RealizaConexao();
                var list = cliente.Listar(conn);

                while (list.Read())
                {
                    dt.Rows.Add(new object[] { Convert.ToInt16(list["id"]), list["nome"], list["cpf"], list["telefone"], list["email"], "Atualizar", "Deletar" });
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("erro: " + ex.Message);
            }


            dgvClientes.DataSource = dt;
        }


        // https://stackoverflow.com/questions/33358438/datagridview-get-row-values

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string val = "";
            if (e.RowIndex > -1)
            {
                val = this.dgvClientes[0, e.RowIndex].Value.ToString();

                if (e.ColumnIndex == 5)
                {
                    this.AtualizarCliente(Convert.ToInt32(val));
                    this.ListarClientes_Load("", e);
                }

                if (e.ColumnIndex == 6)
                {
                    //MessageBox.Show("Deletar " + val);
                    this.DeletarCliente(Convert.ToInt32(val));
                    this.ListarClientes_Load("", e);
                }
            }

        }

        private void AtualizarCliente(int id)
        {

            FrmCadastraCliente frm = new FrmCadastraCliente(id);
            frm.ShowDialog();
        }

        private void DeletarCliente(int id)
        {
            DialogResult resultDialog = MessageBox.Show("Confirmar Exclusão",
                "Deletar?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultDialog == DialogResult.Yes)
            {

                Conexao conectaBD = new Conexao();
                var conn = conectaBD.RealizaConexao();
                MySqlTransaction tr = conn.BeginTransaction();

                try
                {

                    Pessoa pessoa = new Pessoa();
                    bool delPessoa = pessoa.Deletar(conn, id);

                    Cliente cliente = new Cliente();
                    bool delCliente = cliente.Deletar(conn, id);

                    tr.Commit();

                    MessageBox.Show("Cliente excluído!");

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Erro ao excluir cliente: " + ex.Message);
                    tr.Rollback();

                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
