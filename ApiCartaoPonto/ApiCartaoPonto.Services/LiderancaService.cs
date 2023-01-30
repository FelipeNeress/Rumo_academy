using ApiCartaoPonto.Domain.Exceptions;
using ApiCartaoPonto.Domain.Models;
using ApiCartaoPonto.Repositories.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCartaoPonto.Services
{
    public class LiderancaService
    {
        private readonly LiderancaRepositorio _repositorio;
        public LiderancaService(LiderancaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public List<Lideranca> Listar(string? descricaoEquipe)
        {
            try
            {
                _repositorio.AbrirConexao();
                return _repositorio.ListarLideres(descricaoEquipe);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Inserir(Lideranca model)
        {
            try
            {
                ValidarModelLideranca(model);
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

                if (!_repositorio.SeExiste(id))
                    throw new ValidacaoException($"Nenhum registro afetado para o identificaro de número {id}");

                _repositorio.Deletar(id);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Atualizar(Lideranca model)
        {
            try
            {

                ValidarModelLideranca(model);
                _repositorio.AbrirConexao();
                _repositorio.Atualizar(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        private static void ValidarModelLideranca(Lideranca model)
        {
            if (string.IsNullOrWhiteSpace(model.DescricaoEquipe))
                throw new ValidacaoException("A descrição da equipe é obrigatório.");

            if (model.DescricaoEquipe.Trim().Length < 5 || model.DescricaoEquipe.Trim().Length > 255)
                throw new ValidacaoException("A descriçao precisa ter entre 5 a 255 caracteres.");

            model.DescricaoEquipe = model.DescricaoEquipe.Trim();
        }
    }
}
