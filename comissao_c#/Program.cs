using System;

namespace exercise_3_CSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string nome;
            double salario, vendas, comissao;
            

            Console.WriteLine("Consulta folha de pagamento\nDigite o seu nome !");
            nome= Console.ReadLine();
            Console.WriteLine("qual o seu salario ?");
            salario=float.Parse(Console.ReadLine());
            Console.WriteLine("qual foi o valor total das vendas realizada esse mês ?");
            vendas=float.Parse(Console.ReadLine());
            comissao = (vendas * 0.15 + vendas) - vendas;
            Console.WriteLine($"{nome} seu sálario é de R$ {salario:F2}, sua comissão é de R$ {comissao:F2}.\nE o seu sálario total esse mês é de R$ {salario+comissao:F2}");

        }
    }
}