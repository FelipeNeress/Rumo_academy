namespace exercise_6_cSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] nome = new string[10];
            string nomeMaisVelho = "";
            int[] idade= new int[10];
            int i = 0, maiorIdade = 0;

            Console.WriteLine("-------------------vamos descobrir quem é o mais velho---------------------------");
            for(i = 0; i < 10; i++)
            {
                Console.WriteLine($"qual é o seu nome da {i + 1}ª pessoa?");
                nome[i] = Console.ReadLine();
                Console.WriteLine($"qual é a sua idade da {i + 1}ª pessoa?");
                idade[i] = int.Parse(Console.ReadLine());
                Console.Clear();
                if (maiorIdade < idade[i])
                {
                    nomeMaisVelho = nome[i];
                    maiorIdade = idade[i];
                }
            }

            Console.WriteLine($"A pessoa com a maior idade é {nomeMaisVelho} com a idade de {maiorIdade} anos");

        }
    }
}