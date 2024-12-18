using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace vendas_prime.cliente
{
    public partial class FrmCadastraCliente : Form
    {

        public int id;

        public FrmCadastraCliente()
        {
            InitializeComponent();
        }

        public FrmCadastraCliente(int id)
        {

            this.id = id;

            InitializeComponent();

            Cliente cliente = new Cliente();
            var result = cliente.BuscarById(id);

            txtNome.Text = Convert.ToString(result["nome"]);
            txtCpf.Text = Convert.ToString(result["cpf"]); ;
            txtTelefone.Text = Convert.ToString(result["telefone"]); ;
            txtEmail.Text = Convert.ToString(result["email"]);

            btnCadastrar.Text = "Atualizar";
            this.Text = "Atualizar Cliente";

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {

            Conexao conectaBD = new Conexao();
            var conn = conectaBD.RealizaConexao();
            MySqlTransaction tr = conn.BeginTransaction();

            var id = this.id;
            var nome = txtNome.Text;
            var cpf = txtCpf.Text;
            var telefone = txtTelefone.Text;
            var email = txtEmail.Text;


            if (nome != "" && cpf != "" && telefone != "" && email != "")
            {

                try
                {

                    if (id == 0)
                    {
                        Pessoa pessoa = new Pessoa(nome, cpf);
                        int idpessoa = pessoa.Cadastrar(conn);

                        Cliente cliente = new Cliente(idpessoa, telefone, email);
                        int idcliente = cliente.Cadastrar(conn);

                        MessageBox.Show("Cliente cadastrado com sucesso");
                    }
                    else
                    {
                        Pessoa pessoa = new Pessoa(id, nome, cpf);
                        int idpessoa = pessoa.Atualizar(conn);

                        Cliente cliente = new Cliente(id, telefone, email);
                        int idcliente = cliente.Atualizar(conn);

                        MessageBox.Show("Cliente atualizado com sucesso");
                    }

                    tr.Commit();



                }
                catch (Exception ex)
                {

                    MessageBox.Show("Erro no cadastro de cliente: " + ex.Message);
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
