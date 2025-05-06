using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_noticias.Models
{
    public class Categorias
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string nombre { get; set; }
        [StringLength(500)]
        public string descripcion { get; set; }
        [NotMapped]
        public IFormFile imagenArchivo { get; set; }
        public string? imagen { get; set; }
        [NotMapped]
        public string imagenUrl => $"/uploads/images/{imagen}";
        public ICollection<Noticias>? Noticias { get; set; }
    }
}
