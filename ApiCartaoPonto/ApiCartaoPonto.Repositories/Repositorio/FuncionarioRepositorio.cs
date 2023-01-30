using ApiCartaoPonto.Domain.Exceptions;
using ApiCartaoPonto.Domain.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCartaoPonto.Repositories.Repositorio
{
    public class FuncionarioRepositorio : Contexto
    {
        public FuncionarioRepositorio(IConfiguration configuration) : base(configuration)
        {
        }
        public List<Funcionario> ListarFuncionario(string? nome)
        {
            string comandoSql = @"SELECT FuncionarioId, NomeDoFuncionario, Cpf, NascimentoFuncionario, DataDeAdmissao, CelularFuncionario, EmailFuncionario, CargoId FROM Funcionarios";

            if (!string.IsNullOrWhiteSpace(nome))
                comandoSql += " WHERE nome LIKE @nome";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                if (!string.IsNullOrWhiteSpace(nome))
                    cmd.Parameters.AddWithValue("@nome", "%" + nome + "%");

                using (var rdr = cmd.ExecuteReader())
                {
                    var funcionarios = new List<Funcionario>();
                    while (rdr.Read())
                    {
                        var funcionario = new Funcionario();
                        funcionario.FuncionarioId = Convert.ToInt32(rdr["FuncionarioId"]);
                        funcionario.NomeDoFuncionario = Convert.ToString(rdr["NomeDoFuncionario"]);
                        funcionario.Cpf = Convert.ToString(rdr["Cpf"]);
                        funcionario.NascimentoFuncionario = Convert.ToDateTime(rdr["NascimentoFuncionario"]);
                        funcionario.DataDeAdmissao = Convert.ToDateTime(rdr["DataDeAdmissao"]);
                        funcionario.CelularFuncionario = Convert.ToString(rdr["CelularFuncionario"]);
                        funcionario.EmailFuncionario = Convert.ToString(rdr["EmailFuncionario"]);
                        funcionario.CargoId = Convert.ToInt32(rdr["CargoId"]);
                        funcionarios.Add(funcionario);
                    }
                    return funcionarios;
                }
            }
        }
        public void Inserir(Funcionario model)
        {
            string comandoSql = @"INSERT INTO Funcionarios 
                                    (NomeDoFuncionario, Cpf, NascimentoFuncionario, DataDeAdmissao, CelularFuncionario, EmailFuncionario, CargoId) 
                                        VALUES
                                    (@NomeDoFuncionario, @Cpf, @NascimentoFuncionario, @DataDeAdmissao, @CelularFuncionario, @EmailFuncionario, @CargoId) ;"
            ;

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                var funcionario = new Funcionario();
                cmd.Parameters.AddWithValue("@NomeDoFuncionario", model.NomeDoFuncionario);
                cmd.Parameters.AddWithValue("@Cpf", model.Cpf);
                cmd.Parameters.AddWithValue("@NascimentoFuncionario", model.NascimentoFuncionario);
                cmd.Parameters.AddWithValue("@DataDeAdmissao", model.DataDeAdmissao);
                cmd.Parameters.AddWithValue("@CelularFuncionario", model.CelularFuncionario);
                cmd.Parameters.AddWithValue("@EmailFuncionario", model.EmailFuncionario);
                cmd.Parameters.AddWithValue("@CargoId", model.CargoId);
                cmd.ExecuteNonQuery();
            }
        }
        public void Deletar(string cpf)
        {
            string comandoSql = @"DELETE FROM Funcionarios 
                                WHERE Cpf = @Cpf";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@Cpf", cpf);

                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para o cpf {cpf}");
            }
        }
        public void Atualizar(Funcionario model)
        {
            string comandoSql = @"UPDATE Funcionarios 
                                SET 
                                    NomeDoFuncionario = @NomeDoFuncionario, Cpf = @Cpf, NascimentoFuncionario = @NascimentoFuncionario, DataDeAdmissao = @DataDeAdmissao,
                                    CelularFuncionario = @CelularFuncionario, EmailFuncionario = @EmailFuncionario, CargoId = @CargoId
                                WHERE FuncionarioId = @FuncionarioId;";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@FuncionarioId", model.FuncionarioId);
                cmd.Parameters.AddWithValue("@NomeDoFuncionario", model.NomeDoFuncionario);
                cmd.Parameters.AddWithValue("@Cpf", model.Cpf);
                cmd.Parameters.AddWithValue("@NascimentoFuncionario", model.NascimentoFuncionario);
                cmd.Parameters.AddWithValue("@DataDeAdmissao", model.DataDeAdmissao);
                cmd.Parameters.AddWithValue("@CelularFuncionario", model.CelularFuncionario);
                cmd.Parameters.AddWithValue("@EmailFuncionario", model.EmailFuncionario);
                cmd.Parameters.AddWithValue("@CargoId", model.CargoId);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para o cpf {model.FuncionarioId}");
            }
        }

        public bool SeExiste(string cpf)
        {
            string comandoSql = @"SELECT COUNT(Cpf) as Total FROM Funcionarios WHERE Cpf = @Cpf";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@Cpf", cpf);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
        public bool SeExisteFuncionario(Funcionario model)
        {
            string comandoSql = @"SELECT COUNT(FuncionarioId) as Total FROM Funcionarios WHERE FuncionarioId = @FuncionarioId";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@FuncionarioId", model.FuncionarioId);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }

        }
        public bool SeExisteCadastra(Funcionario model)
        {
            string comandoSql = @"SELECT COUNT(Cpf) as Total FROM Funcionarios WHERE Cpf = @Cpf";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@Cpf", model.Cpf);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
        public bool ValidarCargo(Funcionario model)
        {
            string comandoSql = @"SELECT COUNT(CargoId) as Total FROM Cargos WHERE CargoId = @CargoId";
            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {

                cmd.Parameters.AddWithValue("@CargoId", model.CargoId);
                return Convert.ToBoolean(cmd.ExecuteScalar());

            }

        }
    }
}
