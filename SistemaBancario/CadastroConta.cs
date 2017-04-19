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
    public partial class CadastroConta : Form
    {
        public int contador;
        public Conexao co;
        string stringConexao;

        public CadastroConta()
        {
            co = new Conexao();
            stringConexao = co.retorna_conexao();
            InitializeComponent();
        }
        

        private void CadastroConta_Load(object sender, EventArgs e)
        {
            
            textBox4.Enabled = false; //desabilito o textbox do cliente para que nao seja incluido nenhum dado no mesmo
            contador = 0;
            
            SqlConnection conexao = new SqlConnection(stringConexao);

            conexao.Open();

            SqlCommand comando = new SqlCommand("SELECT * FROM CONTAS", conexao);
            comando.ExecuteNonQuery();
            int count = comando.ExecuteNonQuery();

            if (count>0)
            {
                contador = (Int32)comando.ExecuteScalar();
            }
            
            
            conexao.Close();


        }

        private void button1_Click(object sender, EventArgs e) // clique no cadastrar
        {
            if (!String.IsNullOrEmpty(textBox4.Text)&& !String.IsNullOrEmpty(textBox1.Text)&& !String.IsNullOrEmpty(textBox2.Text)&&(radioButton1.Checked==true||radioButton2.Checked==true) ) // se todos os campos estiver preenchidos, entra no if do cadastro, caso contrario ele nao deixa cadastra a conta
            {
                SqlConnection conexao = new SqlConnection(stringConexao);

                conexao.Open();

                string tipo;
                if (radioButton1.Checked == true)
                {
                    tipo = "C";
                    
                }
                else
                {
                    tipo = "P";
                }

                SqlTransaction sqlTran = conexao.BeginTransaction(); 
                SqlCommand command = conexao.CreateCommand(); 
                command.Transaction = sqlTran;
                try
                {
                    command.CommandText = "INSERT INTO CONTAS (Agencia, Tipo, Conta, Saldo, IdCliente) VALUES ('" + textBox1.Text + "', '" + tipo + "','" + textBox2.Text + "', '0', '" + textBox3.Text + "' )";
                    command.ExecuteNonQuery();

                    sqlTran.Commit();
                    this.Close();
                    MessageBox.Show("Conta Cadastrada com sucesso");
                }

               catch (SqlException ee)
                {
                    if (ee.Number == 2627) { MessageBox.Show("Dados Duplicados, tente novamente"); } // verifica se nao ha duplicidade de numero de conta e id com o codigo da excecao de duplicidade
                    else
                    {


                        MessageBox.Show("Erro na Conexao");

                    }
                    sqlTran.Rollback();

                }
                conexao.Close();
            }
            else
            {
                MessageBox.Show("Dados Incompletos");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CadastroCliente telaCliente = new CadastroCliente();
            telaCliente.ShowDialog();

            Cliente rec = telaCliente.envia_Cliente() as Cliente; // o cliente é instanciado atraves de um metodo pois este ja retorno um objeto do tipo cliente
            if (!String.IsNullOrEmpty(rec.Nome) && !String.IsNullOrEmpty(rec.IdCliente.ToString())) // verifica se os campos do cliente instanciados estao preenchidos
            {
                textBox3.Text = rec.IdCliente.ToString();
                textBox4.Text = rec.Nome;

            }
            
            
        }

        private void textBox3_Leave(object sender, EventArgs e) // quando o textbox do id do cliente perde o foco é feita uma verificacao no banco se o id do cliente existe
        {
            SqlConnection conexao = new SqlConnection(stringConexao);

            conexao.Open();

            SqlCommand comando = new SqlCommand("SELECT NOME FROM CLIENTES WHERE IdCliente = '" + textBox3.Text.ToString()+"' ", conexao);
            SqlDataReader leitor = comando.ExecuteReader();

            int achou = 0;
            string recebenome = "";
            while (leitor.Read())
            {
                achou = 1;
                recebenome = leitor["Nome"].ToString();
            }
            if (achou ==1)
            {
                textBox4.Text = recebenome;
            }
            else
            {
                textBox3.Clear();
                textBox4.Clear();
            }

            textBox4.Enabled = false;
            conexao.Close();


        }
    }
}
