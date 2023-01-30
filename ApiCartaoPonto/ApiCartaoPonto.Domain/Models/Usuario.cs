using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCartaoPonto.Domain.Models
{
    public enum EnumPermissaoUsuario
    {
        Funcionario = 1,
        Rh = 2,       
        Gerente = 3
    }
    public class Usuario
    {
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
