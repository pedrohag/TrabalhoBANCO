using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaBancario
{
    public partial class CadastroCliente : Form
    {
        public Conexao co;
        string stringConexao;

        public CadastroCliente()
        {
            co = new Conexao();
            stringConexao = co.retorna_conexao();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox2.Text))
            {

                SqlConnection conexao = new SqlConnection(stringConexao);

                conexao.Open();

                SqlTransaction sqlTran = conexao.BeginTransaction(); 
                SqlCommand command = conexao.CreateCommand(); 
                command.Transaction = sqlTran;

                try
                {
                    command.CommandText = "INSERT INTO Clientes (Nome) VALUES ('" + textBox2.Text + "' )";
                    command.ExecuteNonQuery();

                    sqlTran.Commit();
                    this.Close();
                    MessageBox.Show("Cliente Cadastrado com sucesso");
                }
                catch (SqlException ee)
                {
                    MessageBox.Show("Erro na Conexao");
                    sqlTran.Rollback();

                }
                conexao.Close();
                
            }
            else
            {
                MessageBox.Show("Dados Incompletos");
            }
        }

        private void CadastroCliente_Load(object sender, EventArgs e)
        {
            SqlConnection conexao = new SqlConnection(stringConexao);

            conexao.Open();
            SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM Clientes ;", conexao); // faz um select que traz a quantidade de linhas na tabela de clientes
            Int32 cont = (Int32)comando.ExecuteScalar(); // variavel cont recebe o retorno do select
            cont += 1; // somo +1 para designar ao ID do novo cliente que estou cadastrando, apenas para mostrar ao usuario
            textBox1.Text = cont.ToString();
            textBox1.Enabled = false; // desabilida o textbox do id pois o mesmo serve apenas para exibicao
            conexao.Close();

        }
        public object envia_Cliente() // retorna um objeto do tipo CLiente para a tela de cadastro de Conta
        {
            Cliente teste = new Cliente();
            teste.IdCliente = Convert.ToInt32(textBox1.Text);
            teste.Nome = textBox2.Text;
            return teste;
        }

    }
}
