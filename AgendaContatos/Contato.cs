using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContatos
{
    public class Contato
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string NumeroTelefone { get; set; }

        public Contato(string nome = "", string email = "", string numero = "")
        {
            this.Nome = nome;
            this.Email = email;
            this.NumeroTelefone = numero;
        }



        //Sobrescrita do método para formatar corretamente a string do Contato
        public override string ToString()
        {
            return string.Format("{0} - {1}, {2}", this.Nome, this.Email, this.NumeroTelefone);
        }
    }
}
