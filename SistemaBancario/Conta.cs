using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBancario
{
    public class Conta
    {
        public int IdConta { get; set; }
        public int IdCliente { get; set; }
        public string Agencia { get; set; }
        public string Numero { get; set; }
        
        public decimal Saldo { get; set; }
        public string Tipo { get; set; }

        public virtual decimal Saca(decimal valor)
        {
            return this.Saldo -= valor;
        }
    }
}
