namespace _1___Gaming_Website_Project.Models
{
    public class GameDevice
    {
        public int GameId { get; set; }
        public Game game { get; set; } = default!;

        public int DeviceId { get; set; }
        public Device device { get; set; } = default!;
    }
}
