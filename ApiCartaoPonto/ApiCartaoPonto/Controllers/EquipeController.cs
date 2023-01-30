using ApiCartaoPonto.Domain.Exceptions;
using ApiCartaoPonto.Domain.Models;
using ApiCartaoPonto.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiCartaoPonto.Controllers
{
    [Authorize]
    [ApiController]
    public class EquipeController : ControllerBase
    {
        private readonly EquipeService _service;
        public EquipeController(EquipeService service)
        {
            _service = service;
        }

        [Authorize(Roles = "1,2,3")]
        [HttpGet("equipe")]
        public IActionResult Listar([FromQuery] int equipId)
        {
            return StatusCode(200, _service.Listar(equipId));
        }

        [Authorize(Roles = "2")]
        [HttpPost("equipe")]
        public IActionResult Inserir([FromQuery] Equipe model)
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
        [HttpDelete("equipe")]
        public IActionResult Deletar([FromQuery] int equipeExiste)
        {
            try
            {
                _service.Deletar(equipeExiste);
                return StatusCode(200, "cargo apagado com sucesso");
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
        [HttpPut("equipe")]
        public IActionResult Atualizar([FromQuery] Equipe model)
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
