using MakeATeamBE.Db.Models;

namespace MakeATeamBE.Db.Repositories
{
    public interface IUserRepository
    {
        public bool AddUser(string userId, string name);
        public void AddUserToTeam(string userId, int teamId);
        public UserDbo GetUser(string id);
    }
}
