using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaBancario
{
    public class ContaPoupanca : Conta
    {

        public override decimal Saca(decimal valor)
        {
            if (valor > 1000.00m)
            {
                MessageBox.Show("Valor Máximo para Saque para Este tipo de Conta é de R$ 1000,00");
                return 0;
            }
            else
            {
                valor = valor + 0.10m;
                //return base.Saca(valor);
                return valor;
            }
            
            
        }

    }
}
