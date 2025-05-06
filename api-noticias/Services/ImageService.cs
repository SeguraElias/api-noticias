namespace api_noticias.Services
{
    public class ImageService
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger _logger;

        public ImageService(IWebHostEnvironment env, ILogger<ImageService> logger)
        {
            _env = env;
            _logger = logger;
        }

        public async Task<string> SaveImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Archivo de imagen no valido");
            }

            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(extension) || !validExtensions.Contains(extension))
            {
                throw new NotSupportedException("Formato de imagen no soportado");
            }

            var imagesPath = Path.Combine(_env.WebRootPath, "uploads", "images");
            if (!Directory.Exists(imagesPath))
                Directory.CreateDirectory(imagesPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(imagesPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            _logger.LogInformation($"Imagen guardada en: {filePath}");
            return fileName;
        }

        public bool deleteImage(string imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName)) return false;

            var imagePath = Path.Combine(_env.WebRootPath, "uploads", "images", imageName);

            if (!System.IO.File.Exists(imagePath)) return false;

            System.IO.File.Delete(imagePath);
            return true;
        }

        public string getImagePath(string imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName)) return null;

            return Path.Combine("uploads", "images", imageName);
        }
    }
}
