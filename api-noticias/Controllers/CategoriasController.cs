using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_noticias.Data;
using api_noticias.Models;
using api_noticias.Services;
using api_noticias.DTOs.Categorias;

namespace api_noticias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ImageService _imageService;

        public CategoriasController(AppDbContext context, ImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        // GET: api/categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaResponseDTO>>> getCategorias()
        {
           return await _context.Categorias
                .Select(c => new CategoriaResponseDTO
                {
                    Id = c.Id,
                    nombre = c.nombre,
                    descripcion = c.descripcion,
                    imagenUrl = !string.IsNullOrEmpty(c.imagen)
                                ? c.imagenUrl
                                : null
                })
                .ToListAsync();
        }

        // GET: api/categorias/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaResponseDTO>> getCategoriaById(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(new CategoriaResponseDTO
            {
                Id = categoria.Id,
                nombre = categoria.nombre,
                descripcion = categoria.descripcion,
                imagenUrl = categoria.imagenUrl
            });
        }

        // POST: api/categorias
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<CategoriaResponseDTO>> createCategoria([FromForm] CategoriaCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoria = new Categorias
            {
                nombre = dto.nombre,
                descripcion = dto.descripcion
            };

            if (dto.imagenArchivo != null)
            {
                try
                {
                    categoria.imagen = await _imageService.SaveImage(dto.imagenArchivo);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return Ok(new CategoriaResponseDTO
            {
                Id = categoria.Id,
                nombre = categoria.nombre,
                descripcion = categoria.descripcion,
                imagenUrl = categoria.imagenUrl
            });
        }

        // PUT: api/categorias
        [HttpPut("{id}")]
        public async Task<IActionResult> updateCategoria(int id, [FromForm] CategoriaUpdateDTO dto)
        {
            var categoriaExistente = await _context.Categorias.FindAsync(id);
            
            if (categoriaExistente == null) 
                return NotFound();

            categoriaExistente.nombre = dto.nombre;
            categoriaExistente.descripcion = dto.descripcion;

            if (dto.imagenArchivo != null)
            {
                try
                {
                    categoriaExistente.imagen = await _imageService.SaveImage(dto.imagenArchivo);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/categorias/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(categoria.imagen))
                _imageService.deleteImage(categoria.imagen);

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
