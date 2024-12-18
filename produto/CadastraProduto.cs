using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace vendas_prime.produto
{
    public partial class FrmCadastraProduto : Form
    {

        public int id;

        public FrmCadastraProduto()
        {
            InitializeComponent();
        }

        public FrmCadastraProduto(int id)
        {

            this.id = id;

            InitializeComponent();

            Produto produto = new Produto();
            var result = produto.BuscarById(id);

            txtNome.Text = Convert.ToString(result["nome"]);
            txtPreco.Text = Convert.ToString(result["preco"]); ;
            txtEstoque.Text = Convert.ToString(result["estoque"]); ;

            btnCadastrar.Text = "Atualizar";
            this.Text = "Atualizar Produto";

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {

            Conexao conectaBD = new Conexao();
            var conn = conectaBD.RealizaConexao();
            MySqlTransaction tr = conn.BeginTransaction();

            var id = this.id;
            var nome = txtNome.Text;
            var preco = Convert.ToDecimal(txtPreco.Text);
            int? estoque = Convert.ToInt16(txtEstoque.Text);

            estoque = estoque ?? 0;


            if (nome != "" && preco != 0)
            {

                try
                {

                    if (id == 0)
                    {

                        Produto produto = new Produto(nome, preco, estoque);
                        int idproduto = produto.Cadastrar(conn);

                        MessageBox.Show("Produto cadastrado com sucesso");
                    }
                    else
                    {

                        Produto produto = new Produto(id, nome, preco, estoque);
                        int idproduto = produto.Atualizar(conn);

                        MessageBox.Show("Produto atualizado com sucesso");
                    }

                    tr.Commit();

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Erro no cadastro de produto: " + ex.Message);
                    tr.Rollback();

                }
                finally
                {
                    conn.Close();
                }

            }
            else
            {
                MessageBox.Show("todos os campos são obrigatórios");
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
