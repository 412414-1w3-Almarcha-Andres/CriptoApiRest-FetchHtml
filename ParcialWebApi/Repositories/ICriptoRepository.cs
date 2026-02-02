using ParcialWebApi.DTOs;
using ParcialWebApi.Models;

namespace ParcialWebApi.Repositories
{
    public interface ICriptoRepository
    {
      
        Task<List<Criptomoneda?>> ConsultarCategoria(int categoria);
        Task<bool> ActualizarValor(string simbolo, double valorActual);
        Task<bool> ActualizarEstado(int id, string estado);
        Task<List<Criptomoneda?>> ListarCriptomonedas();
        Task<(bool Exito, string Mensaje)> NuevaCripto(InsertarCriptoDto dto);
    }
}
