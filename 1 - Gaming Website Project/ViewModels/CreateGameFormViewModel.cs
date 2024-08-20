using _1___Gaming_Website_Project.Attributes;

namespace _1___Gaming_Website_Project.ViewModels
{
    public class CreateGameFormViewModel:GameFormViewModel
    {        
        // validate extension and size
        [AllowedExtension(FileSettings.AllowedExtension), MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;
    }
}
