using ApiCartaoPonto.Domain.Exceptions;
using ApiCartaoPonto.Domain.Models;
using ApiCartaoPonto.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiCartaoPonto.Controllers
{
    [Authorize]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly CargoService _service;
        public CargoController(CargoService service)
        {
            _service = service;
        }

        [Authorize(Roles = "1,2,3")]
        [HttpGet("cargo")]
        public IActionResult Listar([FromQuery] string? descricao)
        {
            return StatusCode(200, _service.Listar(descricao));
        }

        [Authorize(Roles = "2")]
        [HttpPost("cargo")]
        public IActionResult Inserir([FromQuery] Cargo model)
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
        [HttpDelete("cargo")]
        public IActionResult Deletar([FromQuery] int id)
        {
            try
            {
                _service.Deletar(id);
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
        [HttpPut("cargo")]
        public IActionResult Atualizar([FromQuery] Cargo model)
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
