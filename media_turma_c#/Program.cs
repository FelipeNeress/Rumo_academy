namespace exercise_4_cSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int j, i = 0;
            double media, nota, soma = 0;
            double[] vetor = new double[50];



            Console.WriteLine("calculando a nota média da turma\nlembre-se para parar apenas digite um número negativo Ex: -1, -2...\n");
            do
            {
                Console.WriteLine($"Informe o {i + 1}º valor: ");
                nota=float.Parse( Console.ReadLine() );
                if (nota>=0)
                {
                    vetor[i]=nota;
                    i++;
                }
            }
            while (nota >= 0);

            for(j=0; j<50; j++)
            {
                soma += vetor[j];
            }

            media = soma / i;
            Console.WriteLine($"a média da turma é de {media:F2} pontos.");
        }
    }
}