using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Modelos
{
    internal class Pessoa
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime Nascimento { get; set; }
        public string Observacao { get; set; }
        public Pessoa()
        {

        }
        public Pessoa(string nome, string cpf, DateTime dataNascimento, string observacao)
        {
            Nome = nome;
            Cpf = cpf;
            Nascimento = dataNascimento;
            Observacao = observacao;

        }
    }
}
