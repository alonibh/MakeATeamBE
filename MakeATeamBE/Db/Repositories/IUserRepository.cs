using MakeATeamBE.Db.Models;

namespace MakeATeamBE.Db.Repositories
{
    public interface IUserRepository
    {
        void AddUserToTeam(int userId, int teamId);
        public UserDbo GetUser(int id);
    }
}
