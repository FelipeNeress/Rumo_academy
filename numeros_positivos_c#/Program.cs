namespace exercis_5_cSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int i = 0, j = 0, contador = 5;
            int[] numero= new int[15];

            Console.WriteLine("Vamos descobri quais são os números positivos, digite 15 números de qualquer valor sendo negativo ou possitvo que no final vamos separar para você\n");
            for (i=0; i<15; i++) 
            {
                Console.WriteLine($"agora digite o {i + 1}º número");
                numero[i]=int.Parse( Console.ReadLine() );
            }

            while(contador > 0)
            {
                Console.Clear() ;
                Console.WriteLine($"Agora vamos fazer a contagem regressiva para expulsar os negativos da festa: {contador}");
                contador--;
                System.Threading.Thread.Sleep(1000);
                Console.Clear() ;
            }

            Console.Write("os números positivos que ficaram são: ");
            for (j=0; j<15; j++)
            {
                if (numero[j] > 0)
                {
                    Console.Write($"{numero[j]}, ");
                }
            }
            Console.WriteLine("boa noite pessoa e até a próxima");
        }
    }
}