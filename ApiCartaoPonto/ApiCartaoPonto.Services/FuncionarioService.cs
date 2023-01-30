using ApiCartaoPonto.Domain.Exceptions;
using ApiCartaoPonto.Domain.Models;
using ApiCartaoPonto.Repositories.Repositorio;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Mail;

namespace ApiCartaoPonto.Services
{
    public class FuncionarioService
    {
        private readonly FuncionarioRepositorio _repositorio;
        public FuncionarioService(FuncionarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public List<Funcionario> Listar(string? nome)
        {
            try
            {
                _repositorio.AbrirConexao();
                return _repositorio.ListarFuncionario(nome);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Inserir(Funcionario model)
        {
            try
            {
                ValidarModelCliente(model);

                _repositorio.AbrirConexao();
                if (!_repositorio.ValidarCargo(model))
                    throw new ValidacaoException($"Nenhum cargo encontrado para o CargoId {model.CargoId}");

                if (_repositorio.SeExisteCadastra(model))
                    throw new ValidacaoException($"Essa pessoa já é cadastrada");


                _repositorio.Inserir(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Deletar(string cpf)
        {
            try
            {
                _repositorio.AbrirConexao();
                if (!_repositorio.SeExiste(cpf))
                    throw new ValidacaoException($"Nenhum registro afetado para o identificaro de número {cpf}");

                _repositorio.Deletar(cpf);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Atualizar(Funcionario model)
        {
            try
            {
                
                ValidarModelCliente(model, true);
                _repositorio.AbrirConexao();

                if (!_repositorio.SeExisteFuncionario(model))
                    throw new ValidacaoException($"Nenhum registro afetado para o identificaro de número {model.FuncionarioId}");
                _repositorio.Atualizar(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        #region Validações
        private static void ValidarModelCliente(Funcionario model, bool isUpdate = false, bool isMail = false)
        {
            if (model is null)
                throw new ValidacaoException("O json está mal formatado, ou foi enviado vazio.");

            //Validação de nome
            if (string.IsNullOrWhiteSpace(model.NomeDoFuncionario))
                throw new ValidacaoException("O nome é obrigatório.");

            if (model.NomeDoFuncionario.Trim().Length < 3 || model.NomeDoFuncionario.Trim().Length > 255)
                throw new ValidacaoException("O nome precisa ter entre 3 a 255 caracteres.");

            //Validação de cpf
            if (!isUpdate)
            {
                if (string.IsNullOrWhiteSpace(model.Cpf))
                    throw new ValidacaoException("O cpf e obrigatório.");

                if (!ValidarCpf(model.Cpf))
                    throw new ValidacaoException("O cpf e inválido.");
            }

            //Validação de idade
            if (ObterIdade(model.NascimentoFuncionario) < 16)
                throw new ValidacaoException("Somente maiores de 16 anos podem ser cadastrados como Funcionarios.");

            //Validação de Admissão

            //Validação de Telefone
            if (model.CelularFuncionario is not null
                &&
                (model.CelularFuncionario.Trim().Length < 11
                || model.CelularFuncionario.Trim().Length > 15
                || model.CelularFuncionario.Trim().Length != RemoverMascaraTelefone(model.CelularFuncionario).Length)
                )
                throw new ValidacaoException("O mínimo que o telefone pode ter são 11 a 15 digitos, e não pode conter mascaras.");

            //validaçao de email

            if (!isMail)
            {
                if (string.IsNullOrWhiteSpace(model.EmailFuncionario))
                    throw new ValidacaoException("O email é obrigatório.");

                if (model.EmailFuncionario.Trim().Length < 10 || model.EmailFuncionario.Trim().Length > 255)
                    throw new ValidacaoException("O email precisa ter entre 10 a 255 caracteres.");

                if (!VerificarEmail(model.EmailFuncionario))
                    throw new ValidacaoException("O email não é válido.");


            }

            //tira os espaços em branco das extremidades
            model.NomeDoFuncionario = model.NomeDoFuncionario.Trim();
            model.CelularFuncionario = model.CelularFuncionario.Trim();
            model.EmailFuncionario = model.EmailFuncionario.Trim();
        }
        private static bool ValidarCpf(string? cpf)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cpf))
                    return false;

                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string tempCpf;
                string digito;
                int soma;
                int resto;
                cpf = cpf.Trim();
                cpf = cpf.Replace(".", "").Replace("-", "");
                if (cpf.Length != 11)
                    return false;
                tempCpf = cpf.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                return cpf.EndsWith(digito);
            }
            catch (Exception)
            {
                return false;
            }
        }
        private static int ObterIdade(DateTime birthdate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthdate.Year;
            if (birthdate > today.AddYears(-age)) age--;
            return age;
        }
        private static string RemoverMascaraTelefone(string phoneNumber)
        {
            return Regex.Replace(phoneNumber, @"[^\d]", "");
        }
        private static bool VerificarEmail(string email)
        {
            // Expressão regular para verificar se o formato do email é válido
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            Match match = Regex.Match(email, pattern);

            if (match.Success)
            {
                // Verifica se o endereço de email é realmente válido
                try
                {
                    MailAddress mailAddress = new MailAddress(email);
                    return true;
                }
                catch (FormatException)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        
        #endregion
    }
}
