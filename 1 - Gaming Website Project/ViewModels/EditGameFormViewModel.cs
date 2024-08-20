using _1___Gaming_Website_Project.Attributes;

namespace _1___Gaming_Website_Project.ViewModels
{
    public class EditGameFormViewModel:GameFormViewModel
    {
        public int Id { get; set; }

        public string? CurrentCover { get; set; }

        // validate extension and size
        [AllowedExtension(FileSettings.AllowedExtension), MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
