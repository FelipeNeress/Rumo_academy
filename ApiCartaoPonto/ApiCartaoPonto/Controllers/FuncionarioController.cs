using ApiCartaoPonto.Domain.Exceptions;
using ApiCartaoPonto.Domain.Models;
using ApiCartaoPonto.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using System.Security.Claims;

namespace ApiCartaoPonto.Controllers
{
    [Authorize]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioService _service;
        public FuncionarioController(FuncionarioService service)
        {
            _service = service;
        }

        [Authorize(Roles = "1,2,3")]
        [HttpGet("funcionario")]
        public IActionResult Listar([FromQuery] string? nome)
        {
            return StatusCode(200, _service.Listar(nome));
        }

        [Authorize(Roles = "2")]
        [HttpPost("funcionario")]
        public IActionResult Inserir([FromQuery] Funcionario model) 
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
        [HttpDelete("funcionario")]
        public IActionResult Deletar([FromQuery] string cpf)
        {
            try
            {
                _service.Deletar(cpf);
                return StatusCode(200, "Funcionario apagado com sucesso");
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
        [HttpPut("funcionario")]
        public IActionResult Atualizar([FromQuery] Funcionario model)
        {
            try
            {
                _service.Atualizar(model);
                return StatusCode(201, "Funcionario alterado com sucesso");
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
