using System;
using System.Windows.Forms;
using vendas_prime.cliente;
using vendas_prime.funcionario;
using vendas_prime.produto;

namespace vendas_prime
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void funcionarioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cadastrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCadastraCliente frm = new FrmCadastraCliente();
            frm.ShowDialog();
        }

        private void listarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListarClientes frm = new frmListarClientes();
            frm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void cadastrarFuncionarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCadastraFuncionario frm = new FrmCadastraFuncionario();
            frm.ShowDialog();
        }

        private void listarFuncionarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListarFuncionarios frm = new FrmListarFuncionarios();
            frm.ShowDialog();
        }

        private void cadastrarProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCadastraProduto frm = new FrmCadastraProduto();
            frm.ShowDialog();
        }
    }
}
