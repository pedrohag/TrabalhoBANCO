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
    public partial class TelaSaldo : Form
    {
        public List<Conta> Contas { get; set; }
        public Conexao co;
        string stringConexao;

        public TelaSaldo()
        {
            Contas = new List<Conta>();
            co = new Conexao();
            stringConexao = co.retorna_conexao();
            InitializeComponent();
        }

        private void TelaSaldo_Load(object sender, EventArgs e)
        {
            textBox3.Focus();
            textBox3.Select();
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            Contas.Clear();
            textBox5.Clear();
            SqlConnection conexao = new SqlConnection(stringConexao);

            conexao.Open();

            SqlCommand comando = new SqlCommand("SELECT CONTAS.TIPO, CONTAS.AGENCIA, CONTAS.CONTA, CONTAS.SALDO, CLIENTES.NOME FROM CONTAS JOIN CLIENTES ON CONTAS.IdCliente = CLIENTES.IdCliente WHERE CONTAS.IdCliente = '" + textBox3.Text.ToString() + "' ", conexao);
            SqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                Conta conta = new Conta();
                

                conta.Tipo = reader["Tipo"].ToString();
                conta.Agencia = reader["Agencia"].ToString();
                conta.Numero = reader["Conta"].ToString();
                conta.Saldo = Convert.ToDecimal(reader["Saldo"]);
                textBox4.Text = reader["Nome"].ToString();

                
                Contas.Add(conta);
            }

            dataGridView1.DataSource = Contas;
            dataGridView1.Columns["IdConta"].Visible = false;
            dataGridView1.Columns["Saldo"].Visible = false;
            dataGridView1.Columns["IdCliente"].Visible = false;

            textBox1.Clear();
            textBox2.Clear();

            conexao.Close();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) // joga as informacoes da linha selecinada na grid, para os textbox da tela
        {
            Conta linha = dataGridView1.SelectedRows[0].DataBoundItem as Conta;
            textBox1.Text = linha.Agencia;
            textBox2.Text = linha.Numero;
            textBox5.Text = linha.Saldo.ToString();
        }
    }
}
