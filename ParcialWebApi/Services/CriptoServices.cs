using ParcialWebApi.DTOs;
using ParcialWebApi.Models;
using ParcialWebApi.Repositories;

namespace ParcialWebApi.Services
{
    public class CriptoServices : ICriptoServices
    {
        private readonly ICriptoRepository _repository;
        public CriptoServices(ICriptoRepository repository)
        {
            _repository = repository;
        }

      

        public async Task<List<Criptomoneda?>> ConsultarCategoria(int categoria)
        {
           var resultado =await _repository.ConsultarCategoria(categoria);
            return resultado;
        }

        public async Task<string> ActualizarValor(string simbolo, double valorActual)
        {
           var ok = await _repository.ActualizarValor(simbolo, valorActual);
           
           return ok ? $"Valor de la cripto: {simbolo} actualizado a : {valorActual}." : $"No se pudo actualizar el valor de la cripto {simbolo}."; ;
        }

        public async Task<string> ActualizarEstado(int id, string estado)
        {
            var ok = await _repository.ActualizarEstado(id, estado);
            return ok ? $"Estado de la cripto con id: {id} actualizado a : {estado}." : $"No se pudo actualizar el estado de la cripto con id: {id}."; ;
        }

        public Task<List<Criptomoneda?>> ListarCriptomonedas()
        {
            var resultado = _repository.ListarCriptomonedas();
            return resultado ;
        }
    }
}
