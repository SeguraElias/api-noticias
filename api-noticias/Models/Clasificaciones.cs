using System.ComponentModel.DataAnnotations;

namespace api_noticias.Models
{
    public class Clasificaciones
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string nombre { get; set; }
        [StringLength(500)]
        public string descripcion { get; set; }

        public ICollection<Noticias> Noticias { get; set; }
    }
}
