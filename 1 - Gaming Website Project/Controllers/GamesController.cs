
using _1___Gaming_Website_Project.Models;
using _1___Gaming_Website_Project.Services;

namespace _1___Gaming_Website_Project.Controllers
{
    public class GamesController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IDevicesService _devicesService;
        private readonly IGameService _gameService;

        public GamesController(ICategoriesService categoriesService, IDevicesService devicesService, IGameService gameService)
        {
            _categoriesService = categoriesService;
            _devicesService = devicesService;
            _gameService = gameService; 
        }

        public IActionResult Index()
        {
            var games = _gameService.GetAll();
            return View(games);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel model = new() 
            {
                Categories = _categoriesService.GetSelectList(),
                Devices = _devicesService.GetSelectLists(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetSelectList();
                model.Devices = _devicesService.GetSelectLists();
                return View(model);
            }

            // save data in database
            // and save cover in server
            await _gameService.Create(model);
            return RedirectToAction(nameof(Index), "Games");
        }

        public IActionResult Details(int id)
        {
            var games = _gameService.GetByID(id);
            if (games is null)
                return NotFound();
            return View(games);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            // get data then edit it.
            var games = _gameService.GetByID(id);
            if (games is null)
                return NotFound();

            EditGameFormViewModel viewModel = new()
            {
                Id = id,
                Name = games.Name,
                Description = games.Description,
                CategoryId = games.CategoryId,
                CurrentCover = games.Cover,
                SelectedDevices = games.Devices.Select(d => d.DeviceId).ToList(),
                Devices = _devicesService.GetSelectLists(),
                Categories = _categoriesService.GetSelectList(),
            };
            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _categoriesService.GetSelectList();
                viewModel.Devices = _devicesService.GetSelectLists();
                return View(viewModel);
            }

            var games = await _gameService.Update(viewModel);
            if (games is null)
                return BadRequest();

            return RedirectToAction(nameof(Index), "Games");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            bool result = _gameService.Delete(id);
            return result? Ok() : BadRequest();
        }
    }
}

