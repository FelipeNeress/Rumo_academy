using ApiCartaoPonto.Domain.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCartaoPonto.Repositories.Repositorio
{
    public class LiderancaRepositorio : Contexto
    {
        public LiderancaRepositorio(IConfiguration configuration) : base(configuration)
        {
        }
        public List<Lideranca> ListarLideres(string? descricaoEquipe)
        {
            string comandoSql = @"SELECT 
                                    LiderancaId ,FuncionarioId ,DescricaoEquipe 
                                FROM 
                                    Liderancas";

            if (!string.IsNullOrWhiteSpace(descricaoEquipe))
                comandoSql += " WHERE DescricaoEquipe LIKE @DescricaoEquipe";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                if (!string.IsNullOrWhiteSpace(descricaoEquipe))
                    cmd.Parameters.AddWithValue("@DescricaoEquipe", "%" + descricaoEquipe + "%");

                using (var rdr = cmd.ExecuteReader())
                {
                    var liderancas = new List<Lideranca>();
                    while (rdr.Read())
                    {
                        var lideranca = new Lideranca();
                        lideranca.LiderancaId = Convert.ToInt32(rdr["LiderancaId"]);
                        lideranca.DescricaoEquipe = Convert.ToString(rdr["DescricaoEquipe"]);
                        lideranca.FuncionarioId = Convert.ToInt32(rdr["FuncionarioId"]);

                        liderancas.Add(lideranca);
                    }
                    return liderancas;
                }
            }
        }
        public void Inserir(Lideranca model)
        {
            string comandoSql = @"INSERT INTO Liderancas 
                                    (DescricaoEquipe, FuncionarioId) 
                                        VALUES
                                    (@DescricaoEquipe, @FuncionarioId);"
            ;

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@DescricaoEquipe", model.DescricaoEquipe);
                cmd.Parameters.AddWithValue("@FuncionarioId", model.FuncionarioId);
                cmd.ExecuteNonQuery();
            }
        }
        public void Deletar(int id)
        {
            string comandoSql = @"DELETE FROM Liderancas 
                                WHERE LiderancaId = @LiderancaId";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@LiderancaId", id);

                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para o identificaro de número {id}");
            }
        }
        public bool SeExiste(int id)
        {
            string comandoSql = @"SELECT COUNT(LiderancaId) as Total FROM Liderancas WHERE LiderancaId = @LiderancaId";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@LiderancaId", id);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
        public void Atualizar(Lideranca model)
        {
            string comandoSql = @"UPDATE Liderancas 
                                SET 
                                    DescricaoEquipe = @DescricaoEquipe,
                                    FuncionarioId = @FuncionarioId
                                WHERE 
                                    LiderancaId = @LiderancaId";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@LiderancaId", model.LiderancaId);
                cmd.Parameters.AddWithValue("@DescricaoEquipe", model.DescricaoEquipe);
                cmd.Parameters.AddWithValue("@FuncionarioId", model.FuncionarioId);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para o o registro {model.LiderancaId}");
            }
        }
    }
}
