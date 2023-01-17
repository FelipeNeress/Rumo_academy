using PetShop.Modelos;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PetShop.Repositorio
{
    internal class RepositorioPetShop
    {
        private readonly string _caminholistaClientes = $"C:{Path.DirectorySeparatorChar}projetos{Path.DirectorySeparatorChar}PetShop{Path.DirectorySeparatorChar}DatabaseLocal{Path.DirectorySeparatorChar}clientes.csv";
        private List<Pessoa> ListaClientes = new List<Pessoa>();

        public RepositorioPetShop()
        {
            if (!File.Exists(_caminholistaClientes))
            {
                var file = File.Create(_caminholistaClientes);
                file.Close();
            }
        }
        public void Inserir(Pessoa cliente)
        {
            if (!(VerificaExistenciaCliente(cliente.Cpf)))
            {

                File.AppendAllText(_caminholistaClientes, $"{cliente.Nome};{cliente.Cpf};{cliente.Nascimento};{cliente.Observacao}{Environment.NewLine}");
                Console.WriteLine($"{Environment.NewLine}Cliente cadastrado com sucesso!");
            }

            else
                Console.WriteLine($"{Environment.NewLine}Cpf já utilizado, cadastro cancelado.");

            Console.WriteLine("Aperte qualquer botão para continuar...");
            Console.ReadKey();
        }
        public List<Pessoa> Listar()
        {
            CarregarClientesLista();

            return ListaClientes;
        }
        private bool VerificaExistenciaCliente(string cpf)
        {
            CarregarClientesLista();
            Pessoa cliente = ListaClientes.Find(c => c.Cpf == cpf);

            if (cliente == null)
                return false;
            else
                return true;

        }
        private Pessoa LinhaTextoParaCliente(string linha)
        {
            var colunas = linha.Split(';');

            var cliente = new Pessoa();
            cliente.Nome = colunas[0];
            cliente.Cpf = colunas[1];
            cliente.Nascimento = Convert.ToDateTime(colunas[2]);
            cliente.Observacao = colunas[3];

            return cliente;
        }
        private void CarregarClientesLista()
        {
            ListaClientes.Clear();
            var sr = new StreamReader(_caminholistaClientes);
            while (true)
            {
                var linha = sr.ReadLine();

                if (linha == null)
                    break;

                ListaClientes.Add(LinhaTextoParaCliente(linha));
            }

            sr.Close();
        }

        public void Remover(string cpf)
        {
            CarregarClientesLista();

            if (cpf != "Não encontrado")
            {
                var remove = ListaClientes.FindIndex(x => x.Cpf == cpf);
                ListaClientes.RemoveAt(remove);
                RegravarClientes(ListaClientes);
                Console.WriteLine($"{Environment.NewLine}Cliente removido com sucesso !");
                Console.WriteLine("Pressione uma tecla para continuar! ");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Cliente não encontrado");
                Console.WriteLine("Aperte qualque botão para continuar e escolha uma opçao válida");
                Console.ReadKey();
            }
        }
        public void RegravarClientes(List<Pessoa> pessoas) 
        {
            var sw = new StreamWriter(_caminholistaClientes);

            foreach (var pessoa in pessoas)
            {
                sw.WriteLine(GerarLinhaCliente(pessoa.Cpf, pessoa));
            }
            sw.Close();
        }
        private string GerarLinhaCliente(string cpf, Pessoa pessoa)
        {
            return $"{pessoa.Nome};{pessoa.Cpf};{pessoa.Nascimento}; {pessoa.Observacao}";
        }
    }
}

