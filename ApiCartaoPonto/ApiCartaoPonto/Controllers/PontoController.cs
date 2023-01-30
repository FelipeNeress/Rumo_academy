using ApiCartaoPonto.Domain.Exceptions;
using ApiCartaoPonto.Domain.Models.MarcacaoDePonto.Models.Models;
using ApiCartaoPonto.Services;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiCartaoPonto.Controllers
{
    [Authorize]
    [ApiController]
    public class PontoController : ControllerBase
    {
        private readonly PontoServices _service;

        public PontoController(PontoServices service)
        {
            _service = service;
        }

        [Authorize(Roles = "2")]
        [HttpGet("ponto")]
        public IActionResult Listar([FromQuery] string? id)
        {
            return StatusCode(200, _service.Listar(id));
        }

        [Authorize(Roles = "1,2,3")]
        [HttpPost("ponto")]
        public IActionResult Inserir([FromQuery] Ponto ponto)
        {
            try
            {
                _service.Inserir(ponto);
                return StatusCode(201);
            }
            catch (ValidacaoException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [Authorize(Roles = "2")]
        [HttpDelete("ponto")]
        public IActionResult Apagar([FromQuery] double id)
        {
            try
            {
                _service.Apagar(id);
                return StatusCode(200, "Ponto apagado do banco de dados");
            }
            catch (ValidacaoException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }


        }

        [Authorize(Roles = "2")]
        [HttpPut("ponto")]
        public IActionResult Atualizar([FromQuery] Ponto ponto)
        {
            try
            {
                _service.Atualizar(ponto);
                return StatusCode(201);
            }
            catch (ValidacaoException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
