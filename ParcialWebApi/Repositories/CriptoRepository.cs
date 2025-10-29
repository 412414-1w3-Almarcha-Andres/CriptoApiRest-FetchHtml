using Microsoft.EntityFrameworkCore;
using ParcialWebApi.DTOs;
using ParcialWebApi.Models;

namespace ParcialWebApi.Repositories
{
    public class CriptoRepository : ICriptoRepository
    {
        private CriptoContext _context;
        public CriptoRepository(CriptoContext context)
        {
            _context = context;
        }

        

        public async Task<List<Criptomoneda?>> ConsultarCategoria(int categoria)
        {
            var fechaLimite = DateTime.Now.AddDays(-1);

            // Intentamos obtener criptomonedas actualizadas recientemente
            var recientes = await _context.Criptomonedas
                .Where(c => c.Categoria == categoria && c.UltimaActualizacion >= fechaLimite)
                .ToListAsync();

            // Si hay resultados que sea de ayer, los devolvemos en reciente
            if (recientes.Any())
            {
                return recientes;
            }

            // Si no hay recientes, devolvemos todas las criptos de la categoría sin filtrar por fecha
            return await _context.Criptomonedas
                .Where(c => c.Categoria == categoria)
                .ToListAsync();
        }
        public async Task<bool> ActualizarValor(string simbolo, double valorActual)
        {
            var criptomoneda = await _context.Criptomonedas.FirstOrDefaultAsync(c => c.Simbolo == simbolo);
            if (criptomoneda is null)
                            return false;
           var fechaLimite = DateTime.Now.AddDays(-1);
            if (criptomoneda.UltimaActualizacion < fechaLimite)
            {
                throw new Exception("Solo se permiten actualizar valores con fecha de ultima actializacion de hace 1 dia como maximo");
            }
            criptomoneda.ValorActual = valorActual;
            criptomoneda.UltimaActualizacion = DateTime.Now;
           await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> ActualizarEstado(int id, string estado)
        {
           var criptomoneda = await _context.Criptomonedas.FindAsync(id);
            if (criptomoneda is null)
                return false;
            if (criptomoneda.Estado != "H")
            {
                throw new Exception("Solo se pueden inhabilitar criptomonedas que esten habilitadas (estado 'H').");
            }
            criptomoneda.Estado = estado;
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<List<Criptomoneda?>> ListarCriptomonedas()
        {
           var listado = _context.Criptomonedas.ToListAsync();
            return listado;
        }
    }
}
