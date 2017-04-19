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
    public partial class TelaOperacoes : Form 
    {
        public List<Conta> Contas { get; set; } // variavel da lista que sera inserida na grid
        public Conexao co;
        string stringConexao;

        public TelaOperacoes()
        {
            Contas = new List<Conta>(); // instancio a lista de contas
            co = new Conexao();
            stringConexao = co.retorna_conexao();
            InitializeComponent();
        }

        private void TelaOperacoes_Load(object sender, EventArgs e)
        {
            carrega();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TelaSaque sa = new TelaSaque(this);
            sa.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TelaDepo de = new TelaDepo(this);
            de.ShowDialog();
            
            
        }

        public void carrega()
        {
            this.dataGridView1.DataSource = null; // aqui ele limpa a grid
            this.dataGridView1.Rows.Clear();// aqui ele limpa a grid
            this.Contas.Clear(); // aqui ele limpa a lista de conta
            SqlConnection conexao = new SqlConnection(stringConexao);

            conexao.Open();

            SqlCommand comando = new SqlCommand("Select * from Contas", conexao);
            SqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                Conta conta = new Conta();
                conta.IdConta = Convert.ToInt32(reader["Id"]);
                conta.Numero = reader["Conta"].ToString();
                conta.Agencia = reader["Agencia"].ToString();
                conta.Saldo = Convert.ToDecimal(reader["Saldo"]);
                conta.Tipo = reader["Tipo"].ToString();
                conta.IdCliente = Convert.ToInt32(reader["IdCliente"]);
                Contas.Add(conta); // adiciona a conta na lista de conta
            }

            dataGridView1.DataSource = Contas; // aqui ele preenche a grid com a nova lista de conta
            dataGridView1.ReadOnly = true; // deixo a grid como apenas visualizacao
            conexao.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            TelaSaldo sa = new TelaSaldo();
            sa.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TelaTran tt = new TelaTran(this);
            tt.ShowDialog();
        }
    }
}
