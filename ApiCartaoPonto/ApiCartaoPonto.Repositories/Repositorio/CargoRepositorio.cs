using ApiCartaoPonto.Domain.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCartaoPonto.Repositories.Repositorio
{
    public class CargoRepositorio : Contexto
    {
        public CargoRepositorio(IConfiguration configuration) : base(configuration)
        {
        }
        public List<Cargo> ListarCargo(string? descricao)
        {
            string comandoSql = @"SELECT CargoId, Descricao FROM Cargos";

            if (!string.IsNullOrWhiteSpace(descricao))
                comandoSql += " WHERE Descricao LIKE @Descricao";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                if (!string.IsNullOrWhiteSpace(descricao))
                    cmd.Parameters.AddWithValue("@Descricao", "%" + descricao + "%");

                using (var rdr = cmd.ExecuteReader())
                {
                    var cargos = new List<Cargo>();
                    while (rdr.Read())
                    {
                        var cargo = new Cargo();
                        cargo.CargoId = Convert.ToInt32(rdr["CargoId"]);
                        cargo.Descricao = Convert.ToString(rdr["Descricao"]);
                        cargos.Add(cargo);
                    }
                    return cargos;
                }
            }
        }
        public void Inserir(Cargo model)
        {
            string comandoSql = @"INSERT INTO Cargos 
                                    (Descricao) 
                                        VALUES
                                    (@Descricao);"
            ;

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@Descricao", model.Descricao);
                cmd.ExecuteNonQuery();
            }
        }
        public void Deletar(int id)
        {
            string comandoSql = @"DELETE FROM Cargos 
                                WHERE CargoId = @CargoId";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@CargoId", id);

                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para o identificaro de número {id}");
            }
        }
        public void Atualizar(Cargo model)
        {
            string comandoSql = @"UPDATE Cargos 
                                SET 
                                    Descricao = @Descricao
                                WHERE 
                                    CargoId = @CargoId;";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@CargoId", model.CargoId);
                cmd.Parameters.AddWithValue("@Descricao", model.Descricao);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para o o registro {model.CargoId}");
            }
        }
        public bool SeExiste(int id)
        {
            string comandoSql = @"SELECT COUNT(CargoId) as Total FROM Cargos WHERE CargoId = @CargoId";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@CargoId", id);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
        public bool SeExisteAtualizar(Cargo model)
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