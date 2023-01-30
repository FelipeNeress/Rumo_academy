using ApiCartaoPonto.Domain.Exceptions;
using ApiCartaoPonto.Domain.Models;
using ApiCartaoPonto.Repositories.Repositorio;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCartaoPonto.Services
{
    public class CargoService
    {
        private readonly CargoRepositorio _repositorio;
        public CargoService(CargoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public List<Cargo> Listar(string? descricao)
        {
            try
            {
                _repositorio.AbrirConexao();
                return _repositorio.ListarCargo(descricao);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Inserir(Cargo model)
        {
            try
            {
                ValidarModelCargo(model);
                _repositorio.AbrirConexao();
                _repositorio.Inserir(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Deletar(int id)
        {
            try
            {
                _repositorio.AbrirConexao();

                if(!_repositorio.SeExiste(id))
                    throw new ValidacaoException($"Nenhum registro afetado para o identificaro de número {id}");

                _repositorio.Deletar(id);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Atualizar(Cargo model)
        {
            try
            {

                ValidarModelCargo(model);
                _repositorio.AbrirConexao();
                if (!_repositorio.SeExisteAtualizar(model))
                    throw new ValidacaoException($"Nenhum registro afetado para o identificaro de número {model.CargoId}");
                _repositorio.Atualizar(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        private static void ValidarModelCargo(Cargo model)
        {
            if (string.IsNullOrWhiteSpace(model.Descricao))
                throw new ValidacaoException("A descrição do cargo é obrigatório.");

            if (model.Descricao.Trim().Length < 2 || model.Descricao.Trim().Length > 255)
                throw new ValidacaoException("O nome precisa ter entre 2 a 255 caracteres.");

            model.Descricao = model.Descricao.Trim();
        }
    }
}
