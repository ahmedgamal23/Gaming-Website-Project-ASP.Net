using _1___Gaming_Website_Project.Models;
using Microsoft.AspNetCore.Hosting;

namespace _1___Gaming_Website_Project.Services
{
    public class GameService : IGameService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;  // get path for root
        private string _imagePath;

        public GameService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagePath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePath}";
        }

        public async Task Create(CreateGameFormViewModel model)
        {
            var CoverName = await SaveCover(model.Cover);

            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = CoverName,
                Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList(),
            };
            _context.Games.Add(game);
            _context.SaveChanges();
        }

        public IEnumerable<Game> GetAll()
        {
            return _context.Games
                .Include(e => e.Category)
                .Include(e => e.Devices)
                .ThenInclude(d => d.device)
                .AsNoTracking()
                .ToList();
        }

        public Game? GetByID(int id)
        {
            return _context.Games
                .Include(e => e.Category)
                .Include(e => e.Devices)
                .ThenInclude(d => d.device)
                .AsNoTracking()
                .SingleOrDefault(x => x.Id == id);
        }

        public async Task<Game?> Update(EditGameFormViewModel model)
        {
            var game = _context.Games
                .Include(d => d.Devices)
                .SingleOrDefault(d => d.Id == model.Id);

            if (game is null)
                return null;

            var hasNewCover = model.Cover != null;
            var oldCover = game.Cover;

            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices = model.SelectedDevices.Select(d => new GameDevice() { DeviceId = d }).ToList();

            if (hasNewCover)
            {
                // update database with cover
                game.Cover = await SaveCover(model.Cover!);
            }

            _context.Games.Update(game);
            var effected_rows = _context.SaveChanges();
            if(effected_rows > 0)
            {
                if (hasNewCover)
                {
                    // remove old cover
                    var cover = Path.Combine(_imagePath, oldCover);
                    File.Delete(cover);
                }
                return game;
            }
            else
            {
                // update not complete
                var cover = Path.Combine(_imagePath, game.Cover);
                File.Delete(cover);
                return null;
            }
        }

        private async Task<string> SaveCover(IFormFile cover)
        {
            // to save cover in server
            var coverName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(_imagePath, coverName);

            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);
            return coverName;
        }

        public bool Delete(int id)
        {
            var game = _context.Games.Find(id);
            if (game is null)
                return false;

            _context.Games.Remove(game);           
            int effectedRows = _context.SaveChanges();

            if(effectedRows > 0)
            {
                // remove cover
                var cover = Path.Combine(_imagePath, game.Cover);
                File.Delete(cover);
                return true;
            }
            return false;
        }


    }
}
