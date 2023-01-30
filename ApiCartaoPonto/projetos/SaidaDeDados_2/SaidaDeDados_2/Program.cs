using System;
using System.Globalization;

namespace SaidaDeDados_2
{
    class Program
    {
        static void Main(string[] args)
        {

            int idade = 32;
            double saldo = 10.35784;
            string nome = "Maria";

            Console.WriteLine("{0} tem {1} anos e tem saldo igual a {2:F2} reais", nome, idade, saldo); //tecnica do Placeholders e "{2:F2}" usado para limitar as casas decimais 
            Console.WriteLine($"{nome} tem {idade} anos e tem saldo igual a {saldo:F2} reais"); //concatenação oque eu mais gostei
            Console.WriteLine(nome + " tem " + idade + "anos e tem saldo igual a " + saldo.ToString("F2", CultureInfo.InvariantCulture) + " reais"); //interpolação o mais antigo, mas da para trocar a "," pelo "."    
        }
    }
}