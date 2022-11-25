using MakeATeamBE.Db.Models;

namespace MakeATeamBE.Db.Repositories
{
    public interface IUserRepository
    {
        public UserDbo GetUser(int id);
    }
}
