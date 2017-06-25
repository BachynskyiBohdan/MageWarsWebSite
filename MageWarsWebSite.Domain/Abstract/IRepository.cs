namespace MageWarsWebSite.Domain.Abstract
{
    public interface IRepository
    {
        IDeskRepository DeskRepository { get; }
        ICardRepository CardRepository { get; }
        ISchoolRepository SchoolRepository { get; }
        ICardTypeRepository CardTypeRepository { get; }
        ISubTypeRepository SubTypeRepository { get; }

        IUserRepository UserRepository { get; }
        IGameRepository GameRepository { get; }
        IMageRepository MageRepository { get; }
    }
}
