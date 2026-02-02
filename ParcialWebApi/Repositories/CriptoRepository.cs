using Microsoft.EntityFrameworkCore;
using ParcialWebApi.DTOs;
using ParcialWebApi.Models;
using System.Xml;

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

        public async Task<(bool Exito, string Mensaje)> NuevaCripto(InsertarCriptoDto dto)
        {
            var existeNombre = await _context.Criptomonedas
                                        .AnyAsync(a => a.Nombre == dto.Nombre);

            if (existeNombre)
                return (false, $"El nombre '{dto.Nombre}' ya está en uso");
            var existeSimbolo = await _context.Criptomonedas.AnyAsync(a => a.Simbolo == dto.Simbolo);
            if (existeSimbolo) return (false,$"el simbolo {dto.Simbolo} ya se encuentra en uso");


            var entidad = new Criptomoneda
            {
                Nombre = dto.Nombre,
                Simbolo = dto.Simbolo,
                ValorActual = dto.ValorActual,
                UltimaActualizacion = DateTime.UtcNow,
                Categoria = dto.Categoria,
                Estado =dto.Estado// mejor referenciar una categoría existente
            };

            _context.Criptomonedas.Add(entidad);
            await _context.SaveChangesAsync();

            return (true, $"Criptomoneda creada con Id {entidad.Id}");

        }

    }
}
