using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PetShop.Ultilidarios
{
    public static class Uteis
    {
        public static bool ValidarNome(string nome)
        {
          
            if (nome is null or "")
                return false;
            
         
            if (!(nome.Length >= 3 && nome.Length <= 80))      
                return false;
           
            string pattern = @"^[A-Z]*$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(nome);
        }
        public static bool ValidaCPF(string cpf)
        {
            cpf = Regex.Replace(cpf, "[^0-9]", "");

            if (cpf.Length != 11)
                return false;

            for (int i = 0; i < cpf.Length; i++)
            {
                if (cpf[i] != cpf[i + 1])
                    break;  
                else if (i == cpf.Length - 2)
                    return false;
            }

            int[] digito1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum1 = 0;

            for (int i = 0; i < 9; i++)
                sum1 += int.Parse(cpf[i].ToString()) * digito1[i];
      
            int result1 = sum1 % 11;

            if (result1 < 2)
                result1 = 0;

            else
                result1 = 11 - result1;
          
            if (result1 != int.Parse(cpf[9].ToString()))
                return false;

            int[] digito2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum2 = 0;

            for (int i = 0; i < 10; i++)
                sum2 += int.Parse(cpf[i].ToString()) * digito2[i];

            int result2 = sum2 % 11;

            if (result2 < 2)
                result2 = 0;
           
            else
                result2 = 11 - result2;

            if (result2 != int.Parse(cpf[10].ToString()))
                return false;

            return true;
        }
        public static bool ValidaNascimento(string nascimento)
        {
            DateTime dataNascimento;
            if (!DateTime.TryParseExact(nascimento, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataNascimento))
            {
                Console.Write($"{Environment.NewLine}Data fora do padrão requisitado.{Environment.NewLine}Por favor, insira novamente: ");
                return false;
            }

            int idade = Convert.ToInt32(DateTime.Now.Year - dataNascimento.Year);
            if (!(idade >= 16 && idade <= 120))
            {
                Console.Write($"{Environment.NewLine}A idade mínima é 16 anos e a máxima é de 120!{Environment.NewLine}Digite novamente a data: ");
                return false;
            }

            return true;
        }
        public static bool ValidaCPFBusca(string cpf)
        {
            
            cpf = Regex.Replace(cpf, @"[^0-9]", "");
            
            if (cpf.Length != 11)
                return false;
            
            return true;
        }
    }
}

