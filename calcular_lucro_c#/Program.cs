namespace exercise_7_cSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double precoCompraTotal = 0.0, precoVendaTotal = 0.0, lucroTotal = 0.0;
            double[] precoProduto = new double[50];
            double[] precoVenda = new double[50];
            int i = 0, quantidadeTotal = 0;
            int[] quantidade=new int[50];
            string[] produto = new string[50];

            for (i=0; i<50; i++)
            {
                Console.WriteLine("qual é o produto ? ");
                produto[i] = Console.ReadLine();
                if (produto[i] == "parar")
                {
                    Console.Clear();
                    Console.WriteLine($"o lucro total dessa venda foi R$:{lucroTotal:F2}");
                    return;
                }
                Console.WriteLine("quantidade do produto: ");
                quantidade[i] = int.Parse(Console.ReadLine());

                Console.WriteLine("preço do produto ");
                precoProduto[i] = int.Parse(Console.ReadLine());

                Console.WriteLine("preço que vendeu o produto: ");
                precoVenda[i] = int.Parse(Console.ReadLine());

                quantidadeTotal = quantidade[i];
                precoCompraTotal += precoProduto[i];
                precoVendaTotal += precoVenda[i];
                lucroTotal = (precoVendaTotal * quantidadeTotal) - (precoCompraTotal * quantidadeTotal);
            }
        }
    }
}