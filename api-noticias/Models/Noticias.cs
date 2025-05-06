using System.ComponentModel.DataAnnotations;

namespace api_noticias.Models
{
    public class Noticias
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string titulo { get; set; }
        [StringLength(500)]
        public string descripcion { get; set; }
        public string? imagen { get; set; }

        // llaves foraneas
        public int idCategoria { get; set; }
        public int idClasificacion { get; set; }

        // relaciones
        public Categorias Categoria { get; set; }
        public Clasificaciones Clasificacion { get; set; }
    }
}
