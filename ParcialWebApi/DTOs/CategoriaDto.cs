using ParcialWebApi.Models;

namespace ParcialWebApi.DTOs
{
    public partial class CategoriaDto
    {
        public int Id { get; set; }    

        public string Nombre { get; set; }

        public virtual ICollection<CriptoDTO> Criptomoneda { get; set; } = new List<CriptoDTO>();

    }
}
