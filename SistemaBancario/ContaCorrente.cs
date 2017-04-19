using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBancario
{
    public class ContaCorrente: Conta
    {
        public override decimal Saca(decimal valor)
        {
            valor = valor + 0.20m;
            //return base.Saca(valor);
            return valor;
        }
    }
}
