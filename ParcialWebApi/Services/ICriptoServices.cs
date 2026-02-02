
using ParcialWebApi.DTOs;
using ParcialWebApi.Models;

namespace ParcialWebApi.Services
{
    public interface ICriptoServices
    {
      
        Task <List<Criptomoneda?>>ConsultarCategoria(int categoria);
        Task<string> ActualizarValor(string simbolo, double valorActual);
        Task<string> ActualizarEstado(int id, string estado);
        Task<List<Criptomoneda?>> ListarCriptomonedas();
        Task<(bool Exito, string Mensaje)> NuevaCripto(InsertarCriptoDto dto);
    }
}
