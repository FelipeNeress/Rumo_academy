using PetShop.Modelos;
using PetShop.Ultilidarios;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PetShop.Servicos
{
    internal class PetShopServico
    {
        private readonly Repositorio.RepositorioPetShop _repositorio;
        public PetShopServico()
        {
            _repositorio = new Repositorio.RepositorioPetShop();
        }
        public void Perguntas()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("|====================================================================================================================|");
                Thread.Sleep(100);
                Console.WriteLine("|                     ##                ##                                                                           |");
                Thread.Sleep(100);
                Console.WriteLine("|  ######    ####     #####    #####    ##       ####    ######     #######  ######  ##       ##   #######    ###### |");
                Thread.Sleep(100);
                Console.WriteLine("|  ##  ##  ##  ##     ##     ##        #####   ##  ##    ##  ##     ##       ##      ##            ##   ##    ##     |");
                Thread.Sleep(100);
                Console.WriteLine("|  ##  ##  ######     ##      #####    ##  ##  ##  ##    ##  ##     #####    #####   ##       ##   ##   ##    ####   |");
                Thread.Sleep(100);
                Console.WriteLine("|  #####   ##         ## ##       ##   ##  ##  ##  ##    #####      ##       ##      ##       ##   #####      ##     |");
                Thread.Sleep(100);
                Console.WriteLine("|  ##       #####      ###   ######   ###  ##   ####     ##         ##       ##      ##       ##   ##         ##     |");
                Thread.Sleep(100);
                Console.WriteLine("| ####                                                  ####        ##       ######  #######  ##   ##         ###### |");
                Thread.Sleep(100);
                Console.WriteLine("|====================================================================================================================|");
                Thread.Sleep(100);
                Console.WriteLine($"{Environment.NewLine}Selecione uma opção");
                Console.WriteLine("1) Consultar clientes cadastrados");
                Console.WriteLine("2) Consultar os aniversariantes do mês");
                Console.WriteLine("3) Buscar Cliente por Cpf");
                Console.WriteLine("4) Cadastrar um novo cliente");
                Console.WriteLine($"5) Remover Cliente{Environment.NewLine}");
                var resposta = Console.ReadLine();
                Console.Clear();

                try
                {
                    switch (resposta)
                    {
                        case "1":
                            ListarClientes();
                            break;

                        case "2":
                            AniversariantesMes();
                            break;

                        case "3":
                            BuscarClienteCPF();
                            break;

                        case "4":
                            CadastraCliente();
                            break;

                        case "5":
                            RemoveCliente();
                            break;

                        default:
                            Console.WriteLine("Aperte qualque botão para continuar e escolha uma opçao válida");
                            Console.ReadKey();
                            break;
                    }
                }
                catch (InvalidOperationException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Pressione uma tecla para continuar! ");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ocorreu um erro fatal no programa, veja a mensagem do erro e contate o ADM: " + ex.Message);
                    Console.WriteLine("Pressione uma tecla para continuar! ");
                    Console.ReadKey();
                }

            }

        }
        private void ListarClientes()
        {
            var clientes = _repositorio.Listar();
            if (clientes.Count == 0)
            {
                Console.WriteLine("Não há clientes cadastrados.");
                return;
            }
            foreach (Pessoa cliente in clientes)
                Console.WriteLine($"Nome: {cliente.Nome}{Environment.NewLine}CPF: {cliente.Cpf}{Environment.NewLine}Data de Nascimento: {cliente.Nascimento.ToString("dd/MM/yyyy")}{Environment.NewLine}Observação: {cliente.Observacao}{Environment.NewLine}");

            Console.WriteLine($"{Environment.NewLine}Aperte qualquer botão para continuar...");
            Console.ReadKey();
        }
        private void AniversariantesMes()
        {
            List<Pessoa> aniversariantesMes = _repositorio.Listar().FindAll(c => c.Nascimento.Month == DateTime.Now.Month);
            if (aniversariantesMes.Count == 0)
                Console.WriteLine("Nenhum cliente faz aniversário este mês!");

            else
                Console.WriteLine("Os cliente que fazem aniversário neste mês: ");
            foreach (Pessoa pessoa in aniversariantesMes)
                Console.WriteLine($"{pessoa.Nome} data: {pessoa.Nascimento:dd/MM}");

            Console.WriteLine($"{Environment.NewLine}Aperte qualquer botão para continuar...");
            Console.ReadKey();
        }
        private void BuscarClienteCPF()
        {
            Console.Write("Digite o CPF do cliente: ");
            string cpf = "";
            while (true)
            {
                cpf = Console.ReadLine();
                cpf = Regex.Replace(cpf, @"(\d{3})(\d{3})(\d{3})(\d{2})", "$1.$2.$3-$4");
                if (Uteis.ValidaCPFBusca(cpf))
                    break;
                else
                    Console.Write($"{Environment.NewLine}Digite um CPF válido! Digite novamente.{Environment.NewLine}CPF: ");
            }

            Pessoa cliente = _repositorio.Listar().Find(c => c.Cpf == cpf);
            if (cliente == null)
                Console.WriteLine("Cliente não encontrado");

            else
                Console.WriteLine($"{Environment.NewLine}Nome: {cliente.Nome}{Environment.NewLine}CPF: {cliente.Cpf}{Environment.NewLine}Data de Nascimento: {cliente.Nascimento}{Environment.NewLine}Observação: {cliente.Observacao}");

            Console.WriteLine($"{Environment.NewLine}Aperte qualquer botão para continuar...");
            Console.ReadKey();
        }
        private void CadastraCliente()
        {
            Console.WriteLine("Leia as instruções com atenção e preencha os dados do novo cliente");
            Console.Write("Informe o primero nome: ");
            string nome = "";
            while (true)
            {
                nome = Console.ReadLine().ToUpper();
                if (Uteis.ValidarNome(nome))
                {
                    Console.WriteLine($"Nome válido");
                    break;
                }
                else
                    Console.Write($"{Environment.NewLine}Nome inválido, tente novamente{Environment.NewLine}Nome: ");
            }

            Console.Write($"{Environment.NewLine}Cpf: ");
            string cpf = "";
            while (true)
            {
                cpf = Console.ReadLine();
                cpf = Regex.Replace(cpf, @"(\d{3})(\d{3})(\d{3})(\d{2})", "$1.$2.$3-$4");
                if (Uteis.ValidaCPF(cpf))
                {
                    Console.WriteLine("CPF válido!");
                    break;
                }
                else
                    Console.Write($"{Environment.NewLine}CPF inválido, digite novamente.{Environment.NewLine}Cpf: ");
            }

            Console.Write($"{Environment.NewLine}Insira a data de nascimente (dia/mês/ano): ");
            string nascimento;
            DateTime dataNascimento;
            while (true)
            {
                nascimento = (Console.ReadLine());
                if (Uteis.ValidaNascimento(nascimento))
                {
                    Console.WriteLine("Data de nascimento válida!");
                    dataNascimento = Convert.ToDateTime(nascimento);
                    break;
                }

            }

            Console.Write($"{Environment.NewLine}Observações sobe o Cliente: ");
            var observacao = Console.ReadLine();

            Pessoa cliente = new Pessoa(nome, cpf, dataNascimento, observacao);
            _repositorio.Inserir(cliente);

        }

        private void RemoveCliente()
        {
            var cpf = PerguntarIdentificador("remover");
            _repositorio.Remover(cpf);
        }
        private string PerguntarIdentificador(string nomeAcao)
        {
            Console.WriteLine($"Digite o cpf do cliente que quer {nomeAcao}?");
            string cpf = Console.ReadLine();
            cpf = Regex.Replace(cpf, @"(\d{3})(\d{3})(\d{3})(\d{2})", "$1.$2.$3-$4");

            Pessoa cliente = _repositorio.Listar().Find(c => c.Cpf == cpf);
            if (cliente == null)
            {
                Console.WriteLine("Cliente não encontrado");
                return "Não Encontrado";
            }
            else
                return cpf;

        }
    }
}
