using ApiCartaoPonto.Domain.Exceptions;
using ApiCartaoPonto.Domain.Models;
using ApiCartaoPonto.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ApiCartaoPonto.Controllers
{
    [Authorize]
    [ApiController]
    public class LiderancaController : ControllerBase
    {
        private readonly LiderancaService _service;
        public LiderancaController(LiderancaService service)
        {
            _service = service;
        }

        [Authorize(Roles = "1,2,3")]
        [HttpGet("lideranca")]
        public IActionResult Listar([FromQuery] string? descricaoEquipe)
        {
            return StatusCode(200, _service.Listar(descricaoEquipe));
        }

        [Authorize(Roles = "2")]
        [HttpPost("lideranca")]
        public IActionResult Inserir([FromQuery] Lideranca model)
        {
            try
            {
                _service.Inserir(model);
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
        [HttpDelete("lideranca")]
        public IActionResult Deletar([FromQuery] int id)
        {
            try
            {
                _service.Deletar(id);
                return StatusCode(200, "Equipe apagado com sucesso");
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
        [HttpPut("lideranca")]
        public IActionResult Atualizar([FromQuery] Lideranca model)
        {
            try
            {
                _service.Atualizar(model);
                return StatusCode(201, "Atualizado com sucesso");
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

