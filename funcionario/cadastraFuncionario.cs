using System;
using System.Globalization;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace vendas_prime.funcionario
{
    public partial class FrmCadastraFuncionario : Form
    {

        public int id;

        public FrmCadastraFuncionario()
        {
            InitializeComponent();
        }

        public FrmCadastraFuncionario(int id)
        {

            this.id = id;

            InitializeComponent();

            Funcionario funcionario = new Funcionario();
            var result = funcionario.BuscarById(id);

            txtNome.Text = Convert.ToString(result["nome"]);
            txtCpf.Text = Convert.ToString(result["cpf"]); ;
            dtpDataContratacao.Text = Convert.ToString(result["dataContratacao"]);

            btnCadastrar.Text = "Atualizar";
            this.Text = "Atualizar Funcionário";

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {

            Conexao conectaBD = new Conexao();
            var conn = conectaBD.RealizaConexao();
            MySqlTransaction tr = conn.BeginTransaction();

            var id = this.id;
            var nome = txtNome.Text;
            var cpf = txtCpf.Text;
            var dataContratacao = DateTime.ParseExact(dtpDataContratacao.Text, "dd'/'MM'/'yyyy", new CultureInfo("pt-BR")).ToString("dd/MM/yyyy");
            //var dt = new Date
            //var dataContratacao = txtDataContratacao.Text;
            //var data = dtpDataContratacao.Text;
            //DateTime dt = DateTime.ParseExact(dateTimePicker1.Text, "dd'/'MM'/'yyyy", new CultureInfo("pt-BR"));

            //MessageBox.Show(data);
            //MessageBox.Show(dataContratacao);


            if (nome != "" && cpf != "" && dataContratacao != "")
            {

                try
                {

                    if (id == 0)
                    {
                        Pessoa pessoa = new Pessoa(nome, cpf);
                        int idpessoa = pessoa.Cadastrar(conn);

                        Funcionario funcionario = new Funcionario(idpessoa, dataContratacao);
                        int idfuncionario = funcionario.Cadastrar(conn);

                        MessageBox.Show("Cliente cadastrado com sucesso");
                    }
                    else
                    {
                        Pessoa pessoa = new Pessoa(id, nome, cpf);
                        int idpessoa = pessoa.Atualizar(conn);

                        Funcionario funcionario = new Funcionario(id, dataContratacao);
                        int idfuncionario = funcionario.Atualizar(conn);

                        MessageBox.Show("Funcionário atualizado com sucesso");
                    }

                    tr.Commit();



                }
                catch (Exception ex)
                {

                    MessageBox.Show("Erro no cadastro de funcionário: " + ex.Message);
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

        private void cadastraFuncionario_Load(object sender, EventArgs e)
        {

        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
