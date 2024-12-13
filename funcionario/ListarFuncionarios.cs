using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace vendas_prime.funcionario
{
    public partial class FrmListarFuncionarios : Form
    {
        public FrmListarFuncionarios()
        {
            InitializeComponent();
        }

        private void ListarFuncionarios_Load(object sender, System.EventArgs e)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nome", typeof(string));
            dt.Columns.Add("CPF", typeof(string));
            dt.Columns.Add("dataContratacao", typeof(string));
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

            Funcionario funcionario = new Funcionario();

            Conexao conectaBD = new Conexao();
            var conn = conectaBD.RealizaConexao();
            var list = funcionario.Listar(conn);

            while (list.Read())
            {
                dt.Rows.Add(new object[] { Convert.ToInt16(list["id"]), list["nome"], list["cpf"], list["dataContratacao"], "Atualizar", "Deletar" });
            }

            dgvFuncionarios.DataSource = dt;
        }


        // https://stackoverflow.com/questions/33358438/datagridview-get-row-values

        private void dgvFuncionarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string val = "";
            if (e.RowIndex > -1)
            {
                val = this.dgvFuncionarios[0, e.RowIndex].Value.ToString();

                if (e.ColumnIndex == 4)
                {
                    this.AtualizarFuncionario(Convert.ToInt32(val));
                    this.ListarFuncionarios_Load("", e);
                }

                if (e.ColumnIndex == 5)
                {
                    //MessageBox.Show("Deletar " + val);
                    this.DeletarFuncionario(Convert.ToInt32(val));
                    this.ListarFuncionarios_Load("", e);
                }
            }

        }

        private void AtualizarFuncionario(int id)
        {

            FrmCadastraFuncionario frm = new FrmCadastraFuncionario(id);
            frm.ShowDialog();
        }

        private void DeletarFuncionario(int id)
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

                    Funcionario funcionario = new Funcionario();
                    bool delCliente = funcionario.Deletar(conn, id);

                    tr.Commit();

                    MessageBox.Show("Funcionário excluído!");

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Erro ao excluir funcionário: " + ex.Message);
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
