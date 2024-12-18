using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace vendas_prime.produto
{
    public partial class FrmListarProdutos : Form
    {
        public FrmListarProdutos()
        {
            InitializeComponent();
        }

        private void ListarProdutos_Load(object sender, System.EventArgs e)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nome", typeof(string));
            dt.Columns.Add("Preço", typeof(string));
            dt.Columns.Add("Estoque", typeof(string));
            dt.Columns.Add("Ação", typeof(string));
            dt.Columns.Add("Ação2", typeof(string));

            Produto produto = new Produto();

            Conexao conectaBD = new Conexao();
            var conn = conectaBD.RealizaConexao();
            var list = produto.Listar(conn);

            while (list.Read())
            {
                dt.Rows.Add(new object[] { Convert.ToInt16(list["id"]), list["nome"], list["preco"], list["estoque"], "Atualizar", "Deletar" });
            }

            dgvProdutos.DataSource = dt;
        }

        private void dgvProdutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string val = "";
            if (e.RowIndex > -1)
            {
                val = this.dgvProdutos[0, e.RowIndex].Value.ToString();

                if (e.ColumnIndex == 4)
                {
                    this.AtualizarProduto(Convert.ToInt32(val));
                    this.ListarProdutos_Load("", e);
                }

                if (e.ColumnIndex == 5)
                {
                    this.DeletarProduto(Convert.ToInt32(val));
                    this.ListarProdutos_Load("", e);
                }
            }

        }

        private void AtualizarProduto(int id)
        {

            FrmCadastraProduto frm = new FrmCadastraProduto(id);
            frm.ShowDialog();
        }

        private void DeletarProduto(int id)
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

                    Produto produto = new Produto();
                    bool delProduto = produto.Deletar(conn, id);

                    tr.Commit();

                    MessageBox.Show("Produto excluído!");

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Erro ao excluir produto: " + ex.Message);
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
