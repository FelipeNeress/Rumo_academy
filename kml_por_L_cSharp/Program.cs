    using System;

namespace kml_por_L_cSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int resposta;

            Console.WriteLine("Bem vindo, esse programa te ajudara a saber o consumo de KM/L e a distancia máxima que o seu véiculo pode fazer.\nPara saber a distancia máxima digite 1 e para saber a média digite 2\n");
            resposta=int.Parse(Console.ReadLine());
            if(resposta == 1)
            {
                float kml, tanque, total;
                Console.WriteLine("porfavor informe os km/l ?");
                kml= float.Parse(Console.ReadLine());
                Console.WriteLine("agora informe, quantos litros o tanque do seu véiculo tem ?");
                tanque=float.Parse(Console.ReadLine());
                total = kml*tanque;
                Console.WriteLine($"com o tanque de combustível cheio seu véiculo percorre a distancia máxima de {total:F2} KM");
            }
            if(resposta == 2)
            {
                float bombalitro, hodometro, kml, tanque, kmt;
                Console.WriteLine("então a partir de agora vamos fazer os sequintes passo\n1º zere o hodômetro parcial do painel\n2º no próximo abastecimento, confira a quantidade de combustível que abasteceu no tanque, através da bomba do posto.");
                Console.WriteLine("\ncom essas informações vamos primeiro calcular a média de KM/L seu véiculo\ninforme, qual a quantidade de litro que você abasteceu");
                bombalitro=float.Parse(Console.ReadLine());
                Console.WriteLine("agora informe, qual foi a quantidade de km rodados ?");
                hodometro=float.Parse(Console.ReadLine());
                kml = hodometro / bombalitro;
                Console.WriteLine($"o seu véiculo faz {kml:F2} KM/L");
                Console.WriteLine("agora vamos saber a distancia máxima que o seu véiculo pode percorrer, quantos litros o tanque do seu véiculo tem ? ");
                tanque=float.Parse(Console.ReadLine());
                kmt = kml * tanque;
                Console.WriteLine($"com o tanque de combustível cheio seu véiculo percorre a distancia máxima de {kmt:F2} KM");

            }
            else
            {
                Console.WriteLine("opção inválida, reinicie o programa e digite uma opção válida");
            }

            
        }
    }
}