using System.ComponentModel.DataAnnotations;

namespace api_noticias.DTOs.Categorias
{
    public class CategoriaCreateDTO
    {
        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [StringLength(500)]
        public string descripcion { get; set; }

        public IFormFile? imagenArchivo { get; set; }
    }
}
