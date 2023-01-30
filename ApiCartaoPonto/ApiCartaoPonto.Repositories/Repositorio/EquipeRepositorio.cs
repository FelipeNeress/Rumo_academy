using ApiCartaoPonto.Domain.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCartaoPonto.Repositories.Repositorio
{
    public class EquipeRepositorio : Contexto
    {
        public EquipeRepositorio(IConfiguration configuration) : base(configuration)
        {
        }
        public List<Equipe> ListarEquipe(int equipeId)
        {
            string comandoSql = @"SELECT EquipeId, LiderancaId, FuncionarioId FROM Equipes";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {

                using (var rdr = cmd.ExecuteReader())
                {
                    var equipes = new List<Equipe>();
                    while (rdr.Read())
                    {
                        var equipe = new Equipe();
                        equipe.EquipeId = Convert.ToInt32(rdr["EquipeId"]);
                        equipe.LiderancaId = Convert.ToInt32(rdr["LiderancaId"]);
                        equipe.FuncionarioId = Convert.ToInt32(rdr["FuncionarioId"]);
                        equipes.Add(equipe);
                    }
                    return equipes;
                }
            }
        }
        public void Inserir(Equipe model)
        {
            string comandoSql = @"INSERT INTO Equipes 
                                    (LiderancaId, FuncionarioId) 
                                        VALUES
                                    (@LiderancaId, @FuncionarioId);"
            ;

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@LiderancaId", model.LiderancaId);
                cmd.Parameters.AddWithValue("@FuncionarioId", model.FuncionarioId);
                cmd.ExecuteNonQuery();
            }
        }
        public void Deletar(int equipeExiste)
        {
            string comandoSql = @"DELETE FROM Equipes 
                                WHERE EquipeId = @EquipeId";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@EquipeId", equipeExiste);

                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para o identificaro de número {equipeExiste}");
            }
        }
        public void Atualizar(Equipe model)
        {
            string comandoSql = @"UPDATE Equipes 
                                SET 
                                    LiderancaId = @LiderancaId, FuncionarioId = @FuncionarioId
                                WHERE 
                                    EquipeId = @EquipeId;";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@EquipeId", model.EquipeId);
                cmd.Parameters.AddWithValue("@LiderancaId", model.LiderancaId);
                cmd.Parameters.AddWithValue("@FuncionarioId", model.FuncionarioId);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para o o registro {model.EquipeId}");
            }
        }
        public bool SeExisteLider(Equipe model)
        {
            string comandoSql = @"SELECT COUNT(LiderancaId) as Total FROM Liderancas WHERE LiderancaId = @LiderancaId";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@LiderancaId", model.LiderancaId);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
        public bool SeExisteFuncionario(Equipe model)
        {
            string comandoSql = @"SELECT COUNT(FuncionarioId) as Total FROM Funcionarios WHERE FuncionarioId = @FuncionarioId";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@FuncionarioId", model.FuncionarioId);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
        public bool SeExisteEquipe(int equipeExiste)
        {
            string comandoSql = @"SELECT COUNT(EquipeId) as Total FROM Equipes WHERE EquipeId = @EquipeId";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@EquipeId", equipeExiste);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
    }
}
