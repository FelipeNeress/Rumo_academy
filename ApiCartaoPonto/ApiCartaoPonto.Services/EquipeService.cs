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
    public class EquipeService
    {
        private readonly EquipeRepositorio _repositorio;
        public EquipeService(EquipeRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public List<Equipe> Listar(int equipeId)
        {
            try
            {
                _repositorio.AbrirConexao();
                return _repositorio.ListarEquipe(equipeId);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Inserir(Equipe model)
        {
            try
            {
                ValidarModelEquipe(model);
                _repositorio.AbrirConexao();
                if (!_repositorio.SeExisteLider(model))
                    throw new ValidacaoException($"Lider não existe {model.LiderancaId}");
                if (!_repositorio.SeExisteFuncionario(model))
                    throw new ValidacaoException($"Funcionario não existe {model.FuncionarioId}");
                _repositorio.Inserir(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Deletar(int equipeExiste)
        {
            try
            {
                _repositorio.AbrirConexao();

                if (!_repositorio.SeExisteEquipe(equipeExiste))
                    throw new ValidacaoException($"Nenhum registro afetado para o identificaro de número {equipeExiste}");

                _repositorio.Deletar(equipeExiste);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Atualizar(Equipe model)
        {
            try
            {

                ValidarModelEquipe(model);
                _repositorio.AbrirConexao();
                if (!_repositorio.SeExisteLider(model))
                    throw new ValidacaoException($"Lider não existe {model.LiderancaId}");
                if (!_repositorio.SeExisteFuncionario(model))
                    throw new ValidacaoException($"Funcionario não existe {model.FuncionarioId}");
                _repositorio.Atualizar(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        private static void ValidarModelEquipe(Equipe model)
        {
            if (model.LiderancaId <= 0)
                throw new ValidacaoException("Consulte a tabela e veja as equipes e funcionarios disponiveis");

            if(model.FuncionarioId <= 0)
                throw new ValidacaoException("Consulte a tabela e veja as equipes e funcionarios disponiveis");
        }
    }
}
