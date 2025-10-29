using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using ParcialWebApi.DTOs;
using ParcialWebApi.Models;
using ParcialWebApi.Repositories;
using ParcialWebApi.Services;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnviosWebAppi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriptoController : ControllerBase
    {
        private ICriptoServices _criptoServices;

        public CriptoController(ICriptoServices criptoServices)
        {
            _criptoServices = criptoServices;
        }
        //* consultar criptomonedas de una categoría determinada("Plataforma", "Moneda", "Token"),
        //*Solo es posible consultar monedas cuya última actualización no supere un día a la fecha.(hacerlo sin limite de fecha para probar traer por categoria)
        //*GET /cripto | Recupera todas las criptomonedas según las consideraciones indicadas
        [HttpGet("CriptoPorCategoria")]
        public async Task<IActionResult> ConsultarCategoria([FromQuery] int categoria)
        {
            if (categoria == null)
            {
                return Content("Categoría inválida. Debe ser 1 (Plataforma), 2 (Moneda) o 3 (Token).");//para capturar un error en una ventana de navegador usamos return Content 
            }
            if (categoria < 1 || categoria > 3)
            {
                return BadRequest("Categoría inválida. Debe ser 1 (Plataforma), 2 (Moneda) o 3 (Token).");
            }
            var resultado = await _criptoServices.ConsultarCategoria(categoria);
            if (resultado == null || resultado.Count == 0)
            {
                return NotFound("No se encontraron criptomonedas para la categoría especificada.");
            }
            //if (resultado.Any(c => c != null && c.UltimaActualizacion < DateTime.Now.AddDays(-1)))
            //{
            //    return Ok("No hay criptomonedas actualizadas en el último día para la categoría especificada.");
            //}
            return Ok(resultado);
        }
        //* actualizar el valor actual (junto con la fecha/hora de la última cotización) de una criptomoneda identificada por símbolo(por ejemplo: BTC para Bitcoins)
        //*Al momento de actualizar la cotización de una moneda la fecha/hora de la última cotización no puede ser
        //superior a un día.Por ejemplo, es posible indicar que el valor de ayer fue de x dólares,
        // pero no el de antes de ayer.
        //*PUT /cripto? simbolo = ETC; valorActual=20 |Permite actualizar el valor de la moneda a partir del símbolo.
        [HttpPut("ActualizarValor")]
        public async Task<IActionResult> ActualizarValor([FromQuery] string simbolo, [FromQuery] double valorActual)
        {

            try
            {
                var msg = await _criptoServices.ActualizarValor(simbolo, valorActual);
                return Ok(msg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //* registrar la inhabilitación de una moneda.
        //*Solo es posible registrar la baja (inhabilitación) de una moneda si su estado es “H” (Habilitada). 
        //Los estados posibles son: “H”-Habilitada | “NH” – No Habilitada.
        //* DELETE /cripto/1: Permite actualizar el estado de la criptomoneda identificada por id.
        [HttpPut("ActualizarEstado")]
        public async Task<IActionResult> ActualizarEstado([FromQuery] int id, [FromQuery] string estado)
        {
            try
            {
                var msg = await _criptoServices.ActualizarEstado(id, estado);
                return Ok(msg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //**//ejer aparte , listar todas las criptomonedas
        [HttpGet("ListarCriptomonedas")]
        public async Task<IActionResult> ListarCriptomonedas()
        {
            var resultado = await _criptoServices.ListarCriptomonedas();
            return Ok(resultado);
        }
    }
}
