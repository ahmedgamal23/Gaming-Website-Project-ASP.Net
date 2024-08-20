namespace _1___Gaming_Website_Project.Models
{
    public class Device: BaseEntity
    {
        [MaxLength(500)]
        public string Icon { get; set; } = string.Empty;
        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
