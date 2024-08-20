using System.ComponentModel.DataAnnotations;

namespace _1___Gaming_Website_Project.Models
{
    public class Category : BaseEntity
    {
        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
