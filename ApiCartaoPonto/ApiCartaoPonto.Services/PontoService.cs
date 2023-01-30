using ApiCartaoPonto.Domain.Exceptions;
using ApiCartaoPonto.Domain.Models.MarcacaoDePonto.Models.Models;
using ApiCartaoPonto.Repositories.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCartaoPonto.Services
{
    public class PontoServices
    {
        private readonly PontoRepositorio _repositorio;
        public PontoServices(PontoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public List<Ponto> Listar(string? id)
        {
            try
            {
                _repositorio.AbrirConexao();
                return _repositorio.ListarPontos(id);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        public void Inserir(Ponto ponto)
        {
            try
            {
                ValidarPonto(ponto);
                _repositorio.AbrirConexao();
                if (!_repositorio.SeExisteIdFuncionario(ponto.FuncionarioId))
                    throw new ValidacaoException($"Nenhum registro encontrado para o Identificador do funcionario {ponto.FuncionarioId}");
                _repositorio.Inserir(ponto);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Apagar(double id)
        {
            try
            {
                _repositorio.AbrirConexao();

                if (!_repositorio.SeExiste(id))
                    throw new ValidacaoException($"Nenhum registro encontrado para o Identificador {id}");

                _repositorio.Apagar(id);
            }
            finally
            {
                {
                    _repositorio.FecharConexao();
                }
            }
        }
        public void Atualizar(Ponto ponto)
        {
            try
            {

                ValidarPonto(ponto);
                _repositorio.AbrirConexao();


                if (!_repositorio.SeExiste(ponto.PontoId))
                    throw new ValidacaoException($"Nenhum reistro encontrado par o Identificador {ponto.PontoId}");

                _repositorio.Atualizar(ponto);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        public static void ValidarPonto(Ponto ponto)
        {


            if (string.IsNullOrWhiteSpace(ponto.DataHorarioPonto.ToString()))
                throw new ValidacaoException("A data e horario são obrigatorios.");

            if (ponto.Justificativa is not null)
            {
                if (ponto.Justificativa.Length > 255)
                    throw new ValidacaoException("A descrição do cargo precisa ter entre 2 e 255 caracteres");
            }


            if (ponto.FuncionarioId == 0)
                throw new ValidacaoException("O identificador de funcionario é obrigatorio.");
        }
    }
}
