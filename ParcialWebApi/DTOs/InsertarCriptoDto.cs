namespace ParcialWebApi.DTOs
{
    public class InsertarCriptoDto
    {
        public string Nombre { get; set; }
        public string Simbolo { get; set; }
        public double ValorActual { get; set; }
        public DateTime UltimaActualizacion { get; set; }
        public int Categoria { get; set; }
        public string Estado { get; set; }
    }
}
