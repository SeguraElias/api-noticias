namespace api_noticias.DTOs.Categorias
{
    public class CategoriaResponseDTO
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string imagenUrl { get; set; }
    }
}
