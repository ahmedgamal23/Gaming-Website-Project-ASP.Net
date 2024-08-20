namespace _1___Gaming_Website_Project.Services
{
    public interface IGameService
    {
        Task Create(CreateGameFormViewModel model);
        IEnumerable<Game> GetAll();
        Game? GetByID(int id);
        Task<Game?> Update(EditGameFormViewModel model);
        bool Delete(int id);
    }
}
