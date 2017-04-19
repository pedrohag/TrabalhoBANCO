using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaBancario
{
    public partial class TelaInicio : Form
    {
        public TelaInicio()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) /// click do botao cadastro de conta
        {
            CadastroConta telaCadastro = new CadastroConta(); // chamo a tela de cadastro de conta
            telaCadastro.ShowDialog();//mostro a tela de cadastro de conta
            
        }

        private void button2_Click(object sender, EventArgs e) // click no botao de operacoes
        {
            TelaOperacoes op = new TelaOperacoes();// chamo a tela de operacoes
            op.ShowDialog();// mostro a tela de operacoes


        }
    }
}
